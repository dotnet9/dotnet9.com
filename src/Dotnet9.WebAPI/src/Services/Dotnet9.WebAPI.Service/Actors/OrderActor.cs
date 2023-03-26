namespace Dotnet9.WebAPI.Service.Actors;

public class OrderActor : Actor, IOrderActor
{
    readonly IOrderRepository _orderRepository;

    public OrderActor(ActorHost host, IOrderRepository orderRepository) : base(host)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<Order>> GetListAsync()
    {
        var data = await _orderRepository.GetListAsync();
        return data;
    }
}