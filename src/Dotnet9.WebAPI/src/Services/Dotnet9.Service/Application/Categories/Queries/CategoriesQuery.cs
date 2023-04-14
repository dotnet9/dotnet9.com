namespace Dotnet9.Service.Application.Categories.Queries;

public record CategoriesQuery : ItemsQueryBase<PaginatedListBase<CategoryBrief>>
{
    public override PaginatedListBase<CategoryBrief> Result { get; set; } = default!;
}