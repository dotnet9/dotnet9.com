using System;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Abouts;

public class AboutDto : AuditedEntityDto<Guid>
{
    public string Details { get; set; }
}