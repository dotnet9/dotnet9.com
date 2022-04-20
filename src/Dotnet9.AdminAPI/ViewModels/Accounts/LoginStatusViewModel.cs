namespace Dotnet9.AdminAPI.ViewModels.Accounts;

public class LoginStatusViewModel
{
    public bool IsLogin { get; set; }
    public bool IsInit { get; set; }

    public static LoginStatusViewModel Success()
    {
        return new LoginStatusViewModel {IsLogin = true};
    }

    public static LoginStatusViewModel Fail()
    {
        return new LoginStatusViewModel {IsLogin = false};
    }
}