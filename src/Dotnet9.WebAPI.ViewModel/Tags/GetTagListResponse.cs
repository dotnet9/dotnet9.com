namespace Dotnet9.WebAPI.ViewModel.Tags;

public record GetTagListResponse(IEnumerable<TagDto>? Tags, long Total);