namespace Dotnet9.WebAPI.ViewModel.Auth;

public record ChangeMyPasswordRequest(string OldPassword, string NewPassword);