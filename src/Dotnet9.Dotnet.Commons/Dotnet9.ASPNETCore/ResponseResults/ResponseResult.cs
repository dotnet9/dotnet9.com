namespace Dotnet9.ASPNETCore.ResponseResults;

public class ResponseResult<T>
{
    public bool Success { get; set; }
    [JsonIgnore] public HttpStatusCode HttpStatusCode { get; set; }

    public string Code
    {
        get { return ((int)HttpStatusCode).ToString(); }
    }

    public string? Message { get; set; }
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
            HttpStatusCode = HttpStatusCode.BadRequest,
            Message = message
        };
    }

    public static ResponseResult<T> GetError(HttpStatusCode code, string message)
    {
        return new ResponseResult<T>
        {
            Success = false,
            HttpStatusCode = code,
            Message = message
        };
    }

    public static ResponseResult<T> GetResult(bool success, HttpStatusCode code, string message, T data)
    {
        return new ResponseResult<T>
        {
            Success = success,
            HttpStatusCode = code,
            Message = message,
            Data = data
        };
    }

    public static implicit operator ResponseResult<T>(T data)
    {
        return GetSuccess(data);
    }
}