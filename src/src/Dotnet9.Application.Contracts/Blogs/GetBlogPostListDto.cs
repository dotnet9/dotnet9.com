using Volo.Abp.Application.Dtos;

namespace Dotnet9.Blogs;

public class GetBlogPostListDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}