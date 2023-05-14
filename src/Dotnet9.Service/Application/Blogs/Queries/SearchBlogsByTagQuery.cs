namespace Dotnet9.Service.Application.Blogs.Queries;

public record SearchBlogsByTagQuery : ItemsQueryBase<PaginatedListBase<BlogBrief>>
{
    public string TagName { get; set; } = default!;

    public override PaginatedListBase<BlogBrief> Result { get; set; } = default!;
}