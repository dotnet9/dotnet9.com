using System.Net;
using AutoMapper;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Application.Contracts.Tags;
using Dotnet9.Core;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Repositories;
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

    public async Task<TagViewModel?> GetTagAsync(string? tagName)
    {
        var vm = new TagViewModel();

        if (tagName.IsNullOrWhiteSpace())
        {
            var tags = await _tagRepository.GetListCountAsync();

            vm.Tags = _mapper.Map<List<TagCount>, List<TagCountDto>>(tags);
        }
        else
        {
            var factName = WebUtility.UrlDecode(tagName);
            vm.TagName = tagName;
            var tag = await _tagRepository.FindByNameAsync(factName!);
            if (tag == null)
            {
                return vm;
            }

            var blogPosts =
                await _blogPostRepository.SelectBlogPostBriefAsync(x =>
                        x.Tags != null && x.Tags.Any(d => d.TagId == tag.Id),
                    x => x.CreateDate, SortDirectionKind.Descending);
            if (blogPosts != null)
            {
                vm.BlogPosts = _mapper.Map<List<BlogPostBrief>, List<BlogPostBriefDto>>(blogPosts);
            }
        }

        return vm;
    }
}