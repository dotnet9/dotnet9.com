namespace Dotnet9.WebAPI.ViewModel.Tags;

public record GetTagListResponse(IEnumerable<TagDto>? Records, long Count);