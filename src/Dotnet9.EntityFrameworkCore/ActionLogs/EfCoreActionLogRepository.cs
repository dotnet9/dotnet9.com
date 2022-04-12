using Dotnet9.Domain.ActionLogs;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.ActionLogs;

public class EfCoreActionLogRepository : EfCoreRepository<ActionLog>, IActionLogRepository
{
    public EfCoreActionLogRepository(Dotnet9DbContext context) : base(context)
    {
    }
}