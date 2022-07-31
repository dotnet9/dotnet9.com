namespace IdentityService.Infrastructure;

internal class IdDbContext : IdentityDbContext<User, Role, Guid>
{
    public IdDbContext(DbContextOptions<IdDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        builder.EnableSoftDeletionGlobalFilter();
    }
}