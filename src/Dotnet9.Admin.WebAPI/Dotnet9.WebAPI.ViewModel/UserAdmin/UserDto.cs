namespace Dotnet9.WebAPI.ViewModel.UserAdmin;

public record UserDto(Guid Id, string UserName, string RoleNames, string PhoneNumber, DateTime CreationTime);