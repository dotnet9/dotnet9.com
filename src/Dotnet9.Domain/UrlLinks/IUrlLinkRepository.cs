using Dotnet9.Domain.Repositories;

namespace Dotnet9.Domain.UrlLinks;

public interface IUrlLinkRepository : IRepository<UrlLink>
{
    Task<UrlLink?> FindByNameAsync(string name);
    Task<UrlLink?> FindByUrlAsync(string url);
    Task<List<UrlLink>> GetListAsync();
}