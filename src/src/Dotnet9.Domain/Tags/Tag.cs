using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Tags;

public class Tag : FullAuditedAggregateRoot<Guid>
{
    private Tag()
    {
    }

    internal Tag(Guid id, [NotNull] string name, string description) : base(id)
    {
        SetName(name);
        Description = description;
    }

    public string Name { get; private set; }

    public string Description { get; set; }

    internal Tag ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), TagConsts.MaxNameLength);
    }
}