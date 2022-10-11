namespace Dotnet9.Web.ViewModel.BlogPosts;

public class GetBlogPostBriefListByAlbumSlugRequestValidator : AbstractValidator<GetBlogPostBriefListByAlbumSlugRequest>
{
    public GetBlogPostBriefListByAlbumSlugRequestValidator()
    {
        RuleFor(e => e.Slug).NotEmpty().WithMessage("专辑别名不能为空");
        RuleFor(e => e.Current).GreaterThan(0).WithMessage("页索引从1开始"); //页号从1开始
        RuleFor(e => e.PageSize).GreaterThanOrEqualTo(5).WithMessage("分页大小需大于等于5");
    }
}