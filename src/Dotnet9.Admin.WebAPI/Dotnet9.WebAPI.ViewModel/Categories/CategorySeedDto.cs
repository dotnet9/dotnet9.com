namespace Dotnet9.WebAPI.ViewModel.Categories;

public record CategorySeedDto(string Name, string Slug, string Cover, CategorySeedDto[]? Children);