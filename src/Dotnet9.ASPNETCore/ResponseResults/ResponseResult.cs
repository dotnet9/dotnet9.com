namespace Dotnet9.ASPNETCore.ResponseResults;

public class ResponseResult<T>
{
    public bool Success { get; set; }
    [JsonIgnore] public HttpStatusCode Code { get; set; }

    public string? ErrorCode
    {
        get { return Code.ToString(); }
    }

    public string? ErrorMessage { get; set; }
    public T? Data { get; set; }

    public static ResponseResult<T> GetSuccess(T data)
    {
        return new ResponseResult<T>
        {
            Success = true,
            Data = data
        };
    }

    public static ResponseResult<T> GetError(string message)
    {
        return new ResponseResult<T>
        {
            Success = false,
            Code = HttpStatusCode.BadRequest,
            ErrorMessage = message
        };
    }

    public static ResponseResult<T> GetError(HttpStatusCode code, string message)
    {
        return new ResponseResult<T>
        {
            Success = false,
            Code = code,
            ErrorMessage = message
        };
    }

    public static ResponseResult<T> GetResult(bool success, HttpStatusCode code, string message, T data)
    {
        return new ResponseResult<T>
        {
            Success = success,
            Code = code,
            ErrorMessage = message,
            Data = data
        };
    }

    public static implicit operator ResponseResult<T>(T data)
    {
        return GetSuccess(data);
    }
}