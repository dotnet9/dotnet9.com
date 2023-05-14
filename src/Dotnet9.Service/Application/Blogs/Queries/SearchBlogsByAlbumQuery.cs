namespace Dotnet9.Service.Application.Blogs.Queries;

public record SearchBlogsByAlbumQuery : ItemsQueryBase<PaginatedListBase<BlogBrief>>
{
    public string AlbumSlug { get; set; } = default!;
    public string? AlbumName { get; set; }

    public override PaginatedListBase<BlogBrief> Result { get; set; } = default!;
}