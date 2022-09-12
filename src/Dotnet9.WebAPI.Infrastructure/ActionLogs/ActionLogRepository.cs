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
        Expression<Func<ActionLog, bool>> whereLambda;
        if (keywords.IsNullOrWhiteSpace())
        {
            whereLambda = log => true;
        }
        else
        {
            whereLambda = log =>
                EF.Functions.Like(log.IP, $"%{keywords}%")
                || EF.Functions.Like(log.OS, $"%{keywords}%");
        }

        var query = _dbContext.ActionLogs.Where(whereLambda);
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