namespace Dotnet9.Service.Application.Blogs.Queries;

public record GetBlogsOfWeekHotQuery : ItemsQueryBase<PaginatedListBase<BlogBrief>>
{
    public override PaginatedListBase<BlogBrief> Result { get; set; } = default!;
}