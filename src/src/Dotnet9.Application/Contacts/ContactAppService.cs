using Dotnet9.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.Contacts;

public class ContactAppService : Dotnet9AppService, IContactAppService
{
    private readonly IRepository<Contact, Guid> _contactRepository;

    public ContactAppService(IRepository<Contact, Guid> contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<ContactDto> GetAsync(Guid id)
    {
        var contact = await _contactRepository.GetAsync(id);
        return ObjectMapper.Map<Contact, ContactDto>(contact);
    }

    public async Task<PagedResultDto<ContactDto>> GetListAsync(GetContactListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Contact.Name);
        }

        var queryable = await _contactRepository.GetQueryableAsync();

        var query = from contact in queryable.AsQueryable()
            where (contact.Name.Contains(input.Filter)
                   || contact.Email.Contains(input.Filter)
                   || contact.Subject.Contains(input.Filter)
                   || contact.Message.Contains(input.Filter))
            select contact;

        query = query
            .OrderBy(input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var queryResult = await AsyncExecuter.ToListAsync(query);

        var totalCount = await _contactRepository.GetCountAsync();

        return new PagedResultDto<ContactDto>(totalCount,
            ObjectMapper.Map<List<Contact>, List<ContactDto>>(queryResult));
    }

    [Authorize(Dotnet9Permissions.Contacts.Create)]
    public async Task<ContactDto> CreateAsync(CreateContactDto input)
    {
        var contact = ObjectMapper.Map<CreateContactDto, Contact>(input);

        await _contactRepository.InsertAsync(contact);

        return ObjectMapper.Map<Contact, ContactDto>(contact);
    }

    [Authorize(Dotnet9Permissions.Contacts.Edit)]
    public async Task UpdateAsync(Guid id, UpdateContactDto input)
    {
        var contact = await _contactRepository.GetAsync(id);

        contact.Name = input.Name;
        contact.Email = input.Email;
        contact.Subject = input.Subject;
        contact.Message = input.Message;

        await _contactRepository.UpdateAsync(contact);
    }

    [Authorize(Dotnet9Permissions.Contacts.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _contactRepository.DeleteAsync(id);
    }
}