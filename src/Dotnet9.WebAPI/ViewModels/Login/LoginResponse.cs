namespace Dotnet9.WebAPI.ViewModels.Login;

public record LoginResponse(string Status, string Type, string CurrentAuthority, string? Token);