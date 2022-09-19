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
        IQueryable<ActionLog> query = _dbContext.ActionLogs!.AsQueryable();
        if (!request.Ua.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                EF.Functions.Like(log.Ua.ToLower(), $"%{request.Ua!.ToLower()}%"));
        }

        if (!request.Os.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                EF.Functions.Like(log.Os.ToLower(), $"%{request.Os!.ToLower()}%"));
        }

        if (!request.Browser.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                EF.Functions.Like(log.Browser.ToLower(), $"%{request.Browser!.ToLower()}%"));
        }

        if (!request.Ip.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                EF.Functions.Like(log.Ip.ToLower(), $"%{request.Ip!.ToLower()}%"));
        }

        if (!request.Referer.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                log.Referer != null && EF.Functions.Like(log.Referer.ToLower(), $"%{request.Referer!.ToLower()}%"));
        }

        if (!request.Original.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                log.Original != null && EF.Functions.Like(log.Original.ToLower(), $"%{request.Original!.ToLower()}%"));
        }

        if (!request.Url.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                log.Url != null && EF.Functions.Like(log.Url.ToLower(), $"%{request.Url!.ToLower()}%"));
        }

        if (!request.Controller.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                log.Controller != null &&
                EF.Functions.Like(log.Controller.ToLower(), $"%{request.Controller!.ToLower()}%"));
        }

        if (!request.Action.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                log.Action != null && EF.Functions.Like(log.Action.ToLower(), $"%{request.Action!.ToLower()}%"));
        }

        if (!request.Method.IsNullOrWhiteSpace())
        {
            query = query.Where(log => log.Method == request.Method);
        }

        if (!request.Arguments.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                log.Arguments != null &&
                EF.Functions.Like(log.Arguments.ToLower(), $"%{request.Arguments!.ToLower()}%"));
        }

        if (request.StartCreationTime != null && request.EndCreationTime == null)
        {
            query = query.Where(log => log.CreationTime >= request.StartCreationTime);
        }
        else if (request.StartCreationTime == null && request.EndCreationTime != null)
        {
            query = query.Where(log => log.CreationTime <= request.EndCreationTime);
        }
        else if (request.StartCreationTime != null && request.EndCreationTime != null)
        {
            query = query.Where(log =>
                log.CreationTime >= request.StartCreationTime && log.CreationTime <= request.EndCreationTime);
        }

        IQueryable<ActionLog> logsFromDB = query.OrderByDescending(x => x.CreationTime)
            .Skip((request.Current - 1) * request.PageSize)
            .Take(request.PageSize);

        return (await logsFromDB.ToArrayAsync(), await query.LongCountAsync());
    }

    public async Task<int> DeleteAsync(Guid[] ids)
    {
        List<ActionLog> logs = await _dbContext.ActionLogs!.Where(log => ids.Contains(log.Id)).ToListAsync();
        _dbContext.RemoveRange(logs);
        return await _dbContext.SaveChangesAsync();
    }
}