namespace Dotnet9.Service.Domain.Aggregates.Categories;

public class Category : FullAggregateRoot<Guid, int>
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

    public Category ChangeName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), CategoryConsts.MaxNameLength,
            CategoryConsts.MinNameLength);
        return this;
    }

    public Category ChangeSlug(string slug)
    {
        Slug = Check.NotNullOrWhiteSpace(slug, nameof(slug), CategoryConsts.MaxSlugLength,
            CategoryConsts.MinSlugLength);
        return this;
    }

    public Category ChangeCover(string cover)
    {
        Cover = Check.NotNullOrWhiteSpace(cover, nameof(cover), CategoryConsts.MaxCoverLength,
            CategoryConsts.MinCoverLength);
        return this;
    }

    public Category ChangeDescription(string? description)
    {
        Description = Check.Length(description, nameof(description), CategoryConsts.MaxDescriptionLength);
        return this;
    }

    public Category ChangeVisible(bool visible)
    {
        Visible = visible;
        return this;
    }

    public Category ChangeParentId(Guid? parentId)
    {
        ParentId = parentId;
        return this;
    }
}
