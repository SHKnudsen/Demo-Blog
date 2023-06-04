using DemoBlog.Data.Configuration;
using DemoBlog.FunctionsAPI.Middleware;
using DemoBlog.Services.Configuration;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(workerApplication =>
    {
        workerApplication.UseMiddleware<ExceptionHandlingMiddleware>();
    })
    .ConfigureOpenApi()
    .ConfigureServices(serviceCollection =>
    {
        serviceCollection
            .RegisterDbContext()
            .RegisterServices();
    })
    .ConfigureHostConfiguration(builder =>
    {
        builder
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .Build();

host.Run();