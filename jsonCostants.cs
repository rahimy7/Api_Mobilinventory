// using System.Xml.Linq;

// using Microsoft.Extensions.Configuration;

// var builder = WebApplication.CreateBuilder(args);
// public static class JsonConstants
// {
//     public static string ServiceUrl = "https://beta.innovacentro.com.do:18597/SANA-TEST/WS/La%20Innovacion%20SRL/Codeunit/RetailWebServices";
//     public static string Usuario = "jrodriguez";
//     public static string Password = "Volumen1";
//     public static string Dominio = "MONTECARLO_DB";
//     public static double Timeout = 30000;
// }

// using System.Xml.Linq;
// using Microsoft.Extensions.Configuration;

// var builder = WebApplication.CreateBuilder(args);

// // Agregar servicios
// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// // SOLUCIÓN 1: Inicializar la clase estática después de crear el builder
// JsonConstants.Initialize(builder.Configuration);

// // SOLUCIÓN 2: Usar Options Pattern (RECOMENDADO)
// builder.Services.Configure<DatabaseSettings>(
//     builder.Configuration.GetSection("DatabaseSettings"));

// var app = builder.Build();

// // Configurar pipeline
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();
// app.UseAuthorization();
// app.MapControllers();

// app.Run();

// // SOLUCIÓN 1: Clase estática que se inicializa
// public static class JsonConstants
// {
//     public static string ServiceUrl { get; private set; } = string.Empty;
//     public static string Usuario { get; private set; } = string.Empty;
//     public static string Password { get; private set; } = string.Empty;
//     public static string Dominio { get; private set; } = string.Empty;
//     public static double Timeout { get; private set; }

//     public static void Initialize(IConfiguration configuration)
//     {
//         ServiceUrl = configuration["DatabaseSettings:ServerUrl"] ?? "https://beta.innovacentro.com.do:18597/SANA-TEST/WS/La%20Innovacion%20SRL/Codeunit/RetailWebServices";
//         Usuario = configuration["DatabaseSettings:Username"] ?? "jrodriguez";
//         Password = configuration["DatabaseSettings:Password"] ?? "Volumen1";
//         Dominio = configuration["DatabaseSettings:Domain"] ?? "MONTECARLO_DB";
//         Timeout = configuration.GetValue<double>("DatabaseSettings:TimeoutSeconds", 30000);
//     }
// }

// // SOLUCIÓN 2: Clase de configuración para Options Pattern (RECOMENDADO)
// public class DatabaseSettings
// {
//     public string ServerUrl { get; set; } = "https://beta.innovacentro.com.do:18597/SANA-TEST/WS/La%20Innovacion%20SRL/Codeunit/RetailWebServices";
//     public string Username { get; set; } = "jrodriguez";
//     public string Password { get; set; } = "Volumen1";
//     public string Domain { get; set; } = "MONTECARLO_DB";
//     public double TimeoutSeconds { get; set; } = 30000;
// }

// // SOLUCIÓN 3: Servicio singleton
// public interface IConfigurationService
// {
//     string ServiceUrl { get; }
//     string Usuario { get; }
//     string Password { get; }
//     string Dominio { get; }
//     double Timeout { get; }
// }

// public class ConfigurationService : IConfigurationService
// {
//     private readonly IConfiguration _configuration;

//     public ConfigurationService(IConfiguration configuration)
//     {
//         _configuration = configuration;
//     }

//     public string ServiceUrl => _configuration["DatabaseSettings:ServerUrl"] ?? "https://beta.innovacentro.com.do:18597/SANA-TEST/WS/La%20Innovacion%20SRL/Codeunit/RetailWebServices";
//     public string Usuario => _configuration["DatabaseSettings:Username"] ?? "jrodriguez";
//     public string Password => _configuration["DatabaseSettings:Password"] ?? "Volumen1";
//     public string Dominio => _configuration["DatabaseSettings:Domain"] ?? "MONTECARLO_DB";
//     public double Timeout => _configuration.GetValue<double>("DatabaseSettings:TimeoutSeconds", 30000);
// }