namespace Dotnet9.WebAPI.Service.Infrastructure;

public class ShopDbContext : MasaDbContext
{
    public DbSet<Order> Orders { get; set; } = default!;

    public ShopDbContext(MasaDbContextOptions<ShopDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreatingExecuting(ModelBuilder builder)
    {
        base.OnModelCreatingExecuting(builder);
    }
}