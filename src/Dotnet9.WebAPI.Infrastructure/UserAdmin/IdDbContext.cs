namespace Dotnet9.WebAPI.Infrastructure.UserAdmin;

public class IdDbContext : IdentityDbContext<User, Role, Guid>
{
    public IdDbContext(DbContextOptions<IdDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.EnableSoftDeletionGlobalFilter();
    }
}