namespace IdentityService.WebAPI.Controllers.UserAdmin;

public record AddAdminUserRequest(string UserName, string PhoneNumber);

public class AddAdminUserRequestValidator : AbstractValidator<AddAdminUserRequest>
{
    public AddAdminUserRequestValidator()
    {
        RuleFor(x => x.UserName).NotNull().NotEmpty().MaximumLength(11);
        RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().MaximumLength(20).MinimumLength(2);
    }
}