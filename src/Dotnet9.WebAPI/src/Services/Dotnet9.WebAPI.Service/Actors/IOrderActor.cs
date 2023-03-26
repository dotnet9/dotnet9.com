namespace Dotnet9.WebAPI.Service.Actors;

public interface IOrderActor : IActor
{
    Task<List<Order>> GetListAsync();
}