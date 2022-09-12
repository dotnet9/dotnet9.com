namespace Dotnet9.WebAPI.FluentValidations.Tags;

public class DeleteTagRequestValidator : AbstractValidator<DeleteTagRequest>
{
    public DeleteTagRequestValidator()
    {
        RuleFor(e => e.Ids).Must(e => e != null && e.Any()).WithMessage("删除的ID不能为空");
    }
}