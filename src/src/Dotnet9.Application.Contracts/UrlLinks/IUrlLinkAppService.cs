using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dotnet9.UrlLinks;

public interface IUrlLinkAppService : IApplicationService
{
    Task<UrlLinkDto> GetAsync(Guid id);
    Task<IReadOnlyList<UrlLinkDto>> GetListAsync();

    Task<PagedResultDto<UrlLinkDto>> GetListAsync(GetUrlLinkListDto input);

    Task<UrlLinkDto> CreateAsync(CreateUrlLinkDto input);

    Task UpdateAsync(Guid id, UpdateUrlLinkDto input);

    Task DeleteAsync(Guid id);
}