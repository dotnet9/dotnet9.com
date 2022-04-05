using AutoMapper;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Core;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Repositories;
using Dotnet9.Web.ViewModels.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class BlogPostController : Controller
{
    private readonly IBlogPostAppService _blogPostAppService;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IMapper _mapper;

    public BlogPostController(IBlogPostAppService blogPostAppService, IBlogPostRepository blogPostRepository,
        IMapper mapper)
    {
        _blogPostAppService = blogPostAppService;
        _blogPostRepository = blogPostRepository;
        _mapper = mapper;
    }

    [Route("{year}/{month}/{slug?}")]
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

    [Route("recommend")]
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
}