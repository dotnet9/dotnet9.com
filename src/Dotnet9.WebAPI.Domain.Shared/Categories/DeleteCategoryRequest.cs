namespace Dotnet9.WebAPI.Domain.Shared.Categories;

public record DeleteCategoryRequest(Guid[] Ids);

public class DeleteCategoryRequestValidator : AbstractValidator<DeleteCategoryRequest>
{
    public DeleteCategoryRequestValidator()
    {
        RuleFor(e => e.Ids).Must(e => e != null && e.Any()).WithMessage("删除的ID不能为空");
    }
}