namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public class GetBlogPostsByCategoryRequestValidator : AbstractValidator<GetBlogPostsByCategoryRequest>
{
    public GetBlogPostsByCategoryRequestValidator()
    {
        RuleFor(e => e.Current).GreaterThan(0).WithMessage("页索引从1开始"); //页号从1开始
        RuleFor(e => e.PageSize).GreaterThanOrEqualTo(5).WithMessage("分页大小需大于等于5");
    }
}