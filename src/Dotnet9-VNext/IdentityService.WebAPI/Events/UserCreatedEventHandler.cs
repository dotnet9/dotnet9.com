namespace IdentityService.WebAPI.Events;

[EventName("IdentityService.User.Created")]
public class UserCreatedEventHandler : JsonIntegrationEventHandler<UserCreatedEvent>
{
    private readonly ISmsSender _smsSender;

    public UserCreatedEventHandler(ISmsSender smsSender)
    {
        _smsSender = smsSender;
    }


    public override Task HandleJson(string eventName, UserCreatedEvent? eventData)
    {
        return _smsSender.SendAsync(eventData!.PhoneNumber, eventData.Password);
    }
}