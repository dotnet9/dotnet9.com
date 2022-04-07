using Dotnet9.Domain.Albums;
using Dotnet9.Domain.Blogs;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.Albums;

public class EfCoreAlbumRepository : EfCoreRepository<Album>, IAlbumRepository
{
    public EfCoreAlbumRepository(Dotnet9DbContext context) : base(context)
    {
    }

    public async Task<Album?> FindByNameAsync(string name)
    {
        return await DbContext.Albums!.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Album?> FindBySlugAsync(string slug)
    {
        return await DbContext.Albums!.FirstOrDefaultAsync(x => x.Slug == slug);
    }

    public async Task<List<AlbumCount>> GetListCountAsync()
    {
        var query = from album in DbContext.Albums
            join blogPostAlbum in DbContext.Set<BlogPostAlbum>()
                on album.Id equals blogPostAlbum.AlbumId
            select new { album.Id, album.Name, album.Slug, album.Cover }
            into x
            group x by new { x.Id, x.Name, x.Slug, x.Cover }
            into g
            orderby g.Count() descending
            select new AlbumCount(g.Key.Id, g.Key.Name, g.Key.Slug, g.Key.Cover, g.Count());

        return await query.ToListAsync();
    }
}