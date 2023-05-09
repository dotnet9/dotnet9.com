namespace Dotnet9.Contracts.Dto.Categories;

public record CategoryBrief(string Name, string Slug, string Cover, string? Description, int BlogCount, Guid? Id);