namespace Dotnet9.WebAPI.ViewModel.Login;

public class ChangePasswordRequestValidator : AbstractValidator<ChangeMyPasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(e => e.OldPassword).NotNull().WithMessage("原密码不能为Null").NotEmpty().WithMessage("原密码不能为空")
            .NotEqual(e => e.NewPassword).WithMessage("新密码不能与原密码相同");
        RuleFor(e => e.NewPassword).NotNull().WithMessage("新密码不能为Null").NotEmpty().WithMessage("新密码不能为空");
    }
}