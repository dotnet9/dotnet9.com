using Dotnet9.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dotnet9.Permissions;

public class Dotnet9PermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var dotnet9Group = context.AddGroup(Dotnet9Permissions.GroupName, L("Permission:Blog"));

        var aboutsPermission =
            dotnet9Group.AddPermission(Dotnet9Permissions.Abouts.Default, L("Permission:Abouts"));
        aboutsPermission.AddChild(Dotnet9Permissions.Abouts.Edit, L("Permission:Abouts.Edit"));

        var albumsPermission = dotnet9Group.AddPermission(Dotnet9Permissions.Albums.Default, L("Permission:Albums"));
        albumsPermission.AddChild(Dotnet9Permissions.Albums.Create, L("Permission:Albums.Create"));
        albumsPermission.AddChild(Dotnet9Permissions.Albums.Edit, L("Permission:Albums.Edit"));
        albumsPermission.AddChild(Dotnet9Permissions.Albums.Delete, L("Permission:Albums.Delete"));

        var blogPostsPermission = dotnet9Group.AddPermission(Dotnet9Permissions.BlogPosts.Default, L("Permission:BlogPosts"));
        blogPostsPermission.AddChild(Dotnet9Permissions.BlogPosts.Create, L("Permission:BlogPosts.Create"));
        blogPostsPermission.AddChild(Dotnet9Permissions.BlogPosts.Edit, L("Permission:BlogPosts.Edit"));
        blogPostsPermission.AddChild(Dotnet9Permissions.BlogPosts.Delete, L("Permission:BlogPosts.Delete"));

        var categoriesPermission = dotnet9Group.AddPermission(Dotnet9Permissions.Categories.Default, L("Permission:Categories"));
        categoriesPermission.AddChild(Dotnet9Permissions.Categories.Create, L("Permission:Categories.Create"));
        categoriesPermission.AddChild(Dotnet9Permissions.Categories.Edit, L("Permission:Categories.Edit"));
        categoriesPermission.AddChild(Dotnet9Permissions.Categories.Delete, L("Permission:Categories.Delete"));

        var commentsPermission = dotnet9Group.AddPermission(Dotnet9Permissions.Comments.Default, L("Permission:Comments"));
        commentsPermission.AddChild(Dotnet9Permissions.Comments.Create, L("Permission:Comments.Create"));
        commentsPermission.AddChild(Dotnet9Permissions.Comments.Edit, L("Permission:Comments.Edit"));
        commentsPermission.AddChild(Dotnet9Permissions.Comments.Delete, L("Permission:Comments.Delete"));

        var contactsPermission = dotnet9Group.AddPermission(Dotnet9Permissions.Contacts.Default, L("Permission:Contacts"));
        contactsPermission.AddChild(Dotnet9Permissions.Contacts.Create, L("Permission:Contacts.Create"));
        contactsPermission.AddChild(Dotnet9Permissions.Contacts.Edit, L("Permission:Contacts.Edit"));
        contactsPermission.AddChild(Dotnet9Permissions.Contacts.Delete, L("Permission:Contacts.Delete"));

        var privaciesPermission =
            dotnet9Group.AddPermission(Dotnet9Permissions.Privacies.Default, L("Permission:Privacies"));
        privaciesPermission.AddChild(Dotnet9Permissions.Privacies.Edit, L("Permission:Privacies.Edit"));

        var ratingsPermission = dotnet9Group.AddPermission(Dotnet9Permissions.Ratings.Default, L("Permission:Ratings"));
        ratingsPermission.AddChild(Dotnet9Permissions.Ratings.Create, L("Permission:Ratings.Create"));
        ratingsPermission.AddChild(Dotnet9Permissions.Ratings.Edit, L("Permission:Ratings.Edit"));
        ratingsPermission.AddChild(Dotnet9Permissions.Ratings.Delete, L("Permission:Ratings.Delete"));

        var tagsPermission = dotnet9Group.AddPermission(Dotnet9Permissions.Tags.Default, L("Permission:Tags"));
        tagsPermission.AddChild(Dotnet9Permissions.Tags.Create, L("Permission:Tags.Create"));
        tagsPermission.AddChild(Dotnet9Permissions.Tags.Edit, L("Permission:Tags.Edit"));
        tagsPermission.AddChild(Dotnet9Permissions.Tags.Delete, L("Permission:Tags.Delete"));

        var urlLinksPermission = dotnet9Group.AddPermission(Dotnet9Permissions.UrlLinks.Default, L("Permission:UrlLinks"));
        urlLinksPermission.AddChild(Dotnet9Permissions.UrlLinks.Create, L("Permission:UrlLinks.Create"));
        urlLinksPermission.AddChild(Dotnet9Permissions.UrlLinks.Edit, L("Permission:UrlLinks.Edit"));
        urlLinksPermission.AddChild(Dotnet9Permissions.UrlLinks.Delete, L("Permission:UrlLinks.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<Dotnet9Resource>(name);
    }
}