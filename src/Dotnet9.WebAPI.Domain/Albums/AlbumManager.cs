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

    public async Task<Album> CreateAsync(Guid? id, Guid[] categoryIds, int sequenceNumber, string name, string slug,
        string cover,
        string? description, bool visible)
    {
        var isNew = id == null;
        Album? oldAlbum = null;
        if (isNew)
        {
            id = Guid.NewGuid();
        }
        else
        {
            oldAlbum = await _albumRepository.FindByIdAsync(id.Value);
            if (oldAlbum == null)
            {
                throw new Exception($"不存在的专辑: {id}");
            }
        }

        var existAlbum = await _albumRepository.FindByNameAsync(name);
        if ((isNew && existAlbum != null) ||
            (isNew == false && existAlbum != null && existAlbum.Id != oldAlbum!.Id))
        {
            throw new Exception($"存在同名的专辑: {name}");
        }

        existAlbum = await _albumRepository.FindBySlugAsync(slug);
        if ((isNew && existAlbum != null) ||
            (isNew == false && existAlbum != null && existAlbum.Id != oldAlbum!.Id))
        {
            throw new Exception($"存在相同别名的专辑: {slug}");
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

    public async Task ChangeCategoryAsync(Album album, Guid[] categoryIds)
    {
        Check.NotNull(album, nameof(album));
        if (categoryIds.IsNullOrEmpty())
        {
            throw new ArgumentNullException(nameof(categoryIds), "分类不能为空");
        }

        foreach (var categoryId in categoryIds)
        {
            var existCategory = await _categoryRepository.FindByIdAsync(categoryId);
            if (existCategory == null)
            {
                throw new Exception($"不存在的分类: {categoryId}");
            }
        }

        album.RemoveAllCategoriesExceptGivenIds(categoryIds);
        foreach (var categoryId in categoryIds)
        {
            album.AddCategory(categoryId);
        }
    }

    public async Task ChangeNameAsync(bool isNew, Album album, string newName)
    {
        Check.NotNull(album, nameof(album));
        Check.NotNullOrWhiteSpace(newName, nameof(newName), AlbumConsts.MaxNameLength, AlbumConsts.MinNameLength);

        var existAlbum = await _albumRepository.FindByNameAsync(newName);
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

        var existAlbum = await _albumRepository.FindBySlugAsync(newSlug);
        if ((isNew && existAlbum != null) ||
            (isNew == false && existAlbum != null && existAlbum.Id != album!.Id))
        {
            throw new Exception($"存在相同别名的专辑: {newSlug}");
        }

        album.ChangeSlug(newSlug);
    }
}