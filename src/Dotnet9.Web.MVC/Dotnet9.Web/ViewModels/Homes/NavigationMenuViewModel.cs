using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Application.Contracts.Tools;
using Dotnet9.Web.ViewModels.Categories;

namespace Dotnet9.Web.ViewModels.Homes;

public class NavigationMenuViewModel
{
    public List<ToolCountDto>? ToolCountDtos { get; set; }
    public List<AlbumCountDto>? AlbumCountDtos { get; set; }
    public List<CategoryForMenuViewModel>? CategoryForMenuViewModels { get; set; }
}