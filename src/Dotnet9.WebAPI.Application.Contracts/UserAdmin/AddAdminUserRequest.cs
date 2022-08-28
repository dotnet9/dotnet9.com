namespace Dotnet9.WebAPI.Application.Contracts.UserAdmin;

public record AddAdminUserRequest(string UserName, string PhoneNumber);

public class AddAdminUserRequestValidator : AbstractValidator<AddAdminUserRequest>
{
    public AddAdminUserRequestValidator()
    {
        RuleFor(e => e.PhoneNumber).NotNull().NotEmpty().MaximumLength(11);
        RuleFor(e => e.UserName).NotEmpty().NotEmpty().MaximumLength(20).MinimumLength(2);
    }
}