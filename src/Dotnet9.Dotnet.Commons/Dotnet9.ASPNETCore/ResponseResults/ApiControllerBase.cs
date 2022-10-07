namespace Dotnet9.ASPNETCore.ResponseResults;

[Route("api/[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected ResponseResult<T> GetSuccess<T>(T data)
    {
        return ResponseResult<T>.GetSuccess(data);
    }

    protected ResponseResult<T> GetError<T>(string message)
    {
        return ResponseResult<T>.GetError(message);
    }

    protected ResponseResult<T> GetError<T>(HttpStatusCode code, string message)
    {
        return ResponseResult<T>.GetError(code, message);
    }

    protected ResponseResult<T> GetResult<T>(bool success, HttpStatusCode code, string message, T data)
    {
        return ResponseResult<T>.GetResult(success, code, message, data);
    }
}