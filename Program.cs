using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpClient("nav")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        // Configuración base para manejar certificados si es necesario
        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
    });

var app = builder.Build();

// Endpoint genérico para cualquier servicio SOAP de NAV
app.MapPost("/nav-soap", async (HttpRequest request, [FromServices] IHttpClientFactory factory) =>
{
    try
    {
        using var reader = new StreamReader(request.Body);
        var body = await reader.ReadToEndAsync();
        // Console.WriteLine("Raw JSON Request:\n" + body);

        var json = System.Text.Json.JsonSerializer.Deserialize<NavSoapRequest>(body);

        if (json is null || string.IsNullOrWhiteSpace(json.serviceUrl) ||
            string.IsNullOrWhiteSpace(json.usuario) ||
            string.IsNullOrWhiteSpace(json.password) ||
            string.IsNullOrWhiteSpace(json.dominio) ||
            string.IsNullOrWhiteSpace(json.operation) ||
            json.parameters is null)
        {
            return Results.BadRequest("Faltan datos requeridos: serviceUrl, usuario, password, dominio, operation, parameters");
        }

        // Console.WriteLine($"Calling NAV Service: {json.serviceUrl}");
        // Console.WriteLine($"Operation: {json.operation}");

        // Crear el cliente con credenciales dinámicas
        var handler = new HttpClientHandler
        {
            Credentials = new NetworkCredential(json.usuario, json.password, json.dominio),
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
        var client = new HttpClient(handler);

        // Construir los parámetros del SOAP
        var parametersXml = new StringBuilder();
        foreach (var param in json.parameters)
        {
            parametersXml.AppendLine($"<{param.Key}>{System.Security.SecurityElement.Escape(param.Value)}</{param.Key}>");
        }

        // Determinar el namespace basado en la URL del servicio
        var serviceName = ExtractServiceNameFromUrl(json.serviceUrl);
        var nameSpace = $"urn:microsoft-dynamics-schemas/codeunit/{serviceName}";
        var soapAction = $"{nameSpace}:{json.operation}";

        var soapEnvelope = $"""
        <?xml version="1.0" encoding="utf-8"?>
        <soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                       xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                       xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
          <soap:Body>
            <{json.operation} xmlns="{nameSpace}">
                {parametersXml}
            </{json.operation}>
          </soap:Body>
        </soap:Envelope>
        """;

        // Console.WriteLine("SOAP Envelope:");
        // Console.WriteLine(soapEnvelope);

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, json.serviceUrl)
        {
            Content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml")
        };

        httpRequest.Headers.Add("SOAPAction", $"\"{soapAction}\"");

        var navResponse = await client.SendAsync(httpRequest);
        var rawXml = await navResponse.Content.ReadAsStringAsync();

        // Console.WriteLine("NAV Response:");
        // Console.WriteLine(rawXml);

        if (!navResponse.IsSuccessStatusCode)
        {
            return Results.Problem($"Error from NAV service: {navResponse.StatusCode} - {rawXml}");
        }

        // Parsear la respuesta y extraer el resultado
        var result = ParseSoapResponse(rawXml, json.operation);
        // var result = XmlToJsonConverter.ConvertXmlToDynamicJson(result2);

        return Results.Ok(new
        {
            success = true,
            operation = json.operation,
            data = result
        });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex}");
        return Results.Problem($"Error interno: {ex.Message}");
    }
});

// Endpoint específico para RetailWebServices con métodos predefinidos
app.MapPost("/retail-services/{operation}", async (
    string operation,
    HttpRequest request,
    [FromServices] IHttpClientFactory factory) =>
{
    try
    {
        using var reader = new StreamReader(request.Body);
        var body = await reader.ReadToEndAsync();

        var json = System.Text.Json.JsonSerializer.Deserialize<RetailServiceRequest>(body);

        if (json is null || string.IsNullOrWhiteSpace(json.serviceUrl) ||
            string.IsNullOrWhiteSpace(json.usuario) ||
            string.IsNullOrWhiteSpace(json.password) ||
            string.IsNullOrWhiteSpace(json.dominio))
        {
            return Results.BadRequest("Faltan credenciales requeridas");
        }

        // Validar que la operación sea válida para RetailWebServices
        var validOperations = new[] {
            "WebRequest", "CreateResponseCode", "GetPosImage", "GetPosHtml",
            "IsOnline", "FillDataSet", "Find", "CreateRecordZoomData",
            "ValidateRecordZoomInput", "ImportRecordZoomData", "InsertRecord",
            "DeleteRecord", "WriteRequestLog", "Search"
        };

        if (!validOperations.Contains(operation))
        {
            return Results.BadRequest($"Operación '{operation}' no válida para RetailWebServices");
        }

        var navRequest = new NavSoapRequest(
            json.serviceUrl,
            json.usuario,
            json.password,
            json.dominio,
            operation,
            json.parameters ?? new Dictionary<string, string>()
        );

        // Reutilizar la lógica del endpoint genérico
        return await CallNavSoapService(navRequest, factory);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error: {ex.Message}");
    }
});

