namespace Dotnet9.WebAPI.Service.Application.Orders;

public class OrderQueryHandler
{
    readonly IOrderRepository _orderRepository;
    public OrderQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [EventHandler]
    public async Task OrderListHandleAsync(OrderQuery query)
    {
        var actorId = new ActorId(Guid.NewGuid().ToString());
        var actor = ActorProxy.Create<IOrderActor>(actorId, nameof(OrderActor));
        query.Result = await actor.GetListAsync();
    }
}