namespace Dotnet9.BlazorWebApp.Services;

public interface ISystemClientService
{
    Task<SiteInfoDto?> GetSiteInfoAsync();
}