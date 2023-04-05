namespace Dotnet9.Service.Infrastructure;

public class Dotnet9DbContext : MasaDbContext<Dotnet9DbContext>
{
    public DbSet<FriendlyLink> FriendlyLinks { get; set; } = default!;

    public Dotnet9DbContext(MasaDbContextOptions<Dotnet9DbContext> options) : base(options)
    {
    }

    protected override void OnModelCreatingExecuting(ModelBuilder builder)
    {
        base.OnModelCreatingExecuting(builder);
    }
}