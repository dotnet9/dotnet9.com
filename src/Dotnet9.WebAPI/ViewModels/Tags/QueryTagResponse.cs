namespace Dotnet9.WebAPI.ViewModels.Tags;

public record QueryTagResponse(IEnumerable<TagDto>? Tags, long TotalCount);