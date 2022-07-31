namespace IdentityService.Infrastructure.Services;

internal class SendCloudResponseModel
{
    public bool Result { get; set; }

    public string? Message { get; set; }

    public int StatusCode { get; set; }
}