using Blazored.LocalStorage;
using Blazorise;
using DemoBlog.BlazorClient;
using DemoBlog.BlazorClient.Services;
using DemoBlog.BlazorClient.Services.HttpClients;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Polly;
using Polly.Extensions.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpRetryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
    .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(retry));

builder.Services
    .AddHttpClient<AzureFunctionMediaHttpClient>(client =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("AzureFunctionMediaApiAddress"));
    })
    .AddPolicyHandler(httpRetryPolicy);

builder.Services
    .AddHttpClient<AzureFunctionBlogPostsHttpClient>(client =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("AzureFunctionBlogPostApiAddress"));
    })
    .AddPolicyHandler(httpRetryPolicy);

builder.Services
    .AddMudServices()
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddEmptyProviders()
    .AddBlazoredLocalStorage()
    .AddScoped<IUserPreferencesService, UserPreferencesService>()
    .AddScoped<LayoutService>();

await builder.Build().RunAsync();
