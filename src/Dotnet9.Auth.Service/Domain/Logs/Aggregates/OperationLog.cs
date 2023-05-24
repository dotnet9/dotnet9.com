namespace Dotnet9.Auth.Service.Domain.Logs.Aggregates;

public class OperationLog : AggregateRoot<Guid>
{
    private Guid _operator;
    private string _operatorName;
    private OperationTypes _operationType;
    private DateTime _operationTime;
    private string _operationDescription = "";

    public Guid Operator
    {
        get => _operator;
        set => _operator = ArgumentExceptionExtensions.ThrowIfDefault(value, nameof(Operator));
    }

    public string OperatorName
    {
        get => _operatorName;
        set => _operatorName = ArgumentExceptionExtensions.ThrowIfNullOrEmpty(value, nameof(OperatorName));
    }

    public OperationTypes OperationType
    {
        get => _operationType;
        set => _operationType = ArgumentExceptionExtensions.ThrowIfDefault(value, nameof(OperationType));
    }

    public DateTime OperationTime
    {
        get => _operationTime;
        set { _operationTime = value.Kind == DateTimeKind.Local ? value.ToUniversalTime() : value; }
    }

    [AllowNull]
    public string OperationDescription
    {
        get => _operationDescription;
        set => _operationDescription = value ?? "";
    }

    public OperationLog(Guid @operator, string operatorName, OperationTypes operationType, DateTime operationTime,
        string? operationDescription)
    {
        Operator = @operator;
        OperatorName = operatorName;
        OperationType = operationType;
        if (operationTime == default) operationTime = DateTime.UtcNow;
        OperationTime = operationTime;
        OperationDescription = operationDescription;
    }
}