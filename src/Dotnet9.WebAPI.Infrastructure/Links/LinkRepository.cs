using Dotnet9.WebAPI.ViewModel.Links;

namespace Dotnet9.WebAPI.Infrastructure.Links;

internal class LinkRepository : ILinkRepository
{
    private readonly Dotnet9DbContext _dbContext;

    public LinkRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> DeleteAsync(Guid[] ids)
    {
        var datas = await _dbContext.Links!.Where(cat => ids.Contains(cat.Id)).ToListAsync();
        _dbContext.RemoveRange(datas);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<Link?> FindByIdAsync(Guid id)
    {
        return await _dbContext.Links!.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Link?> FindByNameAsync(string name)
    {
        return await _dbContext.Links!.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Link?> FindByUrlAsync(string url)
    {
        return await _dbContext.Links!.FirstOrDefaultAsync(x => x.Url == url);
    }

    public async Task<(Link[]? Links, long Count)> GetListAsync(GetLinkListRequest request)
    {
        var query = _dbContext.Links!.AsQueryable();
        if (!request.Name.IsNullOrWhiteSpace())
        {
            query = query.Where(data =>
                EF.Functions.Like(data.Name, $"%{request.Name}%"));
        }

        if (!request.Url.IsNullOrWhiteSpace())
        {
            query = query.Where(data =>
                EF.Functions.Like(data.Url, $"%{request.Url}%"));
        }

        if (!request.Description.IsNullOrWhiteSpace())
        {
            query = query.Where(data =>
                data.Description != null && EF.Functions.Like(data.Description, $"%{request.Description}%"));
        }

        if (request.Kind != null)
        {
            query = query.Where(data => data.Kind == request.Kind);
        }

        var datasFromDb = query.OrderBy(x => x.SequenceNumber).Skip((request.Current - 1) * request.PageSize).Take(request.PageSize);
        return (await datasFromDb.ToArrayAsync(), await query.LongCountAsync());
    }
}