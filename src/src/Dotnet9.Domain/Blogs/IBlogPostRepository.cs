using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.Blogs;

public interface IBlogPostRepository : IRepository<BlogPost, Guid>
{
    Task<BlogPost> FindByTitleAsync(string title);

    Task<BlogPost> FindBySlugAsync(string slug);

    Task<List<BlogPost>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null);
}