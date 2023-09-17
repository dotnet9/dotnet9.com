namespace Dotnet9.WebAPI.ViewModel.UserAdmin;

public record EditUserRequest(Guid Id, string RoleNames, string PhoneNumber);