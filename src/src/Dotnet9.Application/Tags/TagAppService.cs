using Dotnet9.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.Tags;

[Authorize(Dotnet9Permissions.Tags.Default)]
public class TagAppService : Dotnet9AppService, ITagAppService
{
    private readonly ITagRepository _tagRepository;
    private readonly TagManager _tagManager;

    public TagAppService(ITagRepository tagRepository, TagManager tagManager)
    {
        _tagRepository = tagRepository;
        _tagManager = tagManager;
    }

    public async Task<TagDto> GetAsync(Guid id)
    {
        var tag = await _tagRepository.GetAsync(id);
        return ObjectMapper.Map<Tag, TagDto>(tag);
    }

    public async Task<PagedResultDto<TagDto>> GetListAsync(GetTagListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Tag.Name);
        }

        var tags = await _tagRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting,
            input.Filter);

        var totalCount = input.Filter == null
            ? await _tagRepository.CountAsync()
            : await _tagRepository.CountAsync(tag => tag.Name.Contains(input.Filter));

        return new PagedResultDto<TagDto>(totalCount, ObjectMapper.Map<List<Tag>, List<TagDto>>(tags));
    }

    [Authorize(Dotnet9Permissions.Tags.Create)]
    public async Task<TagDto> CreateAsync(CreateTagDto input)
    {
        var tag = await _tagManager.CreateAsync(input.Name, input.Description);

        await _tagRepository.InsertAsync(tag);

        return ObjectMapper.Map<Tag, TagDto>(tag);
    }

    [Authorize(Dotnet9Permissions.Tags.Edit)]
    public async Task UpdateAsync(Guid id, UpdateTagDto input)
    {
        var tag = await _tagRepository.GetAsync(id);

        if (tag.Name != input.Name)
        {
            await _tagManager.ChangeNameAsync(tag, input.Name);
        }

        tag.Description = input.Description;

        await _tagRepository.UpdateAsync(tag);
    }

    [Authorize(Dotnet9Permissions.Tags.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _tagRepository.DeleteAsync(id);
    }
}