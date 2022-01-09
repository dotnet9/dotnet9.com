using Volo.Abp.Application.Dtos;

namespace Dotnet9.Ratings;

public class GetRatingListDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}