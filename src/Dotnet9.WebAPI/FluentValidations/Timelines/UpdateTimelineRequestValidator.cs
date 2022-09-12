namespace Dotnet9.WebAPI.FluentValidations.Timelines;

public class UpdateTimelineRequestValidator : AbstractValidator<UpdateTimelineRequest>
{
    public UpdateTimelineRequestValidator()
    {
        RuleFor(x => x.Title).NotNull().Length(TimelineConsts.MinTitleLength, TimelineConsts.MaxTitleLength)
            .WithMessage($"标题长度范围[{TimelineConsts.MinTitleLength},{TimelineConsts.MaxTitleLength}]");

        RuleFor(x => x.Content).NotNull().Length(TimelineConsts.MinContentLength, TimelineConsts.MaxContentLength)
            .WithMessage($"内容长度范围[{TimelineConsts.MinContentLength},{TimelineConsts.MaxContentLength}]");
    }
}