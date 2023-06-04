using DemoBlog.Data.Repositories;
using DemoBlog.Domain.Repositories;
using DemoBlog.Services.Abstraction.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DemoBlog.Data.Configuration;

public static class Setup
{
    public static IServiceCollection RegisterDbContext(
        this IServiceCollection services)
            => services
                .AddSingleton<IDbConnection, DbContextConnection>()
                .AddPooledDbContextFactory<AppDbContext>((provider, opt) =>
                {
                    var connectionString = provider
                        .GetRequiredService<IDbConnection>().ConnectionString;
                    opt.UseSqlServer(connectionString);
                })
                .AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
}
