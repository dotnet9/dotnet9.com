namespace Dotnet9.Web.Service.Albums;

internal class AlbumService : IAlbumService
{
    private readonly Dotnet9DbContext _dbContext;

    public AlbumService(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<AlbumBrief>> GetAlbumsAsync()
    {
        List<AlbumBrief> albums = await _dbContext.Albums!.Select(c => new AlbumBrief(c.Slug, c.Name, c.Description,
                _dbContext.Set<BlogPostAlbum>().Count(d => d.AlbumId == c.Id)))
            .ToListAsync();
        IOrderedEnumerable<AlbumBrief> distinctCategories = from cat in albums
            where cat.BlogCount > 0
            orderby cat.BlogCount descending
            select cat;
        return distinctCategories.ToList();
    }
}