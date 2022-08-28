namespace Dotnet9.WebAPI.Application.Contracts.UserAdmin;

public record EditAdminUserRequest(string PhoneNumber);

public class EditAdminUserRequestValidator : AbstractValidator<EditAdminUserRequest>
{
    public EditAdminUserRequestValidator()
    {
        RuleFor(e => e.PhoneNumber).NotNull().NotEmpty();
    }
}