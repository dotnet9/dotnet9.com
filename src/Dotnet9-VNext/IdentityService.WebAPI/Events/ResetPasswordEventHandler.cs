namespace IdentityService.WebAPI.Events;

[EventName("IdentityService.User.PasswordReset")]
public class ResetPasswordEventHandler : JsonIntegrationEventHandler<ResetPasswordEvent>
{
    private readonly ILogger<ResetPasswordEventHandler> _logger;
    private readonly ISmsSender _smsSender;

    public ResetPasswordEventHandler(ILogger<ResetPasswordEventHandler> logger, ISmsSender smsSender)
    {
        _logger = logger;
        _smsSender = smsSender;
    }

    public override Task HandleJson(string eventName, ResetPasswordEvent? eventData)
    {
        return _smsSender.SendAsync(eventData.PhoneNumber, eventData.Password);
    }
}