using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Albums;

public class Album : FullAuditedAggregateRoot<Guid>
{
    private Album()
    {
    }


    internal Album(Guid id, [NotNull] string name, string coverImageUrl, string description) : base(id)
    {
        SetName(name);
        CoverImageUrl = coverImageUrl;
        Description = description;
    }

    internal Album ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), AlbumConsts.MaxNameLength);
    }

    [NotNull] public string Name { get; set; }

    public string CoverImageUrl { get; set; }

    public string Description { get; set; }
}