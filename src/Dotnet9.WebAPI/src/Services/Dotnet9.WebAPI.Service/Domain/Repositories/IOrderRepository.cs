namespace Dotnet9.WebAPI.Service.Domain.Repositories;


public interface IOrderRepository : IRepository<Order>
{
    Task<List<Order>> GetListAsync();
}