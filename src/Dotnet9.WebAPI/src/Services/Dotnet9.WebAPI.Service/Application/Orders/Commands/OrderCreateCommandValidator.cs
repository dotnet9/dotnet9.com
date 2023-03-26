namespace Dotnet9.WebAPI.Service.Application.Orders.Commands;

public class OrderCreateCommandValidator : AbstractValidator<OrderCreateCommand>
{
    public OrderCreateCommandValidator()
    {
        RuleFor(cmd => cmd.Items).Must(cmd => cmd.Any()).WithMessage("The order items cannot be empty");
    }
}