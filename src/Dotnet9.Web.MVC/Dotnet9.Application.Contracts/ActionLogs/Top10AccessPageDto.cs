namespace Dotnet9.Application.Contracts.ActionLogs;

public class Top10AccessPageDto
{
    public int Total { get; set; }
    public List<Top10AccessPageItemDto>? Datas { get; set; }
}