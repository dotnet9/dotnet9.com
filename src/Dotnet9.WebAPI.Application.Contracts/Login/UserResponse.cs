namespace Dotnet9.WebAPI.Application.Contracts.Login;

public record UserResponse(Guid Id, string PhoneNumber, DateTime CreationTime);