using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Dotnet9.WebAPI.Infrastructure.ActionLogs;

internal class ActionLogRepository : IActionLogRepository
{
    private readonly Dotnet9DbContext _dbContext;

    public ActionLogRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> DeleteAsync(Guid[] ids)
    {
        List<ActionLog> logs = await _dbContext.ActionLogs!.Where(log => ids.Contains(log.Id)).ToListAsync();
        _dbContext.RemoveRange(logs);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<(ActionLog[]? Logs, long Count)> GetListAsync(GetActionLogListRequest request)
    {
        IQueryable<ActionLog> query = _dbContext.ActionLogs!.AsQueryable();
        query = CheckAddQuery(request, query);

        void UseDefaultSort()
        {
            query = query.OrderByDescending(x => x.CreationTime);
        }

        if (!request.Sort.IsNullOrWhiteSpace())
        {
            var sort = JsonSerializer.Deserialize<Dictionary<string, string>>(request.Sort!);
            var sortFieldName = sort?.Keys.FirstOrDefault();
            var sortOrder = sort?.Values.FirstOrDefault();
            if (sortFieldName.IsNullOrWhiteSpace() || sortOrder.IsNullOrWhiteSpace())
            {
                UseDefaultSort();
            }
            else
            {
                query = query.OrderBy(sortFieldName, "ascend" == sortOrder);
            }
        }
        else
        {
            UseDefaultSort();
        }

        IQueryable<ActionLog> logsFromDB = query.Skip((request.Current - 1) * request.PageSize).Take(request.PageSize);

        return (await logsFromDB.ToArrayAsync(), await query.LongCountAsync());
    }

    private static IQueryable<ActionLog> CheckAddQuery(GetActionLogListRequest request, IQueryable<ActionLog> query)
    {
        if (!request.Keywords.IsNullOrWhiteSpace())
        {
            var keywords = $"%{request.Keywords!.ToLower()}%";
            query = query.Where(log =>
                EF.Functions.Like(log.Ua.ToLower(), keywords)
                || EF.Functions.Like(log.Os.ToLower(), keywords)
                || EF.Functions.Like(log.Browser.ToLower(), keywords)
                || EF.Functions.Like(log.Ip.ToLower(), keywords)
                || (log.Referer != null && EF.Functions.Like(log.Referer.ToLower(), keywords))
                || (log.Original != null && EF.Functions.Like(log.Original.ToLower(), keywords))
                || (log.Url != null && EF.Functions.Like(log.Url.ToLower(), keywords))
                || (log.Controller != null && EF.Functions.Like(log.Controller.ToLower(), keywords))
                || (log.Action != null && EF.Functions.Like(log.Action.ToLower(), keywords))
                || log.Method == request.Keywords
                || (log.Arguments != null && EF.Functions.Like(log.Arguments.ToLower(), keywords)));
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

        return query;
    }
}