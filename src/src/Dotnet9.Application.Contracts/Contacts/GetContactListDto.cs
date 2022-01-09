using Volo.Abp.Application.Dtos;

namespace Dotnet9.Contacts;

public class GetContactListDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}