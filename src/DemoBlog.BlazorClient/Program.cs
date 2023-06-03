using Blazored.LocalStorage;
using DemoBlog.BlazorClient;
using DemoBlog.BlazorClient.Services;
using DemoBlog.BlazorClient.Services.HttpClients;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseAddress = builder.Configuration["BaseAddress"] ?? builder.HostEnvironment.BaseAddress;

builder.Services.AddHttpClient<MediaBlobClient>(client => client.BaseAddress = new Uri(baseAddress));

builder.Services
    .AddMudServices()
    .AddBlazoredLocalStorage()
    .AddScoped<IUserPreferencesService, UserPreferencesService>()
    .AddScoped<LayoutService>();

await builder.Build().RunAsync();
