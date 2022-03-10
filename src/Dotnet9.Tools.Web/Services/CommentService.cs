using Dotnet9.Tools.Web.Models.Comments;
using Dotnet9.Tools.Web.ViewModels.Comments;

namespace Dotnet9.Tools.Web.Services;

public class CommentService
{
    private static readonly List<CommentViewModel> _comments = new();

    public async Task<List<CommentViewModel>> GetCommentsAsync(string urlMark)
    {
        if (_comments.Count > 0) return _comments;

        var rd = new Random(DateTimeOffset.Now.Millisecond);
        for (var i = 0; i < rd.Next(5, 50); i++)
        {
            var item1 = new CommentViewModel
            {
                Id = i.ToString(), UrlMark = urlMark, UserName = $"user {i + 1}",
                Timestamp = DateTimeOffset.UtcNow.GetTimestamp(), Content = "test content1"
            };
            _comments.Add(item1);

            if (i > 0)
            {
                item1.ReplyKind = (CommentReplyKind) rd.Next(Enum.GetNames(typeof(CommentReplyKind)).Length);
                if (item1.ReplyKind == CommentReplyKind.Default) continue;
                item1.ReplyId = _comments[i - 1].Id;
                item1.Reply = new CommentViewModel
                {
                    Id = $"{i}+j",
                    UrlMark = urlMark,
                    UserName = $"user {i + 1}-2",
                    Timestamp = DateTimeOffset.UtcNow.GetTimestamp(),
                    Content = "test content1-1",
                    ReplyKind = CommentReplyKind.Default
                };
            }
            else
            {
                item1.ReplyKind = CommentReplyKind.Default;
            }
        }

        await Task.CompletedTask;

        return _comments;
    }

    public async Task<bool> Add(CommentForCreationViewModel comment)
    {
        _comments.Add(new CommentViewModel
        {
            Id = comment.Id,
            UrlMark = comment.UrlMark,
            UserName = comment.UserName,
            Timestamp = comment.Timestamp,
            Content = comment.Content,
            ReplyKind = comment.ReplyKind,
            ReplyId = comment.ReplyId
        });
        return await Task.FromResult(true);
    }
}