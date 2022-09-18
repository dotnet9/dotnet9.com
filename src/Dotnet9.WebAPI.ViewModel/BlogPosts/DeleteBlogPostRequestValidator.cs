namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public class DeleteBlogPostRequestValidator : AbstractValidator<DeleteBlogPostRequest>
{
    public DeleteBlogPostRequestValidator()
    {
        RuleFor(e => e.Ids).Must(e => e != null && e.Any()).WithMessage("删除的ID不能为空");
    }
}