namespace Dotnet9.Auth.Service.Domain.Logs.Repositories;

public interface IOperationLogRepository : IRepository<OperationLog, Guid>
{
    Task AddDefaultAsync(OperationTypes operationType, string operationDescription, Guid? @operator = null);
}