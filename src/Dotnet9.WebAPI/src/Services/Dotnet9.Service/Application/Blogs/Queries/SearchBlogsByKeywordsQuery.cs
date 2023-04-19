namespace Dotnet9.Service.Application.Blogs.Queries;

public record SearchBlogsByKeywordsQuery : ItemsQueryBase<PaginatedListBase<BlogBrief>>
{
    public string? Keywords { get; set; }

    public override PaginatedListBase<BlogBrief> Result { get; set; } = default!;
}