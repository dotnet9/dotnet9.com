namespace IdentityService.WebAPI.Controllers.Login;

public record LoginByPhoneAndPwdRequest(string PhoneNumber, string Password);

public class LoginByPhoneAndPwdRequestValidator : AbstractValidator<LoginByPhoneAndPwdRequest>
{
    public LoginByPhoneAndPwdRequestValidator()
    {
        RuleFor(x => x.PhoneNumber).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}