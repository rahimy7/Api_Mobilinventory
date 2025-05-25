using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Cliente HTTP con credenciales explícitas
builder.Services
    .AddHttpClient("nav")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        Credentials = new NetworkCredential("jrodriguez", "Volumen1", "MONTECARLO_DB")
    });

var app = builder.Build();

app.MapPost("/soap", async (HttpRequest request, [FromServices] IHttpClientFactory factory) =>
{
    using var reader = new StreamReader(request.Body);
    var xmlRequestInner = await reader.ReadToEndAsync();

    // Construir envelope SOAP completo con CDATA
    var soapEnvelope = $"""
    <?xml version="1.0" encoding="utf-8"?>
    <soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                   xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                   xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
      <soap:Body>
        <ProcessRequest xmlns="urn:microsoft-dynamics-schemas/codeunit/SC_NAV_WebService">
          <request><![CDATA[
            {xmlRequestInner}
          ]]></request>
          <response></response>
        </ProcessRequest>
      </soap:Body>
    </soap:Envelope>
    """;

    try
    {
        var client = factory.CreateClient("nav");

        var soapRequest = new HttpRequestMessage(HttpMethod.Post,
            "https://beta.innovacentro.com.do:18597/SANA-TEST/WS/La%20Innovacion%20SRL/Codeunit/SC_NAV_WebService")
        {
            Content = new StringContent(soapEnvelope, System.Text.Encoding.UTF8, "text/xml")
        };

        // SOAPAction requerido por NAV
        soapRequest.Headers.Add("SOAPAction", "\"urn:microsoft-dynamics-schemas/codeunit/SC_NAV_WebService:ProcessRequest\"");

        var response = await client.SendAsync(soapRequest);
        var result = await response.Content.ReadAsStringAsync();

        return Results.Content(result, "text/xml");
    }
    catch (Exception ex)
    {
        return Results.Problem("Error: " + ex.Message);
    }
});

app.Run();
