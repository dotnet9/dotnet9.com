namespace Dotnet9.WebAPI.FluentValidations.Links;

public class DeleteLinkRequestValidator : AbstractValidator<DeleteLinkRequest>
{
    public DeleteLinkRequestValidator()
    {
        RuleFor(e => e.Ids).Must(e => e != null && e.Any()).WithMessage("删除的ID不能为空");
    }
}