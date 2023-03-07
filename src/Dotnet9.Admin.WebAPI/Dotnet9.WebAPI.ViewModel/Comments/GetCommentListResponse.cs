namespace Dotnet9.WebAPI.ViewModel.Comments;

public record GetCommentListResponse(IEnumerable<CommentDto>? Records, long Count);