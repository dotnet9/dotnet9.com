namespace Dotnet9.Service.Infrastructure.Repositories;

public static class RegisterRepositoryHelper
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAboutRepository, AboutRepository>();
        services.AddScoped<IActionLogRepository, ActionLogRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IDonationRepository, DonationRepository>();
        services.AddScoped<IPrivacyRepository, PrivacyRepository>();
        services.AddScoped<IFriendlyLinkRepository, FriendlyLinkRepository>();
        services.AddScoped<ITimelineRepository, TimelineRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
    }
}