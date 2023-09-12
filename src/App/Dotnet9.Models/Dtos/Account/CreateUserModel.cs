using FluentValidation;

namespace Dotnet9.Models.Dtos.Account;

public class CreateUserModel
{
    /// <summary>
    ///     用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///     密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    ///     邮箱
    /// </summary>
    public string? Email { get; set; }
}

public class CreateUserValidate : AbstractValidator<CreateUserModel>
{
    public CreateUserValidate()
    {
        RuleFor(c => c.UserName).NotEmpty().WithMessage("用户名不能为空").MinimumLength(4).WithMessage("用户名不能为空，且长度大于4");
        When(a => string.IsNullOrWhiteSpace(a.Email) == false,
            () => { RuleFor(a => a.Email).EmailAddress().WithMessage("邮箱格式错误！"); });
    }
}