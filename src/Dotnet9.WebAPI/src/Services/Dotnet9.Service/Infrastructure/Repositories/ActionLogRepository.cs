namespace Dotnet9.Service.Infrastructure.Repositories;

public class ActionLogRepository : Repository<Dotnet9DbContext, ActionLog, Guid>, IActionLogRepository
{
    public ActionLogRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }
}