using System.Linq.Expressions;
using Dotnet9.Domain.ActionLogs;
using Dotnet9.Domain.Repositories;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.ActionLogs;

public class EfCoreActionLogRepository : EfCoreRepository<ActionLog>, IActionLogRepository
{
    public EfCoreActionLogRepository(Dotnet9DbContext context) : base(context)
    {
    }

    public async Task<int> CountIPIn24HoursAsync()
    {
        var q = DbContext.ActionLogs!.Where(x => x.CreateDate > DateTimeOffset.Now.AddDays(-1)).Select(c => new
            {
                c.IP
            })
            .GroupBy(c => c.IP, (k, g) => new
            {
                IP = k,
                IPCount = g.Count()
            });

        return await Task.FromResult(q.Count());
    }

    public async Task<int> CountNotFoundIn24HoursAsync()
    {
        var q = DbContext.ActionLogs!
            .Where(x => x.CreateDate > DateTimeOffset.Now.AddDays(-1) && x.Url!.Contains("404")).Select(c => new
            {
                c.IP
            })
            .GroupBy(c => c.IP, (k, g) => new
            {
                IP = k,
                IPCount = g.Count()
            });

        return await Task.FromResult(q.Count());
    }

    public async Task<LatestActionLog> GetLatestActionLogAsync(DateTimeOffset? request)
    {
        var logPageInfos = await SelectAsync(15, 1,
            x => request == null || request == default(DateTimeOffset) ? x.Id > 0 : x.CreateDate > request,
            x => x.CreateDate,
            SortDirectionKind.Descending);
        var vm = new LatestActionLog
        {
            LatestDate = logPageInfos.Item1.Count > 0 ? logPageInfos.Item1[0].CreateDate : null,
            Datas = logPageInfos.Item1
        };
        return vm;
    }

    public async Task<Top10AccessPage> CountTop10AccessPagesAsync()
    {
        const string errorController = "Dotnet9.Web.Controllers.ErrorController";
        Expression<Func<ActionLog, bool>> where = x =>
            x.CreateDate > DateTimeOffset.Now.AddDays(-1) && !x.Controller!.Equals(errorController);
        var count = new Top10AccessPage
        {
            Total = await CountAsync(where)
        };
        var q = DbContext.ActionLogs!
            .Where(where).Select(
                c => new
                {
                    c.Url
                })
            .GroupBy(c => c.Url, (k, g) => new Top10AccessPageItem
            {
                Url = k,
                Percent = $"{g.Count() * 1.0 / count.Total:P1}",
                Pv = g.Count()
            }).OrderByDescending(x => x.Pv).Take(10);

        count.Datas = await q.ToListAsync();
        return count;
    }

    public async Task<Top10Search> CountTop10SearchAsync()
    {
        const string searchActionName = "Dotnet9.Web.Controllers.BlogPostController.Query (Dotnet9.Web)";
        Expression<Func<ActionLog, bool>> where = x =>
            x.CreateDate > DateTimeOffset.Now.AddDays(-1) && x.Action!.Equals(searchActionName);
        var count = new Top10Search
        {
            Total = await CountAsync(where)
        };
        var q = DbContext.ActionLogs!
            .Where(x => x.CreateDate > DateTimeOffset.Now.AddDays(-1) && x.Action!.Equals(searchActionName)).Select(c =>
                new
                {
                    c.Arguments
                })
            .GroupBy(c => c.Arguments, (k, g) => new Top10SearchItem
            {
                Key = k,
                Percent = $"{g.Count() * 1.0 / count.Total:P1}",
                Pv = g.Count()
            }).OrderByDescending(x => x.Pv).Take(10);

        count.Datas = await q.ToListAsync();
        return count;
    }
}