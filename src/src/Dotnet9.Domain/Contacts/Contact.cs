using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Contacts;

public class Contact : FullAuditedAggregateRoot<Guid>
{
    [NotNull] public string Name { get; set; }

    [NotNull] public string Email { get; set; }
    
    [NotNull] public string Subject { get; set; }

    [NotNull] public string Message { get; set; }
}