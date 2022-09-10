namespace Dotnet9.WebAPI.Domain.Shared.Login;

public record UserResponse(Guid Id, string PhoneNumber, DateTime CreationTime);