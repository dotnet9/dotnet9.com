namespace IdentityService.WebAPI.Controllers.UserAdmin;

public record EditAdminUserRequest(string PhoneNumber);

public class EditAdminUserRequestValidator : AbstractValidator<EditAdminUserRequest>
{
    public EditAdminUserRequestValidator()
    {
        RuleFor(x => x.PhoneNumber).NotNull().NotEmpty();
    }
}