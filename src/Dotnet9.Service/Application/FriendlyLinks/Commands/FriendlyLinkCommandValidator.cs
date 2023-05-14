namespace Dotnet9.Service.Application.FriendlyLinks.Commands;

public class FriendlyLinksQueryValidator : AbstractValidator<FriendlyLinkCommand>
{
    public FriendlyLinksQueryValidator()
    {
        RuleFor(command => command.Name).NotNull()
            .Length(FriendlyLinkConsts.MinNameLength, FriendlyLinkConsts.MaxNameLength)
            .WithMessage($"友情链接名称长度介于{FriendlyLinkConsts.MinNameLength}-{FriendlyLinkConsts.MaxNameLength}之间");
        RuleFor(command => command.Url).NotNull()
            .Length(FriendlyLinkConsts.MinUrlLength, FriendlyLinkConsts.MaxUrlLength)
            .WithMessage($"友情链接URL长度介于{FriendlyLinkConsts.MinUrlLength}-{FriendlyLinkConsts.MaxUrlLength}之间");
        RuleFor(command => command.Description)
            .Length(0, FriendlyLinkConsts.MaxUrlLength)
            .WithMessage($"友情链接URL长度介于{FriendlyLinkConsts.MaxUrlLength}之内");
    }
}