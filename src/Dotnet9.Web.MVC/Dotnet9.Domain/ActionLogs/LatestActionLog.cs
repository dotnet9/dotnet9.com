namespace Dotnet9.Domain.ActionLogs;

public class LatestActionLog
{
    public List<ActionLog>? Datas { get; set; }
    public DateTimeOffset? LatestDate { get; set; }
}