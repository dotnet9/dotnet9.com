namespace Dotnet9.WebAPI.Domain.Albums;

public record Album : AggregateRootEntity
{
    private Album()
    {
    }

    internal Album(
        Guid id,
        Guid categoryId,
        int sequenceNumber,
        string name,
        string slug,
        string cover,
        string? description = null,
        bool visible = false)
    {
        Id = id;
        ChangeCategoryId(categoryId);
        ChangeSequenceNumber(sequenceNumber);
        ChangeName(name);
        ChangeSlug(slug);
        ChangeCover(cover);
        ChangeDescription(description);
        ChangeVisible(visible);
    }

    public Guid CategoryId { get; private set; }
    public int SequenceNumber { get; private set; }
    public string Name { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public string Cover { get; private set; } = null!;
    public string? Description { get; private set; }
    public bool Visible { get; private set; }


    public Album ChangeCategoryId(Guid categoryId)
    {
        CategoryId = categoryId;
        return this;
    }

    public Album ChangeSequenceNumber(int sequenceNumber)
    {
        SequenceNumber = sequenceNumber;
        return this;
    }

    internal Album ChangeName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), AlbumConsts.MaxNameLength,
            AlbumConsts.MinNameLength);
        return this;
    }

    internal Album ChangeSlug(string slug)
    {
        Slug = Check.NotNullOrWhiteSpace(slug, nameof(slug), AlbumConsts.MaxSlugLength,
            AlbumConsts.MinSlugLength);
        return this;
    }

    internal Album ChangeCover(string cover)
    {
        Cover = Check.NotNullOrWhiteSpace(cover, nameof(cover), AlbumConsts.MaxCoverLength,
            AlbumConsts.MinCoverLength);
        return this;
    }

    internal Album ChangeDescription(string? description)
    {
        Description = Check.NotNullOrWhiteSpace(description, nameof(description), AlbumConsts.MaxDescriptionLength);
        return this;
    }

    internal Album ChangeVisible(bool visible)
    {
        Visible = visible;
        return this;
    }
}