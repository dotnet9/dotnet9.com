using AutoMapper;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Repositories;

namespace Dotnet9.Application.Blogs;

public class BlogPostAppService : IBlogPostAppService
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IMapper _mapper;

    public BlogPostAppService(IBlogPostRepository blogPostRepository, IMapper mapper)
    {
        _blogPostRepository = blogPostRepository;
        _mapper = mapper;
    }

    public async Task<BlogPostViewModel?> FindBySlugAsync(string slug)
    {
        var blogPostWithDetails =
            await _blogPostRepository.GetBlogPostAsync(x => x.Slug == slug, x => x.Id, SortDirectionKind.Ascending);
        if (blogPostWithDetails == null) return null;

        var vm = new BlogPostViewModel
            { BlogPost = _mapper.Map<BlogPostWithDetails, BlogPostWithDetailsDto>(blogPostWithDetails) };

        var previewPost = await _blogPostRepository.GetBlogPostBriefAsync(
            x => x.CreateDate < blogPostWithDetails.CreateDate,
            x => x.CreateDate, SortDirectionKind.Descending);
        var nextPost = await _blogPostRepository.GetBlogPostBriefAsync(
            x => x.CreateDate > blogPostWithDetails.CreateDate,
            x => x.CreateDate, SortDirectionKind.Ascending);

        if (previewPost != null) vm.PreviewBlogPost = _mapper.Map<BlogPostBrief, BlogPostBriefDto>(previewPost);

        if (nextPost != null) vm.NextBlogPost = _mapper.Map<BlogPostBrief, BlogPostBriefDto>(nextPost);

        return vm;
    }

    public async Task<List<BlogPostForSitemap>> GetListBlogPostForSitemap()
    {
        var blogPosts = await _blogPostRepository.SelectAsync();
        return _mapper.Map<List<BlogPost>, List<BlogPostForSitemap>>(blogPosts);
    }
}