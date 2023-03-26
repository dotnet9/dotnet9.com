namespace Dotnet9.WebAPI.Service.Infrastructure.Repositories;

public class OrderRepository : Repository<ShopDbContext, Order>, IOrderRepository
{
    public OrderRepository(ShopDbContext context, IUnitOfWork unitOfWork)
        : base(context, unitOfWork)
    {
    }
    public async Task<List<Order>> GetListAsync()
    {
        var data = Enumerable.Range(1, 5).Select(index =>
                  new Order(index, DateTimeOffset.Now.ToUnixTimeSeconds().ToString())).ToList();
        return await Task.FromResult(data);
    }
}