namespace Dotnet9.WebAPI.ViewModel.Tags;

public class UpdateTagRequestValidator : AbstractValidator<UpdateTagRequest>
{
    public UpdateTagRequestValidator()
    {
        RuleFor(x => x.Name).NotNull().Length(TagConsts.MinNameLength, TagConsts.MaxNameLength)
            .WithMessage($"名称长度范围[{TagConsts.MinNameLength},{TagConsts.MaxNameLength}]");
    }
}