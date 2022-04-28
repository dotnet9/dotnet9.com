namespace Dotnet9.Application.Contracts.Users;

public class LoginResultDto
{
    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

    public static LoginResultDto Success()
    {
        return new LoginResultDto {IsSuccess = true};
    }

    public static LoginResultDto Fail(string message)
    {
        return new LoginResultDto {Message = message};
    }
}