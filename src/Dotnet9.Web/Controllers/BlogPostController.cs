using System.Linq.Expressions;
using AutoMapper;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Core;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Categories;
using Dotnet9.Domain.Repositories;
using Dotnet9.Web.ViewModels.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class BlogPostController : Controller
{
    private readonly IBlogPostAppService _blogPostAppService;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public BlogPostController(IBlogPostAppService blogPostAppService, IBlogPostRepository blogPostRepository,
        ICategoryRepository categoryRepository, IMapper mapper)
    {
        _blogPostAppService = blogPostAppService;
        _blogPostRepository = blogPostRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [Route("/{year}/{month}/{slug?}")]
    public async Task<IActionResult> Index(int year, int month, string? slug)
    {
        if (slug.IsNullOrWhiteSpace()) return NotFound();

        var blogPostWithDetailsDto = await _blogPostAppService.FindBySlugAsync(slug!);
        if (blogPostWithDetailsDto == null) return NotFound();

        var vm = new BlogPostViewModel
        {
            BlogPost = blogPostWithDetailsDto
        };
        return View(vm);
    }

    [Route("/recommend")]
    public async Task<IActionResult> Recommend()
    {
        var vm = new RecommendViewModel();
        var recommend =
            await _blogPostRepository.SelectBlogPostAsync(x => x.InBanner, x => x.CreateDate,
                SortDirectionKind.Descending);
        if (recommend != null)
            vm.BlogPostsForRecommend =
                _mapper.Map<List<BlogPostWithDetails>, List<BlogPostWithDetailsDto>>(recommend);

        return await Task.FromResult(View(vm));
    }

    [Route("/latest")]
    public async Task<IActionResult> LoadLatest(string kind = "", int page = 1)
    {
        var loadKind = LoadMoreKind.Dotnet;
        if (Enum.TryParse(typeof(LoadMoreKind), kind, out var enumKind)) loadKind = (LoadMoreKind) enumKind;

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
        if (latest.Item1.Any())
        {
            var vm = new LatestViewModel
            {
                BlogPosts = _mapper.Map<List<BlogPostWithDetails>, List<BlogPostWithDetailsDto>>(latest.Item1)
            };

            return PartialView(vm);
        }

        return Json("");
    }
}