using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoBlog.Data.Configuration;

public static class Setup
{
    public static IServiceCollection RegisterDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
            => services
                .AddPooledDbContextFactory<AppDbContext>(opt =>
                    opt.UseSqlServer(configuration.GetConnectionString("SqlDb")));
}
