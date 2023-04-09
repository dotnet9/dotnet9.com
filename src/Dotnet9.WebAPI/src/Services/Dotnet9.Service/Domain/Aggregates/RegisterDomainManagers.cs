namespace Dotnet9.Service.Domain.Aggregates;

public static class RegisterDomainManagerHelper
{
    public static void RegisterDomainManagers(this IServiceCollection services)
    {
        services.AddScoped<AboutManager>();
        services.AddScoped<ActionLogManager>();
        services.AddScoped<CategoryManager>();
        services.AddScoped<AlbumManager>();
        services.AddScoped<TagManager>();
        services.AddScoped<BlogManager>();
        services.AddScoped<DonationManager>();
        services.AddScoped<PrivacyManager>();
        services.AddScoped<FriendlyLinkManager>();
        services.AddScoped<TimelineManager>();
        services.AddScoped<CommentManager>();
    }
}