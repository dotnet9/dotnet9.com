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
        var logs = await _dbContext.Albums.Where(cat => ids.Contains(cat.Id)).ToListAsync();
        _dbContext.RemoveRange(logs);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<Album?> FindByIdAsync(Guid id)
    {
        return await _dbContext.Albums.Include(album => album.Categories).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Album?> FindByNameAsync(string name)
    {
        return await _dbContext.Albums.Include(album => album.Categories).FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Album?> FindBySlugAsync(string slug)
    {
        return await _dbContext.Albums.Include(album => album.Categories).FirstOrDefaultAsync(x => x.Slug == slug);
    }

    public async Task<(Album[]? Albums, long Count)> GetListAsync(string? keywords, int pageIndex, int pageSize)
    {
        Expression<Func<Album, bool>> whereLambda;
        if (keywords.IsNullOrWhiteSpace())
        {
            whereLambda = log => true;
        }
        else
        {
            whereLambda = log =>
                EF.Functions.Like(log.Name, $"%{keywords}%")
                || EF.Functions.Like(log.Slug, $"%{keywords}%")
                || (log.Description != null && EF.Functions.Like(log.Description!, $"%{keywords}%"));
        }

        var query = _dbContext.Albums.Where(whereLambda);
        var albumsFromDb = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Include(album => album.Categories);

        return (await albumsFromDb.ToArrayAsync(), await query.LongCountAsync());
    }

    public async Task<Category[]> GetCategoriesOfAlbumAsync()
    {
        var categoryIds = await _dbContext.Set<AlbumCategory>().Select(albumCategory => albumCategory.CategoryId)
            .ToListAsync();
        return await _dbContext.Set<Category>().Where(category => categoryIds.Contains(category.Id)).ToArrayAsync();
    }

    public async Task<(Album[]? Albums, long Count)> GetAlbumsByCategoryAsync(Guid categoryId, int pageIndex,
        int pageSize)
    {
        var albumIds = await _dbContext.Set<AlbumCategory>()
            .Where(albumCategory => albumCategory.CategoryId == categoryId)
            .Select(albumCategory => albumCategory.AlbumId)
            .ToListAsync();
        var query = _dbContext.Set<Album>().Where(album => albumIds.Contains(album.Id));
        var albumsFromDb = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Include(album => album.Categories);
        return (await albumsFromDb.ToArrayAsync(), await query.LongCountAsync());
    }
}