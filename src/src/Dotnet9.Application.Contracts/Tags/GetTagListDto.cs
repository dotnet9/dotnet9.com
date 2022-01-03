using Volo.Abp.Application.Dtos;

namespace Dotnet9.Tags;

public class GetTagListDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}