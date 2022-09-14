namespace Dotnet9.ASPNETCore.ResponseResults;

[Route("api/[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    protected ResponseResult<T> Success<T>(T data, string? message = null)
    {
        return ResponseResult<T>.Success(data, message);
    }

    protected ResponseResult<T> Error<T>(string message)
    {
        return ResponseResult<T>.Error(message);
    }

    protected ResponseResult<T> Error<T>(HttpStatusCode code, string message)
    {
        return ResponseResult<T>.Error(code, message);
    }

    protected ResponseResult<T> Result<T>(HttpStatusCode code, string message, T data)
    {
        return ResponseResult<T>.Result(code, message, data);
    }
}