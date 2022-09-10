namespace Dotnet9.WebAPI.Domain.Shared.Login;

public record ChangeMyPasswordRequest(string Password, string Password2);

public class ChangePasswordRequestValidator : AbstractValidator<ChangeMyPasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(e => e.Password).NotNull().WithMessage("原密码不能为Null").NotEmpty().WithMessage("原密码不能为空")
            .NotEqual(e => e.Password2).WithMessage("新密码不能与原密码相同");
        RuleFor(e => e.Password2).NotNull().WithMessage("新密码不能为Null").NotEmpty().WithMessage("新密码不能为空");
    }
}