namespace IdentityService.Infrastructure.Options;

internal class SendCloudEmailSettings
{
    public string? ApiUser { get; set; }

    public string? ApiKey { get; set; }

    public string? From { get; set; }
}