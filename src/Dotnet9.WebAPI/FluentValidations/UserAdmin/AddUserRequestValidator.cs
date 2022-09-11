namespace Dotnet9.WebAPI.FluentValidations.UserAdmin;

public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
{
    public AddUserRequestValidator()
    {
        RuleFor(e => e.UserName)
            .NotNull().WithMessage("用户名不能为Null")
            .NotEmpty().WithMessage("用户名不能为空")
            .MaximumLength(20).WithMessage("用户名长度不能超过20")
            .MinimumLength(2).WithMessage("用户名长度不能小于2");
        RuleFor(e => e.RoleName).Must(e => e is UserRoleConst.Admin or UserRoleConst.User)
            .WithMessage($"角色需要填写为【{UserRoleConst.Admin}】或【{UserRoleConst.User}】");
        RuleFor(e => e.PhoneNumber)
            .NotNull().WithMessage("电话号码不能为Null")
            .NotEmpty().WithMessage("电话号码不能为空")
            .MaximumLength(11).WithMessage("电话号码长度为11");
    }
}