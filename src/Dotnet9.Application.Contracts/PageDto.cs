namespace Dotnet9.Application.Contracts;

public class PageDto<T> : BaseResponse
{
    public List<T>? Datas { get; set; }
    public int Total { get; set; }
}

public class BaseResponse
{
    public string? Message { get; set; }
}