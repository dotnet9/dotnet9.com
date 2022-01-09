using System;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Privacies;

public class PrivacyDto : AuditedEntityDto<Guid>
{
    public string Details { get; set; }
}