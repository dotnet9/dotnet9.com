namespace Dotnet9.Application.Contracts.ActionLogs;

public class Top10SearchDto
{
    public int Total { get; set; }
    public List<Top10SearchItemDto>? Datas { get; set; }
}