namespace Dotnet9.WebAPI.EFCore;

public class Dotnet9DbContext : IdentityDbContext<User, Role, Guid>
{
    public Dotnet9DbContext(DbContextOptions<Dotnet9DbContext> options)
        : base(options)
    {
    }

    public DbSet<About> Abouts { get; private set; }
    public DbSet<ActionLog> ActionLogs { get; private set; }
    public DbSet<Category> Categories { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.EnableSoftDeletionGlobalFilter();
    }
}