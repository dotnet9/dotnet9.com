namespace Dotnet9.WebAPI.Service.Application.Orders;

public class OrderCommandHandler
{
    private readonly OrderDomainService _domainService;

    public OrderCommandHandler(OrderDomainService domainService)
    {
        _domainService = domainService;
    }

    [EventHandler(Order = 1)]
    public async Task CreateHandleAsync(OrderCreateCommand command)
    {
        await _domainService.PlaceOrderAsync();
        //todo your work
        await Task.CompletedTask;
    }
}

public class OrderStockHandler : CommandHandler<OrderCreateCommand>
{
    public new Task CancelAsync(OrderCreateCommand @event, CancellationToken cancellationToken = new CancellationToken())
    {
        //todo cancel todo callback 
        return Task.CompletedTask;
    }

    [EventHandler(FailureLevels = FailureLevels.ThrowAndCancel)]
    public override Task HandleAsync(OrderCreateCommand @event, CancellationToken cancellationToken = new CancellationToken())
    {
        //todo decrease stock
        return Task.CompletedTask;
    }

    [EventHandler(0, FailureLevels.Ignore, IsCancel = true)]
    public Task AddCancelLogs(OrderCreateCommand query)
    {
        //todo increase stock
        return Task.CompletedTask;
    }

}