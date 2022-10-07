namespace Dotnet9.Application.Contracts.Blogs;

public class BlogPostRequest : BasePageRequest
{
    public string? Keyword { get; set; }
}