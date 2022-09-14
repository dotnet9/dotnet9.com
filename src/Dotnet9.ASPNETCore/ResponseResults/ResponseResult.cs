namespace Dotnet9.ASPNETCore.ResponseResults;

public class ResponseResult<T>
{
    public HttpStatusCode Code { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public static ResponseResult<T> Success(T data, string? message = null)
    {
        return new ResponseResult<T>
        {
            Code = HttpStatusCode.OK,
            Message = message,
            Data = data
        };
    }

    public static ResponseResult<T> Error(string message)
    {
        return new ResponseResult<T>
        {
            Code = HttpStatusCode.BadRequest,
            Message = message
        };
    }

    public static ResponseResult<T> Error(HttpStatusCode code, string message)
    {
        return new ResponseResult<T>
        {
            Code = code,
            Message = message
        };
    }

    public static ResponseResult<T> Result(HttpStatusCode code, string message, T data)
    {
        return new ResponseResult<T>
        {
            Code = code,
            Message = message,
            Data = data
        };
    }

    public static implicit operator ResponseResult<T>(T data)
    {
        return Success(data);
    }
}