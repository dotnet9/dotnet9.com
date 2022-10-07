using AutoMapper;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Application.Contracts.Caches;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Repositories;
using Dotnet9.Web.ViewModels.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.ViewComponents.Blogs;

public class RecommendSidebar : ViewComponent
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;

    public RecommendSidebar(IBlogPostRepository blogPostRepository, ICacheService cacheService, IMapper mapper)
    {
        _blogPostRepository = blogPostRepository;
        _cacheService = cacheService;
        _mapper = mapper;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        const string cacheKey = $"{nameof(RecommendSidebar)}-{nameof(InvokeAsync)}";
        var cacheData = await _cacheService.GetAsync<LatestViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        cacheData = new LatestViewModel();

        var recommend = await _blogPostRepository.SelectBlogPostBriefAsync(8, 1, x => x.InBanner, x => x.CreateDate,
            SortDirectionKind.Descending);
        cacheData.BlogPosts =
            _mapper.Map<List<BlogPostBrief>, List<BlogPostBriefDto>>(recommend.Item1);

        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }
}