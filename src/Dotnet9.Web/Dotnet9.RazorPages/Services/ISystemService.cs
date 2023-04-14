using Dotnet9.Contracts.Dto;

namespace Dotnet9.RazorPages.Services;

public interface ISystemService
{
    Task<SiteInfo?> GetSiteInfoAsync();
}
