namespace Dotnet9.WebAPI.EventHandlers;

public class ActionLogCreatedEventHandler : INotificationHandler<ActionLogCreatedEvent>
{
    private readonly IEventBus _eventBus;
    private readonly Dotnet9DbContext _dbContext;
    private readonly ActionLogManager _actionLogManager;

    public ActionLogCreatedEventHandler(IEventBus eventBus, Dotnet9DbContext dbContext,
        ActionLogManager actionLogManager)
    {
        _eventBus = eventBus;
        _dbContext = dbContext;
        _actionLogManager = actionLogManager;
    }

    public async Task Handle(ActionLogCreatedEvent notification, CancellationToken cancellationToken)
    {
        //把领域事件转发为集成事件，让其他微服务听到

        //在领域事件处理中集中进行更新缓存等处理，而不是写到Controller中。因为项目中有可能不止一个地方操作领域对象，这样就就统一了操作。
        
        //var actionLog = _actionLogManager.Create(notification.UId, notification.Ua, notification.Os,
        //    notification.Browser, notification.Ip,
        //    notification.Referer, notification.AccessName,
        //    notification.Original, notification.Url, notification.Controller, notification.Action, notification.Method,
        //    notification.Arguments,
        //    notification.Duration);
        //var actionLogFromDb = await _dbContext.AddAsync(actionLog);
        //await _dbContext.SaveChangesAsync();
        await Task.CompletedTask;
    }
}