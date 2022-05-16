using AutoMapper;
using Dotnet9.Application.Contracts;
using Dotnet9.Application.Contracts.UrlLinks;
using Dotnet9.Domain.UrlLinks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Dotnet9.Web.Controllers;

[Route("api/urllink")]
[Authorize]
[ApiController]
public class UrlLinkController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUrlLinkAppService _urlLinkAppService;
    private readonly IUrlLinkRepository _urlLinkRepository;

    public UrlLinkController(IUrlLinkAppService urlLinkAppService, IUrlLinkRepository urlLinkRepository, IMapper mapper)
    {
        _urlLinkAppService = urlLinkAppService;
        _urlLinkRepository = urlLinkRepository;
        _mapper = mapper;
    }

    [HttpGet("list")]
    public async Task<PageDto<UrlLinkDto>> List([FromQuery] UrlLinkRequest request)
    {
        return await _urlLinkAppService.AdminListAsync(request);
    }

    [HttpDelete("delete")]
    public async Task Delete(int id)
    {
        await _urlLinkRepository.DeleteAsync(x => x.Id == id);
    }

    [HttpGet("get")]
    public async Task<UrlLinkDto?> Get(int id)
    {
        var urlLink = await _urlLinkRepository.GetAsync(x => x.Id == id);
        return urlLink == null ? null : _mapper.Map<UrlLink, UrlLinkDto>(urlLink);
    }

    [HttpPost("addOrUpdate")]
    public async Task AddOrUpdate(AddOrUpdateUrlLinkDto request)
    {
        var urlLink = await _urlLinkRepository.GetAsync(x => x.Id == request.Id);
        if (urlLink == null)
        {
            var urlLinkForDb = _mapper.Map<AddOrUpdateUrlLinkDto, UrlLink>(request);
            urlLinkForDb.CreateDate = DateTimeOffset.Now;
            await _urlLinkRepository.InsertAsync(urlLinkForDb);
        }
        else
        {
            _mapper.Map(request, urlLink, typeof(AddOrUpdateUrlLinkDto), typeof(UrlLink));
            urlLink.UpdateDate = DateTimeOffset.Now;
            await _urlLinkRepository.UpdateAsync(urlLink);
        }
    }
}