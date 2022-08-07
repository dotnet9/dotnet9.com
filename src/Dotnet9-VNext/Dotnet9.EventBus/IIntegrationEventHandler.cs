namespace Dotnet9.EventBus;

public interface IIntegrationEventHandler
{
    Task Handle(string eventName, string eventData);
}