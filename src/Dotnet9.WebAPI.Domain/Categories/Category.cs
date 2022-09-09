namespace Dotnet9.WebAPI.Domain.Categories;

public record Category : AggregateRootEntity
{
    private Category()
    {
    }

    internal Category(
        Guid id,
        int sequenceNumber,
        string name,
        string slug,
        string cover,
        string? description = null,
        bool visible = false,
        Guid? parentId = null)
    {
        Id = id;
        ChangeSequenceNumber(sequenceNumber);
        ChangeName(name);
        ChangeSlug(slug);
        ChangeCover(cover);
        ChangeDescription(description);
        ChangeVisible(visible);
        ChangeParentId(parentId);
    }

    public int SequenceNumber { get; private set; }
    public string Name { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public string Cover { get; private set; } = null!;
    public string? Description { get; private set; }
    public Guid? ParentId { get; private set; }
    public bool Visible { get; private set; }


    public Category ChangeSequenceNumber(int sequenceNumber)
    {
        SequenceNumber = sequenceNumber;
        return this;
    }

    internal Category ChangeName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), CategoryConsts.MaxNameLength,
            CategoryConsts.MinNameLength);
        return this;
    }

    internal Category ChangeSlug(string slug)
    {
        Slug = Check.NotNullOrWhiteSpace(slug, nameof(slug), CategoryConsts.MaxSlugLength,
            CategoryConsts.MinSlugLength);
        return this;
    }

    internal Category ChangeCover(string cover)
    {
        Cover = Check.NotNullOrWhiteSpace(cover, nameof(cover), CategoryConsts.MaxCoverLength,
            CategoryConsts.MinCoverLength);
        return this;
    }

    internal Category ChangeDescription(string? description)
    {
        Description = Check.NotNullOrWhiteSpace(description, nameof(description), CategoryConsts.MaxDescriptionLength);
        return this;
    }

    internal Category ChangeVisible(bool visible)
    {
        Visible = visible;
        return this;
    }

    internal Category ChangeParentId(Guid? parentId)
    {
        ParentId = parentId;
        return this;
    }
}