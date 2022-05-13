namespace Dotnet9.Application.Contracts;

public class PageDto<T> : BaseResponse
{
    public List<T>? Datas { get; set; }
    public int Total { get; set; }

    public static PageDto<T> Success(List<T> datas, int total)
    {
        return new PageDto<T>
        {
            Total = total,
            Datas = datas
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