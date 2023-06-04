using DemoBlog.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DemoBlog.Data.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../DemoBlog.FunctionsAPI/local.settings.json")
                .Build();

            var connectionConfig = new DbContextConnection(configuration);

            var builder = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionConfig.ConnectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
