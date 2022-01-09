using Volo.Abp.Application.Dtos;

namespace Dotnet9.Albums;

public class GetAlbumListDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}