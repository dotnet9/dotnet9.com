namespace Dotnet9.WebShare.Services;

public interface ISystemClientService
{
    Task<SiteInfoDto?> GetSiteInfoAsync();
}