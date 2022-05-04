using Dotnet9.Domain.ActionLogs;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;

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

        return q.Count();
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

        return q.Count();
    }
}