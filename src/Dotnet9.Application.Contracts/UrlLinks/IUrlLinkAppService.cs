namespace Dotnet9.Application.Contracts.UrlLinks;

public interface IUrlLinkAppService
{
    Task<List<UrlLinkDto>> ListAllAsync();
}