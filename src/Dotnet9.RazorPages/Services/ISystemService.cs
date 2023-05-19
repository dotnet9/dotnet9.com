namespace Dotnet9.RazorPages.Services;

public interface ISystemService
{
    Task<SiteInfo?> GetSiteInfoAsync();
}
