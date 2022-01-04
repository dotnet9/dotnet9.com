using JetBrains.Annotations;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.UrlLinks;

public class UrlLink : FullAuditedAggregateRoot<Guid>
{
    [NotNull] public virtual string Name { get; set; }

    [NotNull] public virtual string Url { get; set; }

    [NotNull] public virtual string Description { get; set; }

    public int Index { get; set; }
}