using System;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Contacts;

public class ContactDto : EntityDto<Guid>
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Subject { get; set; }

    public string Message { get; set; }
}