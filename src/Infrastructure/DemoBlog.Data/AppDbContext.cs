using DemoBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoBlog.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<BlogPost> BlogPosts { get; init; }

        public AppDbContext(DbContextOptions options) : base(options) { }
    }
}
