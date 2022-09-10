namespace Dotnet9.WebAPI.Domain.Shared;

public class PageDTO<T>
{
    public List<T>? Data { get; set; }
    public int Total { get; set; }
    public string? Message { get; set; }

    public static PageDTO<T> Success(List<T> data, int total)
    {
        return new PageDTO<T> { Total = total, Data = data };
    }

    public static PageDTO<T> Fail(string message)
    {
        return new PageDTO<T> { Message = message };
    }
}