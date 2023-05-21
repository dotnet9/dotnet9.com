using Npgsql;

namespace Dotnet9.Service.Infrastructure;

public class Dotnet9DbContext : MasaDbContext<Dotnet9DbContext>
{
    public DbSet<About> Abouts { get; set; } = default!;
    public DbSet<ActionLog> ActionLogs { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Album> Albums { get; set; } = default!;
    public DbSet<Tag> Tags { get; set; } = default!;
    public DbSet<Blog> Blogs { get; set; } = default!;
    public DbSet<Donation> Donations { get; set; } = default!;
    public DbSet<Privacy> Privacies { get; set; } = default!;
    public DbSet<FriendlyLink> FriendlyLinks { get; set; } = default!;
    public DbSet<Timeline> Timelines { get; set; } = default!;
    public DbSet<Comment> Comments { get; set; } = default!;

    public Dotnet9DbContext(MasaDbContextOptions<Dotnet9DbContext> options) : base(options)
    {
    }

    protected override void OnModelCreatingExecuting(ModelBuilder builder)
    {
        base.OnModelCreatingExecuting(builder);
    }
}