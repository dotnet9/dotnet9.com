namespace Dotnet9.Service.Domain.Aggregates.Albums;

public class AlbumManager
{
    private readonly IAlbumRepository _albumRepository;
    private readonly ICategoryRepository _categoryRepository;

    public AlbumManager(IAlbumRepository albumRepository, ICategoryRepository categoryRepository)
    {
        _albumRepository = albumRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Album> CreateAsync(Guid? id, Guid[]? categoryIds, int sequenceNumber, string name, string slug,
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

        await ChangeCategoryAsync(oldAlbum, categoryIds);
        await ChangeNameAsync(isNew, oldAlbum, name);
        await ChangeSlugAsync(isNew, oldAlbum, slug);
        return oldAlbum;
    }

    public Album CreateForSeed(int sequenceNumber, string name, string slug, string cover)
    {
        return new Album(Guid.NewGuid(), sequenceNumber, name, slug, cover, null, true);
    }

    public async Task ChangeCategoryAsync(Album album, Guid[]? categoryIds)
    {
        Check.NotNull(album, nameof(album));
        if (categoryIds.IsNullOrEmpty())
        {
            return;
        }

        foreach (Guid id in categoryIds!)
        {
            Category? existCategory = await _categoryRepository.FindByIdAsync(id);
            if (existCategory == null)
            {
                throw new Exception($"不存在的分类: {id}");
            }
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
