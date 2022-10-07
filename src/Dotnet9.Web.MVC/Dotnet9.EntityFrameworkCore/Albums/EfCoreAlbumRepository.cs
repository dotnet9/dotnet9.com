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
        var query = from album in DbContext.Set<Album>()
            select new AlbumCount(album.Id, album.Name, album.Slug, album.Cover, 
                DbContext.Set<BlogPostAlbum>().Count(d => d.AlbumId == album.Id));

        return await query.ToListAsync();
    }
}