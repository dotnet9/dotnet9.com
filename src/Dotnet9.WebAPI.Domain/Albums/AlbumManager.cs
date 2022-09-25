namespace Dotnet9.WebAPI.Domain.Albums;

public class AlbumManager
{
    private readonly IAlbumRepository _albumRepository;
    private readonly ICategoryRepository _categoryRepository;

    public AlbumManager(IAlbumRepository albumRepository, ICategoryRepository categoryRepository)
    {
        _albumRepository = albumRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Album> CreateAsync(Guid? id, string[] categoryNames, int sequenceNumber, string name, string slug,
        string cover,
        string? description, bool visible)
    {
        bool isNew = id == null;
        Album? oldAlbum = null;
        if (isNew)
        {
            id = Guid.NewGuid();
        }
        else
        {
            oldAlbum = await _albumRepository.FindByIdAsync(id!.Value);
            if (oldAlbum == null)
            {
                throw new Exception($"不存在的专辑: {id}");
            }
        }

        if (isNew)
        {
            oldAlbum = new Album(id.Value, sequenceNumber, name, slug, cover, description, visible);
        }
        else
        {
            oldAlbum!.ChangeSequenceNumber(sequenceNumber);
            oldAlbum.ChangeCover(cover);
            oldAlbum.ChangeDescription(description);
            oldAlbum.ChangeVisible(visible);
        }

        await ChangeCategoryAsync(oldAlbum, categoryNames);
        await ChangeNameAsync(isNew, oldAlbum, name);
        await ChangeSlugAsync(isNew, oldAlbum, slug);
        return oldAlbum;
    }

    public Album CreateForSeed(string name, string slug, string cover)
    {
        return new Album(Guid.NewGuid(), 1, name, slug, cover, null, true);
    }

    public async Task ChangeCategoryAsync(Album album, string[] categoryNames)
    {
        Check.NotNull(album, nameof(album));
        if (categoryNames.IsNullOrEmpty())
        {
            throw new ArgumentNullException(nameof(categoryNames), "分类不能为空");
        }

        List<Guid> categoryIds = new List<Guid>();
        foreach (string categoryName in categoryNames)
        {
            Category? existCategory = await _categoryRepository.FindByNameAsync(categoryName);
            if (existCategory == null)
            {
                throw new Exception($"不存在的分类: {categoryName}");
            }

            categoryIds.Add(existCategory.Id);
        }

        album.RemoveAllCategoriesExceptGivenIds(categoryIds);
        foreach (Guid categoryId in categoryIds)
        {
            album.AddCategory(categoryId);
        }
    }

    public async Task ChangeNameAsync(bool isNew, Album album, string newName)
    {
        Check.NotNull(album, nameof(album));
        Check.NotNullOrWhiteSpace(newName, nameof(newName), AlbumConsts.MaxNameLength, AlbumConsts.MinNameLength);

        Album? existAlbum = await _albumRepository.FindByNameAsync(newName);
        if ((isNew && existAlbum != null) ||
            (isNew == false && existAlbum != null && existAlbum.Id != album!.Id))
        {
            throw new Exception($"存在同名的专辑: {newName}");
        }

        album.ChangeName(newName);
    }

    public async Task ChangeSlugAsync(bool isNew, Album album, string newSlug)
    {
        Check.NotNull(album, nameof(album));
        Check.NotNullOrWhiteSpace(newSlug, nameof(newSlug), AlbumConsts.MaxSlugLength, AlbumConsts.MinSlugLength);

        Album? existAlbum = await _albumRepository.FindBySlugAsync(newSlug);
        if ((isNew && existAlbum != null) ||
            (isNew == false && existAlbum != null && existAlbum.Id != album!.Id))
        {
            throw new Exception($"存在相同别名的专辑: {newSlug}");
        }

        album.ChangeSlug(newSlug);
    }

    public async Task<Album> ChangeVisible(Guid id, bool visible)
    {
        Album? oldAlbum = await _albumRepository.FindByIdAsync(id);
        if (oldAlbum == null)
        {
            throw new Exception($"不存在的专辑: {id}");
        }

        oldAlbum.ChangeVisible(visible);
        return oldAlbum;
    }
}