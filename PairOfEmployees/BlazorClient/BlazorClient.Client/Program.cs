using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Web;
using BlazorClient.Client;
using BlazorClient.Client.Pages; // Add this using directive if App is defined here


var builder = WebAssemblyHostBuilder.CreateDefault(args);

var apiBase = builder.Configuration.GetValue<string>("ApiBaseUrl")
    ?? builder.HostEnvironment.BaseAddress;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBase) });

builder.Services.AddHttpClient("api", client =>
{     client.BaseAddress = new Uri(apiBase);
});

builder.Services.AddScoped<ApiClient>();

await builder.Build().RunAsync();


