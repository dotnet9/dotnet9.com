namespace Dotnet9.WebAPI.ViewModel.Tags;

public record TagDto(Guid Id, string Name, int BlogPostCount, DateTime CreationTime);