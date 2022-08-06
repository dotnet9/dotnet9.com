namespace IdentityService.WebAPI.Controllers.Login;

public record ChangePasswordRequest(string Password, string Password2);

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.Password).NotNull().NotEmpty().Equal(e => e.Password2);
        RuleFor(x => x.Password2).NotNull().NotEmpty();
    }
}