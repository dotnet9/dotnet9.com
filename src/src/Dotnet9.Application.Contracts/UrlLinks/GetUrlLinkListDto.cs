using Volo.Abp.Application.Dtos;

namespace Dotnet9.UrlLinks;

public class GetUrlLinkListDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}