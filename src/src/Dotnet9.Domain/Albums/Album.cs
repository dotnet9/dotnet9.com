using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Albums;

public class Album : FullAuditedAggregateRoot<Guid>
{
    [NotNull] public string Name { get; set; }
    
    [NotNull] public virtual string CoverImageUrl { get; protected set; }

    public string Description { get; set; }
}