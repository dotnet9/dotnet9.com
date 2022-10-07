namespace Dotnet9.Application.Contracts.ActionLogs;

public class LatestActionLogDto
{
    public List<LatestActionLogItemDto>? Datas { get; set; }
    public string? LatestDate { get; set; }
}