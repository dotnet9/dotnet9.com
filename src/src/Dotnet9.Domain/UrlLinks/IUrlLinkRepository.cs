using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.UrlLinks;

public interface IUrlLinkRepository : IRepository<UrlLink, Guid>
{
    Task<UrlLink> FindByNameAsync(string name);

    Task<UrlLink> FindByUrlAsync(string url);

    Task<List<UrlLink>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);

    Task<int> CountAsync(string filter = null);
}