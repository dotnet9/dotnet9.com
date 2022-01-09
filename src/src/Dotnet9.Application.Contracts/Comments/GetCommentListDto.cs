using Volo.Abp.Application.Dtos;

namespace Dotnet9.Comments;

public class GetCommentListDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}