namespace Dotnet9.WebAPI.FluentValidations.Categories;

public class AddCategoryRequestValidator : AbstractValidator<AddCategoryRequest>
{
    public AddCategoryRequestValidator()
    {
        RuleFor(x => x.Name).NotNull().Length(CategoryConsts.MinNameLength, CategoryConsts.MaxNameLength)
            .WithMessage($"名称长度范围[{CategoryConsts.MinNameLength},{CategoryConsts.MaxNameLength}]");

        RuleFor(x => x.Slug).NotNull().Length(CategoryConsts.MinSlugLength, CategoryConsts.MaxSlugLength)
            .WithMessage($"别名长度范围[{CategoryConsts.MinSlugLength},{CategoryConsts.MaxSlugLength}]");

        RuleFor(x => x.Cover).NotNull().Length(CategoryConsts.MinCoverLength, CategoryConsts.MaxCoverLength)
            .WithMessage($"封面URL长度范围[{CategoryConsts.MinCoverLength},{CategoryConsts.MaxCoverLength}]");

        RuleFor(x => x.Description).MaximumLength(CategoryConsts.MaxDescriptionLength)
            .WithMessage($"描述长度不能大于{CategoryConsts.MaxDescriptionLength}]");
    }
}