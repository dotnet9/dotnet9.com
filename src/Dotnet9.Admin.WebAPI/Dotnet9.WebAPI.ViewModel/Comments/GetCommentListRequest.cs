namespace Dotnet9.WebAPI.ViewModel.Comments;

public record GetCommentListRequest(string Url, int Current, int PageSize);