// Endpoint de utilidad para verificar conectividad
app.MapPost("/nav-health", async (HttpRequest request, [FromServices] IHttpClientFactory factory) =>
{
    try
    {
        using var reader = new StreamReader(request.Body);
        var body = await reader.ReadToEndAsync();

        var json = System.Text.Json.JsonSerializer.Deserialize<NavHealthRequest>(body);

        if (json is null || string.IsNullOrWhiteSpace(json.serviceUrl))
        {
            return Results.BadRequest("Se requiere serviceUrl");
        }

        var navRequest = new NavSoapRequest(
            json.serviceUrl,
            json.usuario ?? "",
            json.password ?? "",
            json.dominio ?? "",
            "IsOnline",
            new Dictionary<string, string>()
        );

        var result = await CallNavSoapService(navRequest, factory);
        // Console.WriteLine($"HEALTH resultl: {result}");
        return result;
    }
    catch (Exception ex)
    {
        return Results.Problem($"Health check failed: {ex.Message}");
    }
});

app.Run();

// Función auxiliar para extraer el nombre del servicio de la URL
static string ExtractServiceNameFromUrl(string url)
{
    try
    {
        var uri = new Uri(url);
        var segments = uri.Segments;
        // Buscar el último segmento que no esté vacío
        var serviceName = segments.LastOrDefault(s => !string.IsNullOrWhiteSpace(s) && s != "/");
        return Uri.UnescapeDataString(serviceName?.TrimEnd('/') ?? "UnknownService");
    }
    catch
    {
        return "UnknownService";
    }
}

// Función auxiliar para parsear respuestas SOAP
static object ParseSoapResponse(string soapXml, string operation)
{
    try
    {
        var doc = XDocument.Parse(soapXml);
        var ns = XNamespace.Get("urn:microsoft-dynamics-schemas/codeunit/RetailWebServices");

        // Buscar el nodo de respuesta
        var responseElement = doc.Descendants().FirstOrDefault(x =>
            x.Name.LocalName.Equals($"{operation}_Result", StringComparison.OrdinalIgnoreCase));

        if (responseElement != null)
        {
            var result = new Dictionary<string, object>();
            foreach (var element in responseElement.Elements())
            {
                result[element.Name.LocalName] = XmlToJsonConverter.ConvertXmlToDynamicJson(element.Value);
            }
            return result;
        }

        // Si no encontramos un resultado estructurado, devolver el XML completo
        return new { rawResponse = soapXml };
    }
    catch (Exception ex)
    {
        return new { error = $"Error parsing response: {ex.Message}", rawResponse = soapXml };
    }
}

// Función auxiliar reutilizable para llamadas SOAP
static async Task<IResult> CallNavSoapService(NavSoapRequest navRequest, IHttpClientFactory factory)
{
    var handler = new HttpClientHandler
    {
        Credentials = new NetworkCredential(navRequest.usuario, navRequest.password, navRequest.dominio),
        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
    };
    var client = new HttpClient(handler);

    var parametersXml = new StringBuilder();
    foreach (var param in navRequest.parameters)
    {
        parametersXml.AppendLine($"<{param.Key}>{System.Security.SecurityElement.Escape(param.Value)}</{param.Key}>");
    }

    var serviceName = ExtractServiceNameFromUrl(navRequest.serviceUrl);
    var nameSpace = $"urn:microsoft-dynamics-schemas/codeunit/{serviceName}";
    var soapAction = $"{nameSpace}:{navRequest.operation}";

    var soapEnvelope = $"""
    <?xml version="1.0" encoding="utf-8"?>
    <soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                   xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                   xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
      <soap:Body>
        <{navRequest.operation} xmlns="{nameSpace}">
        {parametersXml}        </{navRequest.operation}>
      </soap:Body>
    </soap:Envelope>
    """;

    var httpRequest = new HttpRequestMessage(HttpMethod.Post, navRequest.serviceUrl)
    {
        Content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml")
    };

    httpRequest.Headers.Add("SOAPAction", $"\"{soapAction}\"");

    var navResponse = await client.SendAsync(httpRequest);
    var rawXml = await navResponse.Content.ReadAsStringAsync();

    if (!navResponse.IsSuccessStatusCode)
    {
        return Results.Problem($"Error from NAV service: {navResponse.StatusCode} - {rawXml}");
    }

    var result = ParseSoapResponse(rawXml, navRequest.operation);

    return Results.Ok(new
    {
        success = true,
        operation = navRequest.operation,
        data = result
    });
}

// Records para los diferentes tipos de request
record NavSoapRequest(
    string serviceUrl,
    string usuario,
    string password,
    string dominio,
    string operation,
    Dictionary<string, string> parameters
);

record RetailServiceRequest(
    string serviceUrl,
    string usuario,
    string password,
    string dominio,
    Dictionary<string, string>? parameters
);

record NavHealthRequest(
    string serviceUrl,
    string? usuario,
    string? password,
    string? dominio
);