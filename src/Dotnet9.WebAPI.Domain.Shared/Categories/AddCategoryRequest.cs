namespace Dotnet9.WebAPI.Domain.Shared.Categories;

public class AddCategoryRequest
{
    public int SequenceNumber { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Cover { get; set; } = null!;
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public bool Visible { get; set; }
}

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