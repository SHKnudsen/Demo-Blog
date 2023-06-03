using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using DemoBlog.Services.Configuration;
using Microsoft.Azure.Functions.Worker;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(workerApplication =>
    {
        workerApplication.UseAspNetCoreIntegration();
    })
    .ConfigureAspNetCoreIntegration()
    .ConfigureServices(serviceCollection =>
    {
        serviceCollection.RegisterServices();
    })
    .ConfigureHostConfiguration(builder =>
    {
        builder
            //.SetBasePath(builder.GetContext().ApplicationRootPath)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .Build();

host.Run();