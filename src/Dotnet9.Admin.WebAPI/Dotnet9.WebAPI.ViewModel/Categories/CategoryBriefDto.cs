namespace Dotnet9.WebAPI.ViewModel.Categories;

public record CategoryBriefDto(Guid Id, int SequenceNumber, string Name, int BlogPostCount, string Slug);