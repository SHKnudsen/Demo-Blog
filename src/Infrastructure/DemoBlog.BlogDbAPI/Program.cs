using System.Text.Json;
using DemoBlog.Data.Configuration;
using DemoBlog.FunctionsAPIsShared;
using DemoBlog.Services.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(workerApplication =>
    {
        workerApplication.UseMiddleware<ExceptionHandlingMiddleware>();
    })
    .ConfigureServices(serviceCollection =>
    {
        serviceCollection
            .RegisterDbContext()
            .RegisterBlogDbService();
            //.Configure<JsonSerializerOptions>(o =>
            //{
            //    o.PropertyNameCaseInsensitive = true;
            //});
    })
    .ConfigureHostConfiguration(builder =>
    {
        builder
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .Build();

host.Run();