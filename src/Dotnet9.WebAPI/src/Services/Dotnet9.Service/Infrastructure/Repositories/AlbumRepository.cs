namespace Dotnet9.Service.Infrastructure.Repositories;

public class AlbumRepository : Repository<Dotnet9DbContext, Album, Guid>, IAlbumRepository
{
    public AlbumRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<Album?> FindByIdAsync(Guid id)
    {
        return await Context.Albums!.Include(album => album.Categories).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Album?> FindByNameAsync(string name)
    {
        return await Context.Albums!.Include(album => album.Categories).FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Album?> FindBySlugAsync(string slug)
    {
        return await Context.Albums!.Include(album => album.Categories).FirstOrDefaultAsync(x => x.Slug == slug);
    }
}