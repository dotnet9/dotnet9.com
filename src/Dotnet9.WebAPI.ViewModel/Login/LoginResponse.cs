namespace Dotnet9.WebAPI.ViewModel.Login;

public record LoginResponse(bool Success, string Status, string Type, string CurrentAuthority, string? Token,
    string? ErrorMessage = null);