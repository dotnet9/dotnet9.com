using Masa.BuildingBlocks.Ddd.Domain.Services;

namespace Dotnet9.WebAPI.Service.Domain.Services;

public class OrderDomainService : DomainService
{
    private readonly ILogger<Order> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderDomainService(IDomainEventBus eventBus, ILogger<Order> logger, IOrderRepository orderRepository) : base(eventBus)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    public async Task PlaceOrderAsync()
    {
        //todo create order
        var orderEvent = new OrderCreatedDomainEvent();
        await EventBus.PublishAsync(orderEvent);
    }

    public async Task<IList<Order>> QueryListAsync()
    {
        var actorId = new ActorId(Guid.NewGuid().ToString());
        var actor = ActorProxy.Create<IOrderActor>(actorId, nameof(OrderActor));
        return await actor.GetListAsync();
    }
}