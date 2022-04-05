using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Core;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Categories;
using Dotnet9.Domain.Repositories;
using Dotnet9.Web.Caches;
using Dotnet9.Web.ViewModels.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class BlogPostController : Controller
{
    private readonly IBlogPostAppService _blogPostAppService;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICacheService _cacheService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public BlogPostController(IBlogPostAppService blogPostAppService, IBlogPostRepository blogPostRepository,
        ICategoryRepository categoryRepository, IMapper mapper, ICacheService cacheService)
    {
        _blogPostAppService = blogPostAppService;
        _blogPostRepository = blogPostRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        CacheHelper.Cache = _cacheService = cacheService;
    }

    [Route("/{year}/{month}/{slug?}")]
    public async Task<IActionResult> Index(int year, int month, string? slug)
    {
        if (slug.IsNullOrWhiteSpace()) return NotFound();

        var cacheKey = $"{nameof(BlogPostController)}-{nameof(Index)}-{year}-{month}-{slug}";
        var cacheData = await _cacheService.GetAsync<BlogPostViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        var blogPostWithDetailsDto = await _blogPostAppService.FindBySlugAsync(slug!);
        if (blogPostWithDetailsDto == null) return NotFound();

        cacheData = new BlogPostViewModel
        {
            BlogPost = blogPostWithDetailsDto
        };

        await _cacheService.ReplaceAsync(cacheKey, cacheData);

        return View(cacheData);
    }

    [Route("/recommend")]
    public async Task<IActionResult> Recommend()
    {
        var cacheKey = $"{nameof(BlogPostController)}-{nameof(Recommend)}";
        var cacheData = await _cacheService.GetAsync<RecommendViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        cacheData = new RecommendViewModel();

        var recommend =
            await _blogPostRepository.SelectBlogPostAsync(x => x.InBanner, x => x.CreateDate,
                SortDirectionKind.Descending);

        if (recommend != null)
            cacheData.BlogPostsForRecommend =
                _mapper.Map<List<BlogPostWithDetails>, List<BlogPostWithDetailsDto>>(recommend);

        await _cacheService.ReplaceAsync(cacheKey, cacheData);
        return await Task.FromResult(View(cacheData));
    }

    [Route("/latest")]
    public async Task<IActionResult> LoadLatest(string kind = "", int page = 1)
    {
        var cacheKey = $"{nameof(BlogPostController)}-{nameof(LoadLatest)}-{kind}-{page}";
        var cacheData = await _cacheService.GetAsync<LatestViewModel>(cacheKey);
        if (cacheData != null) return PartialView(cacheData);

        var loadKind = LoadMoreKind.Dotnet;
        if (Enum.TryParse(typeof(LoadMoreKind), kind, out var enumKind))
            loadKind = (LoadMoreKind) Enum.Parse(typeof(LoadMoreKind), kind);

        Expression<Func<BlogPost, bool>> whereLambda = x => x.Id > 0;
        Dictionary<LoadMoreKind, string> kindKeys = new()
        {
            {LoadMoreKind.Dotnet, "dotnet"},
            {LoadMoreKind.Front, "Large-front-end"},
            {LoadMoreKind.Database, "database"},
            {LoadMoreKind.MoreLanguage, "more-language"},
            {LoadMoreKind.Course, "course"},
            {LoadMoreKind.Other, "other"}
        };
        if (kindKeys.ContainsKey(loadKind))
        {
            var categoryKey = kindKeys[loadKind];
            var dotnetCategoryIds =
                (await _categoryRepository.SelectAsync(x => x.Slug.StartsWith(categoryKey))).Select(x => x.Id);
            whereLambda = x =>
                x.Categories != null && x.Categories.Any(d => dotnetCategoryIds.Contains(d.CategoryId));
        }

        var latest = await _blogPostRepository.SelectBlogPostAsync(8, page, whereLambda, x => x.CreateDate,
            SortDirectionKind.Descending);
        if (!latest.Item1.Any()) return Json("");
        cacheData = new LatestViewModel
        {
            BlogPosts = _mapper.Map<List<BlogPostWithDetails>, List<BlogPostWithDetailsDto>>(latest.Item1)
        };
        await _cacheService.ReplaceAsync(cacheKey, cacheData);

        return PartialView(cacheData);
    }


    [Route("/q")]
    public async Task<IActionResult> Query(string? s)
    {
        return await Task.FromResult(View(new QueryViewModel {Query = s, PageIndex = 1}));
    }


    [Route("/qs")]
    public async Task<IActionResult> LoadQuery(string? s, int p = 1)
    {
        var cacheKey = $"{nameof(BlogPostController)}-{nameof(LoadQuery)}-{s}-{p}";
        var cacheData = await _cacheService.GetAsync<QueryViewModel>(cacheKey);
        if (cacheData != null) return PartialView(cacheData);

        Expression<Func<BlogPost, bool>> whereLambda = x => x.Id > 0;
        if (!s.IsNullOrWhiteSpace())
        {
            var queryStr = WebUtility.UrlDecode(s);
            whereLambda = x => x.Title.Contains(queryStr!) || x.Content.Contains(queryStr!);
        }

        var queryResult = await _blogPostRepository.SelectBlogPostAsync(8, p, whereLambda, x => x.CreateDate,
            SortDirectionKind.Descending);

        if (queryResult.Item1.Any())
        {
            cacheData = new QueryViewModel
            {
                Query = s,
                PageIndex = p,
                BlogPosts = _mapper.Map<List<BlogPostWithDetails>, List<BlogPostWithDetailsDto>>(queryResult.Item1)
            };

            await _cacheService.ReplaceAsync(cacheKey, cacheData);

            return PartialView(cacheData);
        }

        return Json("");
    }
}