namespace Dotnet9.WebAPI.ViewModel.UserAdmin;
public record GetUserListRequest(string? UserName, string? RoleName, string? PhoneNumber, DateTime? StartCreationTime, DateTime? EndCreationTime, int Current, int PageSize, string? Sort);
