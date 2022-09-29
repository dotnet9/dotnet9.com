namespace Dotnet9.WebAPI.Events;

public record ResetPasswordEvent(Guid Id, string UserName, string Password, string PhoneNumber);