namespace Dotnet9.Service.Application.Blogs.Queries;

public record SearchBlogsByCategoryQuery : ItemsQueryBase<PaginatedListBase<BlogBrief>>
{
    public string CategorySlug { get; set; } = default!;
    public string? CategoryName { get; set; }

    public override PaginatedListBase<BlogBrief> Result { get; set; } = default!;
}