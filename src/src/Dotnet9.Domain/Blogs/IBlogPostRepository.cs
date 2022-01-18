using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.Blogs;

public interface IBlogPostRepository : IRepository<BlogPost, Guid>
{
    Task<BlogPostWithDetails> FindByTitleAsync(string title);

    Task<BlogPostWithDetails> FindBySlugAsync([NotNull] string slug);

    Task<List<BlogPostWithDetails>> GetListAsync(int skipCount, int maxResultCount, string sorting,
        [CanBeNull] string filter, [CanBeNull] string album, [CanBeNull] string category, [CanBeNull] string tag);

    Task<int> GetCountAsync([CanBeNull] string filter, [CanBeNull] string album, [CanBeNull] string category,
        [CanBeNull] string tag);
}