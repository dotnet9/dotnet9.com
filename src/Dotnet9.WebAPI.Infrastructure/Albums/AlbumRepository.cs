using Microsoft.EntityFrameworkCore.Query;

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

    public async Task<(Album[]? Albums, long Count)> GetListAsync(GetAlbumListRequest request)
    {
        IQueryable<Album> query = _dbContext.Albums!.AsQueryable();
        if (!request.Keywords.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                EF.Functions.Like(log.Name, $"%{request.Keywords}%")
                || EF.Functions.Like(log.Slug, $"%{request.Keywords}%")
                || (log.Description != null && EF.Functions.Like(log.Description!, $"%{request.Keywords}%")));
        }

        IIncludableQueryable<Album, List<AlbumCategory>?> albumsFromDb = query
            .Skip((request.Current - 1) * request.PageSize).Take(request.PageSize)
            .Include(album => album.Categories);

        return (await albumsFromDb.ToArrayAsync(), await query.LongCountAsync());
    }

    public async Task<Category[]> GetCategoriesOfAlbumAsync()
    {
        List<Guid> categoryIds = await _dbContext.Set<AlbumCategory>().Select(albumCategory => albumCategory.CategoryId)
            .ToListAsync();
        return await _dbContext.Set<Category>().Where(category => categoryIds.Contains(category.Id)).ToArrayAsync();
    }

    public async Task<(Album[]? Albums, long Count)> GetAlbumsByCategoryAsync(Guid categoryId, int pageIndex,
        int pageSize)
    {
        List<Guid> albumIds = await _dbContext.Set<AlbumCategory>()
            .Where(albumCategory => albumCategory.CategoryId == categoryId)
            .Select(albumCategory => albumCategory.AlbumId)
            .ToListAsync();
        IQueryable<Album> query = _dbContext.Set<Album>().Where(album => albumIds.Contains(album.Id));
        IIncludableQueryable<Album, List<AlbumCategory>?> albumsFromDb = query.OrderByDescending(x => x.CreationTime)
            .Skip((pageIndex - 1) * pageSize).Take(pageSize)
            .Include(album => album.Categories);
        return (await albumsFromDb.ToArrayAsync(), await query.LongCountAsync());
    }
}