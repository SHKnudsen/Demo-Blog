using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DemoBlog.API;
using DemoBlog.API.Configuration;
using DemoBlog.API.Services;
using DemoBlog.Application.Interfaces.Configuration;
using DemoBlog.Application.Interfaces.Storage;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace DemoBlog.API
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigurationBuilder
                .SetBasePath(builder.GetContext().ApplicationRootPath)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddSingleton<IMediaStorageService, BlobMediaStorageService>()
                .AddSingleton<IDatabaseConnection, MediaStorageConnection>();
        }
    }
}
