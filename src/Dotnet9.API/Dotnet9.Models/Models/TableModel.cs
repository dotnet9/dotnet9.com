namespace Dotnet9.Models.Models;

public class TableModel<T>
{
    public int Code { get; set; }

    public string? Message { get; set; }

    public int Count { get; set; }

    public List<T>? Data { get; set; }
}