namespace Dotnet9.WebAPI.Infrastructure.ActionLogs;

internal class ActionLogRepository : IActionLogRepository
{
    private readonly Dotnet9DbContext _dbContext;

    public ActionLogRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(ActionLog[]? Logs, long Count)> GetListAsync(string? keywords, int pageIndex, int pageSize)
    {
        var query = _dbContext.ActionLogs.AsQueryable();
        if (!keywords.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                EF.Functions.Like(log.Ip, $"%{keywords}%"));
        }

        var logsFromDB = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        return (await logsFromDB.ToArrayAsync(), await query.LongCountAsync());
    }

    public async Task<int> DeleteAsync(Guid[] ids)
    {
        var logs = await _dbContext.ActionLogs.Where(log => ids.Contains(log.Id)).ToListAsync();
        _dbContext.RemoveRange(logs);
        return await _dbContext.SaveChangesAsync();
    }
}