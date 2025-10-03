
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting; // Add this using

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// FIX: Use WebAssemblyHostBuilder for Blazor WebAssembly projects
// Remove the following lines:
// builder.RootComponents.Add<App>("#app");
// builder.RootComponents.Add<HeadOutlet>("head::after");

// Instead, use WebAssemblyHostBuilder to register root components
var webAssemblyBuilder = WebAssemblyHostBuilder.CreateDefault(args);
//webAssemblyBuilder.RootComponents.Add<App>("#app");
//webAssemblyBuilder.RootComponents.Add<HeadOutlet>("head::after");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

//app.MapRazorComponents<App>()
//    .AddInteractiveWebAssemblyRenderMode()
//    .AddAdditionalAssemblies(typeof(BlazorClient.Client._Imports).Assembly);

app.Run();
