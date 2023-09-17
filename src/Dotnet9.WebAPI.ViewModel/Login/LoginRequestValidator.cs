namespace Dotnet9.WebAPI.ViewModel.Login;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(e => e.UserName).NotNull().WithMessage("用户名不能为Null").NotEmpty().WithMessage("用户名不能为空");
        RuleFor(e => e.Password).NotNull().WithMessage("密码不能为Null").NotEmpty().WithMessage("密码不能为空");
        RuleFor(e => e.Type).Must(e => e is LoginRequestType.Account or LoginRequestType.Mobile)
            .WithMessage($"登录请求类型不正确：{LoginRequestType.Account}|{LoginRequestType.Mobile}");
    }
}