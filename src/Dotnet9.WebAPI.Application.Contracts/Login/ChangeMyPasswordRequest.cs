namespace Dotnet9.WebAPI.Application.Contracts.Login;

public record ChangeMyPasswordRequest(string Password, string Password2);

public class ChangePasswordRequestValidator : AbstractValidator<ChangeMyPasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(e => e.Password).NotNull().NotEmpty()
            .NotEqual(e => e.Password2);
        RuleFor(e => e.Password2).NotNull().NotEmpty();
    }
}