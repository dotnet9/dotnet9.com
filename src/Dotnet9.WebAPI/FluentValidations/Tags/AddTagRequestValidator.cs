namespace Dotnet9.WebAPI.FluentValidations.Tags;

public class AddTagRequestValidator : AbstractValidator<AddTagRequest>
{
    public AddTagRequestValidator()
    {
        RuleFor(x => x.Name).NotNull().Length(TagConsts.MinNameLength, TagConsts.MaxNameLength)
            .WithMessage($"名称长度范围[{TagConsts.MinNameLength},{TagConsts.MaxNameLength}]");
    }
}