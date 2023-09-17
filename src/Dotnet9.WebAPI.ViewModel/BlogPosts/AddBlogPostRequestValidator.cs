namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public class AddBlogPostRequestValidator : AbstractValidator<AddBlogPostRequest>
{
    public AddBlogPostRequestValidator()
    {
        RuleFor(x => x.Title).NotNull().Length(BlogPostConsts.MinTitleLength, BlogPostConsts.MaxTitleLength)
            .WithMessage($"标题长度范围[{BlogPostConsts.MinTitleLength},{BlogPostConsts.MaxTitleLength}]");

        RuleFor(x => x.Slug).NotNull().Length(BlogPostConsts.MinSlugLength, BlogPostConsts.MaxSlugLength)
            .WithMessage($"别名长度范围[{BlogPostConsts.MinSlugLength},{BlogPostConsts.MaxSlugLength}]");

        RuleFor(x => x.Description).NotNull()
            .Length(BlogPostConsts.MinDescriptionLength, BlogPostConsts.MaxDescriptionLength)
            .WithMessage($"描述长度范围[{BlogPostConsts.MinDescriptionLength},{BlogPostConsts.MaxDescriptionLength}]");

        RuleFor(x => x.Cover).NotNull().Length(BlogPostConsts.MinCoverLength, BlogPostConsts.MaxCoverLength)
            .WithMessage($"封面URL长度范围[{BlogPostConsts.MinCoverLength},{BlogPostConsts.MaxCoverLength}]");

        RuleFor(x => x.Content).NotNull().Length(BlogPostConsts.MinContentLength, BlogPostConsts.MaxContentLength)
            .WithMessage($"内容长度范围[{BlogPostConsts.MinContentLength},{BlogPostConsts.MaxContentLength}]");

        RuleFor(x => x.Original).MaximumLength(BlogPostConsts.MaxOriginalLength)
            .WithMessage($"来源长度不能大于{BlogPostConsts.MaxOriginalLength}]");

        RuleFor(x => x.OriginalAvatar).MaximumLength(BlogPostConsts.MaxOriginalAvatarLength)
            .WithMessage($"来源作者头像URL长度不能大于{BlogPostConsts.MaxOriginalAvatarLength}]");

        RuleFor(x => x.OriginalTitle).MaximumLength(BlogPostConsts.MaxOriginalTitleLength)
            .WithMessage($"来源文章标题长度不能大于{BlogPostConsts.MaxOriginalTitleLength}]");

        RuleFor(x => x.OriginalLink).MaximumLength(BlogPostConsts.MaxOriginalLinkLength)
            .WithMessage($"来源文章链接长度不能大于{BlogPostConsts.MaxOriginalLinkLength}]");
    }
}