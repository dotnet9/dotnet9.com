namespace Dotnet9.WebAPI.FluentValidations.ActionLogs;

public class DeleteActionLogRequestValidator : AbstractValidator<DeleteActionLogRequest>
{
    public DeleteActionLogRequestValidator()
    {
        RuleFor(e => e.Ids).Must(e => e != null && e.Any()).WithMessage("删除的ID不能为空");
    }
}