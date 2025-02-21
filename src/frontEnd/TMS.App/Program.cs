using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TMS.App.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.IO;
using System.Text;

namespace TMS.App;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        // Load appsettings.json from wwwroot
        var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
        var appSettingsResponse = await http.GetAsync("appsettings.json");
        var appSettingsJson = await appSettingsResponse.Content.ReadAsStringAsync();

        // Parse the JSON configuration
        var configuration = new ConfigurationBuilder()
            .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettingsJson)))
            .Build();

        // Register the configuration in DI
        builder.Services.AddSingleton<IConfiguration>(configuration);

        // Register HttpClient with the API base address from configuration
        var apiBaseAddress = configuration["ApiBaseAddress"];
        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri(apiBaseAddress)
        });

        // Register your services
        builder.Services.AddScoped<IEventAPIService, EventApiService>();

        await builder.Build().RunAsync();
    }
}