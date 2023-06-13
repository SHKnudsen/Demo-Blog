using DemoBlog.Services.Abstraction.Configuration;
using DemoBlog.Services.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace DemoBlog.Services.Configuration
{
    public static class Setup
    {
        public static IServiceCollection RegisterMediaServices(
            this IServiceCollection services)
            => services
                .AddSingleton<IMediaStorageService, BlobMediaStorageService>()
                .AddSingleton<IBlobStorageConnection, MediaStorageConnection>();

        public static IServiceCollection RegisterBlogDbService(
            this IServiceCollection services)
            => services.AddSingleton<IBlogPostDbService, BlogDbService>();
    }
}
