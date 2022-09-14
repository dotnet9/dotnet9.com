namespace Dotnet9.WebAPI.ViewModels.UserAdmin;

public record UserDto(Guid Id, string UserName, string[] RoleNames, string PhoneNumber, DateTime CreationTime);