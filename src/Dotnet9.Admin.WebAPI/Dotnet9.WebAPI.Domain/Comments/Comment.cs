namespace Dotnet9.WebAPI.Domain.Comments;

public record Comment : AggregateRootEntity
{
    private Comment()
    {
    }

    internal Comment(
        Guid id,
        Guid? parentId,
        string url,
        string userName,
        string email,
        string content,
        bool visible = false)
    {
        Id = id;
        ChangeParentId(parentId);
        ChangeUrl(url);
        ChangeUserName(userName);
        ChangeEmail(email);
        ChangeContent(content);
        ChangeVisible(visible);
    }

    public string Url { get; private set; } = null!;

    public Guid? ParentId { get; private set; }

    public string UserName { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string Content { get; private set; } = null!;
    public bool Visible { get; private set; }


    public Comment ChangeUrl(string url)
    {
        Url = Check.NotNullOrWhiteSpace(url, nameof(url), CommentConsts.MaxUrlLength,
            CommentConsts.MinUrlLength);
        return this;
    }

    public Comment ChangeParentId(Guid? parentId)
    {
        ParentId = parentId;
        return this;
    }

    public Comment ChangeUserName(string userName)
    {
        UserName = Check.NotNullOrWhiteSpace(userName, nameof(userName), CommentConsts.MaxUserNameLength,
            CommentConsts.MinUserNameLength);
        return this;
    }

    public Comment ChangeEmail(string email)
    {
        Email = Check.NotNullOrWhiteSpace(email, nameof(email), CommentConsts.MaxEmailLength,
            CommentConsts.MinEmailLength);
        return this;
    }

    public Comment ChangeContent(string content)
    {
        Content = Check.NotNullOrWhiteSpace(content, nameof(content), CommentConsts.MaxContentLength,
            CommentConsts.MinContentLength);
        return this;
    }

    public Comment ChangeVisible(bool visible)
    {
        Visible = visible;
        return this;
    }
}