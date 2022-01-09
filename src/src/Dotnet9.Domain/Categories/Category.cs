using System;
using System.Diagnostics.CodeAnalysis;
using Dotnet9.Albums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Categories;

public class Category : FullAuditedAggregateRoot<Guid>
{
    private Category()
    {
    }


    internal Category(
        Guid id,
        [NotNull] string name,
        string coverImageUrl,
        string description) : base(id)
    {
        SetName(name);
        CoverImageUrl = coverImageUrl;
        Description = description;
    }

    internal Category ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), CategoryConsts.MaxNameLength);
    }

    [NotNull] public string Name { get; set; }

    public string CoverImageUrl { get; set; }

    public string Description { get; set; }
}