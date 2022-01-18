using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Blogs;

public class GetBlogPostListDto : PagedAndSortedResultRequestDto
{
    [CanBeNull] public string Album { get; set; }

    [CanBeNull] public string Category { get; set; }

    [CanBeNull] public string Tag { get; set; }

    public string Filter { get; set; }
}