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

    public async Task<(Link[]? Links, long Count)> GetListAsync(string? keywords, int pageIndex, int pageSize)
    {
        var query = _dbContext.Links!.AsQueryable();
        if (!keywords.IsNullOrWhiteSpace())
        {
            query = query.Where(data =>
                EF.Functions.Like(data.Name, $"%{keywords}%")
                || EF.Functions.Like(data.Url, $"%{keywords}%")
                || (data.Description != null && EF.Functions.Like(data.Description!, $"%{keywords}%")));
        }

        var datasFromDb = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        return (await datasFromDb.ToArrayAsync(), await query.LongCountAsync());
    }
}