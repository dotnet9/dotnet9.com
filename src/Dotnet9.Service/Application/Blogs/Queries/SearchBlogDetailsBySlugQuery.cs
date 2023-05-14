namespace Dotnet9.Service.Application.Blogs.Queries;

public record SearchBlogDetailsBySlugQuery : Query<BlogDetails>
{
    public string Slug { get; set; } = default!;

    public override BlogDetails Result { get; set; } = default!;
}