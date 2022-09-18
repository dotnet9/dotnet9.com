namespace Dotnet9.WebAPI.ViewModel.Tags;

public record GetTagListRequest(string? Keywords, int Current, int PageSize);