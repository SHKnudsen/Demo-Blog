using System.Data;
using System.Reflection.Metadata;
using DemoBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoBlog.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<BlogPost> BlogPosts { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureBlogPost(modelBuilder);
        }

        private void ConfigureBlogPost(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
