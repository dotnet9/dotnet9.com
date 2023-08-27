namespace Dotnet9.WebAPI.ViewModel.UserAdmin;

public record EditUserBaseRequest(Guid Id, string? NickName, string? Brief, string? WebSite);