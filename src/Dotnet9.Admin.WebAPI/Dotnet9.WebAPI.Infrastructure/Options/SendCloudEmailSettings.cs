namespace Dotnet9.WebAPI.Infrastructure.Options;

public class SendCloudEmailSettings
{
    public string? ApiUser { get; set; }
    public string? ApiKey { get; set; }
    public string? From { get; set; }
}