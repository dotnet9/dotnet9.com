namespace Dotnet9.WebAPI.FluentValidations.Timelines;

public class DeleteTimelineRequestValidator : AbstractValidator<DeleteTimelineRequest>
{
    public DeleteTimelineRequestValidator()
    {
        RuleFor(e => e.Ids).Must(e => e != null && e.Any()).WithMessage("删除的ID不能为空");
    }
}