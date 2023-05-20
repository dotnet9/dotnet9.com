namespace Dotnet9.RazorPages.Services;

public interface ISystemClientService
{
    Task<SiteInfoDto?> GetSiteInfoAsync();
}