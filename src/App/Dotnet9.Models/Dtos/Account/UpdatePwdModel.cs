using FluentValidation;

namespace Dotnet9.Models.Dtos.Account;

public class UpdatePwdModel
{
    public Guid id { get; set; }

    public string NewPwd { get; set; }
}

public class UpdatePwdModelValidator : AbstractValidator<UpdatePwdModel>
{
    public UpdatePwdModelValidator()
    {
        RuleFor(a => a.id).NotEmpty().WithMessage("账号未找到");
        RuleFor(a => a.NewPwd).NotEmpty().MinimumLength(6).WithMessage("密码长度不能低于6位");
    }
}