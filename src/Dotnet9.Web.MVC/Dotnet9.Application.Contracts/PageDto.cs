namespace Dotnet9.Application.Contracts;

public class PageDto<T> : BaseResponse
{
    public List<T>? Data { get; set; }
    public int Total { get; set; }

    public static PageDto<T> Success(List<T> data, int total)
    {
        return new PageDto<T>
        {
            Total = total,
            Data = data
        };
    }

    public static PageDto<T> Fail(string message)
    {
        return new PageDto<T> {Message = message};
    }
}

public class BaseResponse
{
    public string? Message { get; set; }
}