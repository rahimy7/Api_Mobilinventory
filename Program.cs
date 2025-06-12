using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpClient("nav");

var app = builder.Build();

app.MapPost("/soap", async (HttpRequest request, [FromServices] IHttpClientFactory factory) =>
{
    try
    {
        using var reader = new StreamReader(request.Body);
        var body = await reader.ReadToEndAsync();
        Console.WriteLine("Raw JSON:\n" + body);

        var json = System.Text.Json.JsonSerializer.Deserialize<NavRequest>(body);

        if (json is null || string.IsNullOrWhiteSpace(json.url) ||
            string.IsNullOrWhiteSpace(json.usuario) ||
            string.IsNullOrWhiteSpace(json.password) ||
            string.IsNullOrWhiteSpace(json.dominio) ||
            string.IsNullOrWhiteSpace(json.payload))
        {
            return Results.BadRequest("Faltan datos requeridos");
        }

        Console.WriteLine("Received JSON:");
        Console.WriteLine(json);

        // Crear el cliente con credenciales dinámicas
        var handler = new HttpClientHandler
        {
            Credentials = new NetworkCredential(json.usuario, json.password, json.dominio)
        };
        var client = new HttpClient(handler);

        var soapEnvelope = $"""
        <?xml version="1.0" encoding="utf-8"?>
        <soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                       xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                       xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
          <soap:Body>
            <ProcessRequest xmlns="urn:microsoft-dynamics-schemas/codeunit/SC_NAV_WebService">
              <request><![CDATA[
                {json.payload}
              ]]></request>
              <response></response>
            </ProcessRequest>
          </soap:Body>
        </soap:Envelope>
        """;

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, json.url)
        {
            Content = new StringContent(soapEnvelope, System.Text.Encoding.UTF8, "text/xml")
        };

        httpRequest.Headers.Add("SOAPAction", "\"urn:microsoft-dynamics-schemas/codeunit/SC_NAV_WebService:ProcessRequest\"");

        var navResponse = await client.SendAsync(httpRequest);
        var rawXml = await navResponse.Content.ReadAsStringAsync();

        // Extraer contenido del <response> y desescapar XML interno
        var xmlDoc = XDocument.Parse(rawXml);
        var responseNode = xmlDoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "response");
        var decodedResponse = WebUtility.HtmlDecode(responseNode?.Value ?? "No se encontró <response>");

        return Results.Content(decodedResponse, "text/xml");
    }
    catch (Exception ex)
    {
        return Results.Problem("Error interno: " + ex.Message);
    }
});

app.Run();

record NavRequest(string url, string usuario, string password, string dominio, string payload);
