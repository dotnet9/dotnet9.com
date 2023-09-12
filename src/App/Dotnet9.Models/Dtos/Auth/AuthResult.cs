namespace Dotnet9.Models.Dtos.Auth;

public class AuthResult<T> : BaseAuthResult where T : new()
{
    public AuthResult(bool isOk, string message = "") : base(message, isOk)
    {
        IsOk = isOk;
        Message = message;
    }


    public T Data { get; set; }
}

public class BaseAuthResult
{
    public BaseAuthResult(string message, bool isOk)
    {
        Message = message;
        IsOk = isOk;
    }

    public string Message { get; set; }

    public bool IsOk { get; set; }
}