namespace Dotnet9.WebAPI.ViewModels.Categories;

public record CategorySeedDto(string Name, string Slug, string Cover, CategorySeedDto[]? Children);