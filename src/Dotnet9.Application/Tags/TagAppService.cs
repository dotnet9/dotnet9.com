using AutoMapper;
using Dotnet9.Application.Contracts.Tags;
using Dotnet9.Domain.Tags;

namespace Dotnet9.Application.Tags;

public class TagAppService : ITagAppService
{
    private readonly TagManager _tagManager;
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;

    public TagAppService(ITagRepository tagRepository, TagManager tagManager, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _tagManager = tagManager;
        _mapper = mapper;
    }

    public async Task<List<TagCountDto>> GetListCountAsync()
    {
        var tags = await _tagRepository.GetListCountAsync();

        return _mapper.Map<List<TagCount>, List<TagCountDto>>(tags);
    }
}