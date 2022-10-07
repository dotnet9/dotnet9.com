namespace Dotnet9.Application.Contracts.UrlLinks;

public interface IUrlLinkAppService
{
    Task<List<UrlLinkDto>> ListAllAsync();
    Task<PageDto<UrlLinkDto>> AdminListAsync(UrlLinkRequest request);
}