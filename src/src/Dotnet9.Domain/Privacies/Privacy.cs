using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Privacies;

public class Privacy : FullAuditedAggregateRoot<Guid>
{
    [NotNull] public string Details { get; set; }
}