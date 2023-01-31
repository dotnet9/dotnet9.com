namespace Dotnet9.WebAPI.Infrastructure.Albums;

internal class AlbumRepository : IAlbumRepository
{
    private readonly Dotnet9DbContext _dbContext;

    public AlbumRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> DeleteAsync(Guid[] ids)
    {
        List<Album> logs = await _dbContext.Albums!.Where(cat => ids.Contains(cat.Id)).ToListAsync();
        _dbContext.RemoveRange(logs);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<Album?> FindByIdAsync(Guid id)
    {
        return await _dbContext.Albums!.Include(album => album.Categories).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Album?> FindByNameAsync(string name)
    {
        return await _dbContext.Albums!.Include(album => album.Categories).FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Album?> FindBySlugAsync(string slug)
    {
        return await _dbContext.Albums!.Include(album => album.Categories).FirstOrDefaultAsync(x => x.Slug == slug);
    }

    public async Task<(AlbumDto[]? Albums, long Count)> GetListAsync(GetAlbumListRequest request)
    {
        IQueryable<Album> query = _dbContext.Albums!.AsQueryable();
        if (!request.Keywords.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                EF.Functions.Like(log.Name, $"%{request.Keywords}%")
                || EF.Functions.Like(log.Slug, $"%{request.Keywords}%")
                || (log.Description != null && EF.Functions.Like(log.Description!, $"%{request.Keywords}%")));
        }

        AlbumDto[] albumsFromDb = await QueryAlbums(query, request.Current, request.PageSize);

        return (albumsFromDb, await query.LongCountAsync());
    }

    public async Task<Category[]> GetCategoriesOfAlbumAsync()
    {
        List<Guid> categoryIds = await _dbContext.Set<AlbumCategory>().Select(albumCategory => albumCategory.CategoryId)
            .ToListAsync();
        return await _dbContext.Set<Category>().Where(category => categoryIds.Contains(category.Id)).ToArrayAsync();
    }

    public async Task<(AlbumDto[]? Albums, long Count)> GetAlbumsByCategoryAsync(Guid categoryId, int pageIndex,
        int pageSize)
    {
        List<Guid> albumIds = await _dbContext.Set<AlbumCategory>()
            .Where(albumCategory => albumCategory.CategoryId == categoryId)
            .Select(albumCategory => albumCategory.AlbumId)
            .ToListAsync();
        IQueryable<Album> query = _dbContext.Set<Album>().Where(album => albumIds.Contains(album.Id));
        AlbumDto[] albumsFromDb = await QueryAlbums(query, pageIndex, pageSize);
        return (albumsFromDb, await query.LongCountAsync());
    }

    private async Task<AlbumDto[]> QueryAlbums(IQueryable<Album> query, int current, int pageSize)
    {
        AlbumDto[] albumsFromDb = await query.OrderByDescending(album => album.CreationTime)
            .Skip((current - 1) * pageSize).Take(pageSize)
            .Include(album => album.Categories).Select(album => new AlbumDto
            {
                Id = album.Id,
                SequenceNumber = album.SequenceNumber,
                Name = album.Name,
                Slug = album.Slug,
                Cover = album.Cover,
                Description = album.Description,
                CategoryIds = album.Categories == null
                    ? null
                    : album.Categories.Select(albumCategory => albumCategory.CategoryId).ToArray(),
                Visible = album.Visible,
                CreationName = album.CreationTime,
                BlogPostCount = _dbContext.Set<BlogPostAlbum>()
                    .Count(blogPostAlbum => blogPostAlbum.AlbumId == album.Id)
            }).ToArrayAsync();

        return albumsFromDb;
    }
}