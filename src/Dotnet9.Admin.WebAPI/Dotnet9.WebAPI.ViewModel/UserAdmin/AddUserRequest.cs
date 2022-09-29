namespace Dotnet9.WebAPI.ViewModel.UserAdmin;

public record AddUserRequest(string UserName, string RoleNames, string PhoneNumber);