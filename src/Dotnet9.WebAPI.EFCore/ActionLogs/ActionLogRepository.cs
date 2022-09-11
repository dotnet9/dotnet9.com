namespace Dotnet9.WebAPI.EFCore.ActionLogs;

internal class ActionLogRepository : IActionLogRepository
{
    private readonly Dotnet9DbContext _dbContext;

    public ActionLogRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<QueryActionLogResponse> QueryAsync(string? keywords, int pageIndex, int pageSize)
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

        var logs = _dbContext.ActionLogs.Where(whereLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        var logCount = logs.LongCount();
        if (logCount <= 0)
        {
            return new QueryActionLogResponse(null, 0);
        }

        var logDatas = await logs.ToListAsync();
        return new QueryActionLogResponse(logDatas.Adapt<ActionLogDTO[]>(), logCount);
    }

    public async Task<int> DeleteAsync(Guid[] ids)
    {
        var logs = await _dbContext.ActionLogs.Where(log => ids.Contains(log.Id)).ToListAsync();
        _dbContext.RemoveRange(logs);
        return await _dbContext.SaveChangesAsync();
    }
}