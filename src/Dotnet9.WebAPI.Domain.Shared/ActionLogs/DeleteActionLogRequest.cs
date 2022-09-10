namespace Dotnet9.WebAPI.Domain.Shared.ActionLogs;

public record DeleteActionLogRequest(Guid[] Ids);

public class DeleteActionLogRequestValidator : AbstractValidator<DeleteActionLogRequest>
{
    public DeleteActionLogRequestValidator()
    {
        RuleFor(e => e.Ids).Must(e => e != null && e.Any()).WithMessage("删除的ID不能为空");
    }
}