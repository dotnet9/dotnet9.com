using AutoMapper;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Domain.Blogs;

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

    public async Task<BlogPostWithDetailsDto?> FindBySlugAsync(string slug)
    {
        var blogPostWithDetails = await _blogPostRepository.FindBySlugAsync(slug);
        return blogPostWithDetails == null
            ? null
            : _mapper.Map<BlogPostWithDetails, BlogPostWithDetailsDto>(blogPostWithDetails);
    }
}