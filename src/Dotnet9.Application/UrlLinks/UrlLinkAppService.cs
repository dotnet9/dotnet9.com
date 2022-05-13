using AutoMapper;
using Dotnet9.Application.Contracts;
using Dotnet9.Application.Contracts.UrlLinks;
using Dotnet9.Domain.Repositories;
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

    public async Task<PageDto<UrlLinkDto>> AdminListAsync(UrlLinkRequest request)
    {
        var (urlLinkList, total) = await _urlLinkRepository.SelectAsync(request.Size, request.Index, x => x.Id > 0,
            x => x.CreateDate,
            SortDirectionKind.Descending);
        var urlLinkListDto = _mapper.Map<List<UrlLink>, List<UrlLinkDto>>(urlLinkList);

        return PageDto<UrlLinkDto>.Success(urlLinkListDto, total);
    }
}