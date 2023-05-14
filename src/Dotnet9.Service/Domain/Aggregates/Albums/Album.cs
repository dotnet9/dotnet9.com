namespace Dotnet9.Service.Domain.Aggregates.Albums;

public class Album : FullAggregateRoot<Guid, int>
{
    private Album()
    {
    }

    internal Album(
        Guid id,
        int sequenceNumber,
        string name,
        string slug,
        string cover,
        string? description = null,
        bool visible = false)
    {
        Id = id;
        ChangeSequenceNumber(sequenceNumber);
        ChangeName(name);
        ChangeSlug(slug);
        ChangeCover(cover);
        ChangeDescription(description);
        ChangeVisible(visible);
        Categories = new List<AlbumCategory>();
    }

    public int SequenceNumber { get; private set; }
    public string Name { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public string Cover { get; private set; } = null!;
    public string? Description { get; private set; }
    public bool Visible { get; private set; }

    public List<AlbumCategory>? Categories { get; }


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
        Description = Check.Length(description, nameof(description), AlbumConsts.MaxDescriptionLength);
        return this;
    }

    internal Album ChangeVisible(bool visible)
    {
        Visible = visible;
        return this;
    }

    #region category

    public void AddCategory(Guid categoryId)
    {
        if (IsInCategory(categoryId))
        {
            return;
        }

        Categories!.Add(new AlbumCategory(Id, categoryId));
    }

    public void RemoveCategory(Guid categoryId)
    {
        if (!IsInCategory(categoryId))
        {
            return;
        }

        Categories!.RemoveAll(x => x.CategoryId == categoryId);
    }

    public void RemoveAllCategoriesExceptGivenIds(ICollection<Guid> categoryIds)
    {
        Check.NotNullOrEmpty(categoryIds, nameof(categoryIds));

        Categories!.RemoveAll(x => !categoryIds.Contains(x.CategoryId));
    }

    public void RemoveAllCategories()
    {
        Categories!.RemoveAll(x => x.AlbumId == Id);
    }

    private bool IsInCategory(Guid categoryId)
    {
        return Categories!.Any(x => x.CategoryId == categoryId);
    }

    #endregion
}
