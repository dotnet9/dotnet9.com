using Microsoft.AspNetCore.Http;

namespace Dotnet9Tools.Exceptions;

public class UserException : Exception
{
    public UserException(string message, int code = StatusCodes.Status500InternalServerError) : base(message)
    {
        Code = code;
    }

    public int Code { get; set; }
}