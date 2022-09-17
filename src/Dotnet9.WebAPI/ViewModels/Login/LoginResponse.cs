namespace Dotnet9.WebAPI.ViewModels.Login;

public record LoginResponse(bool Success, string Status, string Type, string CurrentAuthority, string? Token,
    string? ErrorMessage = null);