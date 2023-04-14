namespace Dotnet9.Service.Application.Albums.Queries;

public record AlbumsQuery : ItemsQueryBase<PaginatedListBase<AlbumBrief>>
{
    public override PaginatedListBase<AlbumBrief> Result { get; set; } = default!;
}