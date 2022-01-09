using Volo.Abp;

namespace Dotnet9.Blogs;

public class BlogPostTitleAlreadyExistsException : BusinessException
{
    public BlogPostTitleAlreadyExistsException(string title)
        : base(Dotnet9DomainErrorCodes.BlogPosts.TitleAlreadyExist)
    {
        WithData("title", title);
    }
}