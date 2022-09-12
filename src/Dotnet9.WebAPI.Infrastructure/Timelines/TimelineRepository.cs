namespace Dotnet9.WebAPI.Infrastructure.Timelines;

internal class TimelineRepository : ITimelineRepository
{
    private readonly Dotnet9DbContext _dbContext;

    public TimelineRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> DeleteAsync(Guid[] ids)
    {
        var datas = await _dbContext.Timelines.Where(cat => ids.Contains(cat.Id)).ToListAsync();
        _dbContext.RemoveRange(datas);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<Timeline?> FindByIdAsync(Guid id)
    {
        return await _dbContext.Timelines.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<(Timeline[]? Timelines, long Count)> GetListAsync(string? keywords, int pageIndex, int pageSize)
    {
        var query = _dbContext.Timelines.AsQueryable();
        if (!keywords.IsNullOrWhiteSpace())
        {
            query = query.Where(data =>
                EF.Functions.Like(data.Title, $"%{keywords}%")
                || EF.Functions.Like(data.Content, $"%{keywords}%"));
        }

        var datasFromDb = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        return (await datasFromDb.ToArrayAsync(), await query.LongCountAsync());
    }
}