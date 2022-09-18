namespace Dotnet9.WebAPI.Infrastructure.ActionLogs;

internal class ActionLogRepository : IActionLogRepository
{
    private readonly Dotnet9DbContext _dbContext;

    public ActionLogRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(ActionLog[]? Logs, long Count)> GetListAsync(GetActionLogListRequest request)
    {
        var query = _dbContext.ActionLogs!.AsQueryable();
        if (!request.Ua.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                EF.Functions.Like(log.Ip, $"%{request.Ua}%"));
        }

        var logsFromDB = query.Skip((request.Current - 1) * request.PageSize).Take(request.PageSize);

        return (await logsFromDB.ToArrayAsync(), await query.LongCountAsync());
    }

    public async Task<int> DeleteAsync(Guid[] ids)
    {
        var logs = await _dbContext.ActionLogs!.Where(log => ids.Contains(log.Id)).ToListAsync();
        _dbContext.RemoveRange(logs);
        return await _dbContext.SaveChangesAsync();
    }
}