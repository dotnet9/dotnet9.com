namespace Dotnet9.Service.Application.Blogs.Queries;

public record BlogArchivesQuery : ItemsQueryBase<PaginatedListBase<BlogArchive>>
{
    public override PaginatedListBase<BlogArchive> Result { get; set; } = default!;
}