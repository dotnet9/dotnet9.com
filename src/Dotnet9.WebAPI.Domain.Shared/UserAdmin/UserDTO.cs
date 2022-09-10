namespace Dotnet9.WebAPI.Domain.Shared.UserAdmin;

public record UserDTO(Guid Id, string UserName, string[] RoleNames, string PhoneNumber, DateTime CreationTime);