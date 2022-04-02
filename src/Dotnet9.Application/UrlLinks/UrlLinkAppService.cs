using AutoMapper;
using Dotnet9.Application.Contracts.UrlLinks;
using Dotnet9.Domain.UrlLinks;

namespace Dotnet9.Application.UrlLinks;

public class UrlLinkAppService : IUrlLinkAppService
{
    private readonly IMapper _mapper;
    private readonly IUrlLinkRepository _urlLinkRepository;

    public UrlLinkAppService(IUrlLinkRepository urlLinkRepository, IMapper mapper)
    {
        _urlLinkRepository = urlLinkRepository;
        _mapper = mapper;
    }

    public async Task<List<UrlLinkDto>> ListAllAsync()
    {
        var urlLinks = await _urlLinkRepository.GetListAsync();

        return _mapper.Map<List<UrlLink>, List<UrlLinkDto>>(urlLinks);
    }
}