namespace Dotnet9.WebAPI.ViewModel.Comments;

public record CommentDto(Guid Id, Guid? ParentId, string Url, string UserName, string Email, string Content,
    DateTime CreationTime);