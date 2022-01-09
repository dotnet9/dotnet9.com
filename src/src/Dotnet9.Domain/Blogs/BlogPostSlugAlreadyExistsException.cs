using Volo.Abp;

namespace Dotnet9.Blogs;

public class BlogPostSlugAlreadyExistsException : BusinessException
{
    public BlogPostSlugAlreadyExistsException(string slug)
        : base(Dotnet9DomainErrorCodes.BlogPosts.SlugAlreadyExist)
    {
        WithData("slug", slug);
    }
}