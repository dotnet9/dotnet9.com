using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dotnet9.Contacts;

public interface IContactAppService : IApplicationService
{
    Task<ContactDto> GetAsync(Guid id);

    Task<PagedResultDto<ContactDto>> GetListAsync(GetContactListDto input);

    Task<ContactDto> CreateAsync(CreateContactDto input);

    Task UpdateAsync(Guid id, UpdateContactDto input);

    Task DeleteAsync(Guid id);
}