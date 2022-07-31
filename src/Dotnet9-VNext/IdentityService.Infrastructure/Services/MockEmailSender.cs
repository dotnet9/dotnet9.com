namespace IdentityService.Infrastructure.Services;

internal class MockEmailSender : IEmailSender
{
    private readonly ILogger<MockEmailSender> _logger;

    public MockEmailSender(ILogger<MockEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(string toEmail, string subject, string body)
    {
        _logger.LogInformation($"Email sent to {toEmail} with subject {subject} and body {body}");


        return Task.CompletedTask;
    }
}