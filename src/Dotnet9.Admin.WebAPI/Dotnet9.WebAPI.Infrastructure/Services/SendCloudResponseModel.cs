namespace Dotnet9.WebAPI.Infrastructure.Services;

internal class SendCloudResponseModel
{
    public bool Result { get; set; }
    public string? Message { get; set; }
    public int StatusCode { get; set; }
}