namespace Dotnet9.WebAPI.ViewModel.Categories;

public record CategorySeedDto(int SequenceNumber, string Name, string Slug, string Cover, CategorySeedDto[]? Children);