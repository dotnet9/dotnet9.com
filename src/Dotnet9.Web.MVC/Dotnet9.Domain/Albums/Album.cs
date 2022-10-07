using Dotnet9.Core;
using Dotnet9.Domain.Shared.Albums;

namespace Dotnet9.Domain.Albums;

public class Album : EntityBase
{
    private Album()
    {
    }

    internal Album(
        int id,
        string name,
        string slug,
        string cover,
        string? description,
        int parentId,
        int index = 1,
        bool isShow = true) : base(id)
    {
        SetName(name);
        SetSlug(slug);
        Cover = cover;
        Description = description;
        ParentId = parentId;
        Index = index;
        IsShow = isShow;
    }

    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Cover { get; set; } = null!;
    public string? Description { get; set; }
    public int? ParentId { get; set; }
    public int Index { get; set; }
    public bool IsShow { get; set; }

    internal Album ChangeName(string name)
    {
        SetName(name);
        return this;
    }

    private void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), AlbumConsts.MaxNameLength);
    }

    internal Album ChangeSlug(string slug)
    {
        SetSlug(slug);
        return this;
    }

    private void SetSlug(string slug)
    {
        Slug = Check.NotNullOrWhiteSpace(slug, nameof(slug), AlbumConsts.MaxSlugLength);
    }
}