using AutoMapper;
using Dotnet9.Application.Contracts.Caches;
using Dotnet9.Application.Contracts.Tags;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.ViewComponents.Tags;

public class TagCloud : ViewComponent
{
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;
    private readonly ITagAppService _tagAppService;

    public TagCloud(ITagAppService tagAppService, ICacheService cacheService, IMapper mapper)
    {
        _tagAppService = tagAppService;
        _cacheService = cacheService;
        _mapper = mapper;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cacheKey = $"{nameof(TagCloud)}-{nameof(InvokeAsync)}";
        var cacheData = await _cacheService.GetAsync<TagViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        cacheData = await _tagAppService.GetTagAsync(null);
        if (cacheData == null) return View();

        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }
}