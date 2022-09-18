namespace Dotnet9.WebAPI.ViewModel.UserAdmin;

public class EditUserRequestValidator : AbstractValidator<EditUserRequest>
{
    public EditUserRequestValidator()
    {
        RuleFor(e => e.RoleName).Must(e => e is UserRoleConst.Admin or UserRoleConst.User)
            .WithMessage($"角色需要填写为【{UserRoleConst.Admin}】或【{UserRoleConst.User}】");
        RuleFor(e => e.PhoneNumber)
            .NotNull().WithMessage("电话号码不能为Null")
            .NotEmpty().WithMessage("电话号码不能为空")
            .MaximumLength(11).WithMessage("电话号码长度为11");
    }
}