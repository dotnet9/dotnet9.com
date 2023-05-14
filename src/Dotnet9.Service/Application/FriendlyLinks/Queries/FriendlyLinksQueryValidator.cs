namespace Dotnet9.Service.Application.FriendlyLinks.Queries;

public class FriendlyLinksQueryValidator : AbstractValidator<FriendlyLinksQuery>
{
    public FriendlyLinksQueryValidator()
    {
        RuleFor(command => command.Page).GreaterThanOrEqualTo(1).WithMessage("页码错误");
        RuleFor(command => command.PageSize).GreaterThanOrEqualTo(20).WithMessage("页大小错误");
    }
}