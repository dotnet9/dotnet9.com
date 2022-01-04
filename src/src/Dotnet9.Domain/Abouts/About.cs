using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Abouts;

public class About : FullAuditedAggregateRoot<Guid>
{
    public string Details { get; set; }
}