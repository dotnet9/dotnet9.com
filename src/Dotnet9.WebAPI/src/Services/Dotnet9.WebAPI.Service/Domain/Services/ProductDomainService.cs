using Masa.BuildingBlocks.Ddd.Domain.Services;

namespace Dotnet9.WebAPI.Service.Domain.Services;

public class ProductDomainService : DomainService
// you can alse implement an interface like below
//, ISagaEventHandler<OrderCreatedDomainEvent>
{

    public ProductDomainService(IDomainEventBus eventBus) : base(eventBus)
    {
    }

    [EventHandler(Order = 0, FailureLevels = FailureLevels.ThrowAndCancel)]
    public Task DeductInvenroyAsync(OrderCreatedDomainEvent @event)
    {
        //todo decrease stock
        throw new NotImplementedException();
    }

    [EventHandler(Order = 1)]
    public void DeductInvenroyCompletedAsync(OrderCreatedDomainEvent @event)
    {
        //todo after decrease stock,like Pub Event to other micro service
    }

    [EventHandler(1, FailureLevels.Ignore, IsCancel = true)]
    public Task CancelDeductInvenroyAsync(OrderCreatedDomainEvent @event)
    {
        //here should be used with ISagaEventHandler

        //throw exception,todo increase stock
        throw new NotImplementedException();
    }
}