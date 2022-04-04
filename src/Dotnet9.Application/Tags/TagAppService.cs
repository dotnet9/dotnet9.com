using AutoMapper;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Application.Contracts.Tags;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Tags;

namespace Dotnet9.Application.Tags;

public class TagAppService : ITagAppService
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IMapper _mapper;
    private readonly TagManager _tagManager;
    private readonly ITagRepository _tagRepository;

    public TagAppService(ITagRepository tagRepository, TagManager tagManager, IBlogPostRepository blogPostRepository,
        IMapper mapper)
    {
        _tagRepository = tagRepository;
        _tagManager = tagManager;
        _blogPostRepository = blogPostRepository;
        _mapper = mapper;
    }

    public async Task<List<TagCountDto>> GetListCountAsync()
    {
        var tags = await _tagRepository.GetListCountAsync();

        return _mapper.Map<List<TagCount>, List<TagCountDto>>(tags);
    }


    public async Task<List<BlogPostWithDetailsDto>?> GetBlogPostListAsync(string tagName)
    {
        var blogPostWithDetailsLists = await _blogPostRepository.GetBlogPostListByTagNameAsync(tagName);
        return blogPostWithDetailsLists == null
            ? null
            : _mapper.Map<List<BlogPostWithDetails>, List<BlogPostWithDetailsDto>>(blogPostWithDetailsLists);
    }
}