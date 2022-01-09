using Volo.Abp.Application.Dtos;

namespace Dotnet9.Categories;

public class GetCategoryListDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}