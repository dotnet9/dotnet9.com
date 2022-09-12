namespace Dotnet9.WebAPI.Infrastructure;

public class Dotnet9DbContext : IdentityDbContext<User, Role, Guid>
{
    public Dotnet9DbContext(DbContextOptions<Dotnet9DbContext> options)
        : base(options)
    {
    }

    public DbSet<About> Abouts { get; } = null!;
    public DbSet<ActionLog> ActionLogs { get; } = null!;
    public DbSet<Category> Categories { get; } = null!;
    public DbSet<Album> Albums { get; } = null!;
    public DbSet<Tag> Tags { get; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.EnableSoftDeletionGlobalFilter();
    }
}