namespace IdentityService.Infrastructure.Services;

public class MockSmsSender : ISmsSender
{
    private readonly ILogger<MockSmsSender> _logger;

    public MockSmsSender(ILogger<MockSmsSender> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(string toPhoneNumber, params string[] args)
    {
        _logger.LogInformation($"Send Sms to {toPhoneNumber}, args: {string.Join(",", args)}");
        return Task.CompletedTask;
    }
}