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

    public async Task<(Link[]? Links, long Count)> GetListAsync(string? name, string? url, string? description,
        LinkKind? kind,
        int pageIndex, int pageSize)
    {
        var query = _dbContext.Links!.AsQueryable();
        if (!name.IsNullOrWhiteSpace())
        {
            query = query.Where(data =>
                EF.Functions.Like(data.Name, $"%{name}%"));
        }

        if (!url.IsNullOrWhiteSpace())
        {
            query = query.Where(data =>
                EF.Functions.Like(data.Url, $"%{url}%"));
        }

        if (!description.IsNullOrWhiteSpace())
        {
            query = query.Where(data =>
                data.Description != null && EF.Functions.Like(data.Description, $"%{description}%"));
        }

        if (kind != null)
        {
            query = query.Where(data => data.Kind == kind);
        }

        var datasFromDb = query.OrderBy(x => x.SequenceNumber).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        return (await datasFromDb.ToArrayAsync(), await query.LongCountAsync());
    }
}