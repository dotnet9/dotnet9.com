using Dotnet9.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.UrlLinks;

public class UrlLinkAppService : Dotnet9AppService, IUrlLinkAppService
{
    private readonly IUrlLinkRepository _urlLinkRepository;
    private readonly UrlLinkManager _urlLinkManager;

    public UrlLinkAppService(IUrlLinkRepository urlLinkRepository, UrlLinkManager urlLinkManager)
    {
        _urlLinkRepository = urlLinkRepository;
        _urlLinkManager = urlLinkManager;
    }

    public async Task<UrlLinkDto> GetAsync(Guid id)
    {
        var urlLink = await _urlLinkRepository.GetAsync(id);
        return ObjectMapper.Map<UrlLink, UrlLinkDto>(urlLink);
    }

    public async Task<IReadOnlyList<UrlLinkDto>> GetListAsync()
    {
        return (await GetListAsync(new GetUrlLinkListDto { MaxResultCount = int.MaxValue })).Items;
    }

    public async Task<PagedResultDto<UrlLinkDto>> GetListAsync(GetUrlLinkListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(UrlLink.Name);
        }

        var urlLinks = await _urlLinkRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting,
            input.Filter);

        var totalCount = input.Filter == null
            ? await _urlLinkRepository.CountAsync()
            : await _urlLinkRepository.CountAsync(
                urlLink => urlLink.Name.Contains(input.Filter)
                           || urlLink.Url.Contains(input.Filter));

        return new PagedResultDto<UrlLinkDto>(totalCount,
            ObjectMapper.Map<List<UrlLink>, List<UrlLinkDto>>(urlLinks));
    }

    [Authorize(Dotnet9Permissions.UrlLinks.Create)]
    public async Task<UrlLinkDto> CreateAsync(CreateUrlLinkDto input)
    {
        var urlLink = await _urlLinkManager.CreateAsync(
            input.Name,
            input.Url,
            input.Description,
            input.Index);

        await _urlLinkRepository.InsertAsync(urlLink);

        return ObjectMapper.Map<UrlLink, UrlLinkDto>(urlLink);
    }

    [Authorize(Dotnet9Permissions.UrlLinks.Edit)]
    public async Task UpdateAsync(Guid id, UpdateUrlLinkDto input)
    {
        var urlLink = await _urlLinkRepository.GetAsync(id);

        if (urlLink.Name != input.Name)
        {
            await _urlLinkManager.ChangeNameAsync(urlLink, input.Name);
        }

        if (urlLink.Url != input.Url)
        {
            await _urlLinkManager.ChangeUrlAsync(urlLink, input.Url);
        }

        urlLink.Description = input.Description;
        urlLink.Index = input.Index;

        await _urlLinkRepository.UpdateAsync(urlLink);
    }

    [Authorize(Dotnet9Permissions.UrlLinks.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _urlLinkRepository.DeleteAsync(id);
    }
}