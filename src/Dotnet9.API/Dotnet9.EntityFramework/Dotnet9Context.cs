using Dotnet9.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dotnet9.EntityFramework;

public class Dotnet9Context : DbContext
{
    public Dotnet9Context(DbContextOptions<Dotnet9Context> options) : base(options)
    {
    }

    public DbSet<BlogPost>? BlogPosts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogPost>().Property(p => p.Title).HasMaxLength(128);
        modelBuilder.Entity<BlogPost>().Property(p => p.Slug).HasMaxLength(128);
        modelBuilder.Entity<BlogPost>().Property(p => p.Creator).HasMaxLength(64);
        modelBuilder.Entity<BlogPost>().Property(p => p.Categories).HasMaxLength(256);
        modelBuilder.Entity<BlogPost>().Property(p => p.Albums).HasMaxLength(256);
        modelBuilder.Entity<BlogPost>().Property(p => p.Tags).HasMaxLength(256);
        modelBuilder.Entity<BlogPost>().Property(p => p.Content).HasMaxLength(1024 * 10);
        modelBuilder.Entity<BlogPost>().Property(p => p.Remark).HasMaxLength(1024);
    }
}