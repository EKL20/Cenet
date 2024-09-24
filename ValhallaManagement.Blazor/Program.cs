using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ValhallaManagement.Blazor;
using ValhallaManagement.Blazor.Services;
using System.Net.Http.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Cargar configuración desde appsettings.json en wwwroot
var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var settings = await httpClient.GetFromJsonAsync<AppSettings>("appsettings.json");

// Usa la configuración de la API desde appsettings.json
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(settings.ApiUrl) });
builder.Services.AddScoped<IVikingoService, VikingoService>();

await builder.Build().RunAsync();

// Clase para mapear la configuración de appsettings.json
public class AppSettings
{
    public string ApiUrl { get; set; }
}
