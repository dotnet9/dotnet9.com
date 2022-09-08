namespace Dotnet9.WebAPI.Application.Contracts.UserAdmin;

public record UserDTO(Guid Id, string UserName, string[] RoleNames, string PhoneNumber, DateTime CreationTime);