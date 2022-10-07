namespace Dotnet9.Domain.ActionLogs;

public class Top10AccessPage
{
    public int Total { get; set; }
    public List<Top10AccessPageItem>? Datas { get; set; }
}