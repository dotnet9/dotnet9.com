using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Application.Contracts.Tools;
using Dotnet9.Web.ViewModels;
using Dotnet9.Web.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.ViewComponents.About;

public class MenuHorizontal : ViewComponent
{
    private readonly IAlbumAppService _albumAppService;
    private readonly ICategoryAppService _categoryAppService;

    public MenuHorizontal(IAlbumAppService albumAppService, ICategoryAppService categoryAppService)
    {
        _albumAppService = albumAppService;
        _categoryAppService = categoryAppService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var vm = new NavigationMenuViewModel
        {
            ToolCountDtos = new List<ToolCountDto>
            {
                new() {Name = "时间戳", RelativeUrl = "/tools/timestamp"},
                new() {Name = "Icon转换", RelativeUrl = "/tools/icon"},
                new() {Name = "正则表达式", RelativeUrl = "/tools/regular"}
            },
            AlbumCountDtos = await _albumAppService.GetListCountAsync(),
            CategoryForMenuViewModels = ReadChildren(await _categoryAppService.ListAllAsync(), -1)
        };
        return await Task.FromResult(View(vm));
    }

    private List<CategoryForMenuViewModel>? ReadChildren(List<CategoryCountDto> sourceCategoryCountDtos, int parentId)
    {
        var children = sourceCategoryCountDtos.FindAll(x => x.ParentId == parentId);
        if (!children.Any())
        {
            return null;
        }

        var categoryForMenuViewModels = new List<CategoryForMenuViewModel>();
        foreach (var categoryCountDto in children)
        {
            var child = new CategoryForMenuViewModel() {Name = categoryCountDto.Name, Slug = categoryCountDto.Slug};
            categoryForMenuViewModels.Add(child);
            child.Children = ReadChildren(sourceCategoryCountDtos, categoryCountDto.Id);
        }

        return categoryForMenuViewModels;
    }
}