using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Application.Contracts.Tools;
using Dotnet9.Web.Caches;
using Dotnet9.Web.ViewModels.Categories;
using Dotnet9.Web.ViewModels.Homes;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.ViewComponents.Abouts;

public class MenuHorizontal : ViewComponent
{
    private readonly IAlbumAppService _albumAppService;
    private readonly ICacheService _cacheService;
    private readonly ICategoryAppService _categoryAppService;

    public MenuHorizontal(IAlbumAppService albumAppService, ICategoryAppService categoryAppService,
        ICacheService cacheService)
    {
        _albumAppService = albumAppService;
        _categoryAppService = categoryAppService;
        _cacheService = cacheService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cacheKey = $"{nameof(MenuHorizontal)}";
        var cacheData = await _cacheService.GetAsync<NavigationMenuViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        cacheData = new NavigationMenuViewModel
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

        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }

    private List<CategoryForMenuViewModel>? ReadChildren(List<CategoryCountDto> sourceCategoryCountDtos, int parentId)
    {
        var children = sourceCategoryCountDtos.FindAll(x => x.ParentId == parentId);
        if (!children.Any()) return null;

        var categoryForMenuViewModels = new List<CategoryForMenuViewModel>();
        foreach (var categoryCountDto in children)
        {
            var child = new CategoryForMenuViewModel {Name = categoryCountDto.Name, Slug = categoryCountDto.Slug};
            categoryForMenuViewModels.Add(child);
            child.Children = ReadChildren(sourceCategoryCountDtos, categoryCountDto.Id);
        }

        return categoryForMenuViewModels;
    }
}