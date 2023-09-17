namespace Dotnet9.WebAPI.ViewModel.Login;

public record ChangeMyPasswordRequest(string OldPassword, string NewPassword);