using Blazored.LocalStorage;
using DemoBlog.BlazorClient;
using DemoBlog.BlazorClient.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
    .AddMudServices()
    .AddBlazoredLocalStorage()
    .AddScoped<IUserPreferencesService, UserPreferencesService>()
    .AddScoped<LayoutService>();

await builder.Build().RunAsync();
