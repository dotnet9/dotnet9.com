namespace Dotnet9.Service.Application.Tags.Queries;

public record TagQuery : ItemsQueryBase<PaginatedListBase<TagBrief>>
{
    public override PaginatedListBase<TagBrief> Result { get; set; } = default!;
}