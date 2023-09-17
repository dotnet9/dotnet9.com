namespace Dotnet9.WebAPI.Events;

public record LikeBlogPostEvent(string Slug, int
    LikeCount) : INotification;