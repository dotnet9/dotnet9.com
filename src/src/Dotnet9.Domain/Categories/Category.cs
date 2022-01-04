using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Categories;

public class Category : FullAuditedAggregateRoot<Guid>
{
    [NotNull] public string Name { get; set; }

    public virtual string CoverImageUrl { get; protected set; }

    public string Description { get; set; }
}