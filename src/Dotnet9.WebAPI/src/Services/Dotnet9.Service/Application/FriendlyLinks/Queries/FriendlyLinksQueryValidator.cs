namespace Dotnet9.Service.Application.FriendlyLinks.Queries;

public class FriendlyLinksQueryValidator : AbstractValidator<FriendlyLinksQuery>
{
    public FriendlyLinksQueryValidator()
    {
        RuleFor(command => command.Page).GreaterThan(1).WithMessage("页码错误");
        RuleFor(command => command.PageSize).GreaterThan(0).WithMessage("页大小错误");
    }
}