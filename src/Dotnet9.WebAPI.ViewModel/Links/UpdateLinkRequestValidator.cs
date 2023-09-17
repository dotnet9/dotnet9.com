namespace Dotnet9.WebAPI.ViewModel.Links;

public class UpdateLinkRequestValidator : AbstractValidator<UpdateLinkRequest>
{
    public UpdateLinkRequestValidator()
    {
        RuleFor(x => x.Name).NotNull().Length(LinkConsts.MinNameLength, LinkConsts.MaxNameLength)
            .WithMessage($"名称长度范围[{LinkConsts.MinNameLength},{LinkConsts.MaxNameLength}]");

        RuleFor(x => x.Url).NotNull().Length(LinkConsts.MinUrlLength, LinkConsts.MaxUrlLength)
            .WithMessage($"Url长度范围[{LinkConsts.MinUrlLength},{LinkConsts.MaxUrlLength}]");

        RuleFor(x => x.Description).MaximumLength(LinkConsts.MaxDescriptionLength)
            .WithMessage($"描述长度不能大于{LinkConsts.MaxDescriptionLength}]");
    }
}