namespace Dotnet9.WebAPI.ViewModels.Tags;

public record QueryTagResponse(IEnumerable<TagDTO>? Tags, long TotalCount);