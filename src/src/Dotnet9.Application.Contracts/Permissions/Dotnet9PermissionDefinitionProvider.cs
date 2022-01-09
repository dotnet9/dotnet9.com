using Dotnet9.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dotnet9.Permissions;

public class Dotnet9PermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var dotnet9Group = context.AddGroup(Dotnet9Permissions.GroupName, L("Permission:Blog"));

        var tagsPermission = dotnet9Group.AddPermission(Dotnet9Permissions.Tags.Default, L("Permission:Tags"));
        tagsPermission.AddChild(Dotnet9Permissions.Tags.Create, L("Permission:Tags.Create"));
        tagsPermission.AddChild(Dotnet9Permissions.Tags.Edit, L("Permission:Tags.Edit"));
        tagsPermission.AddChild(Dotnet9Permissions.Tags.Delete, L("Permission:Tags.Delete"));

        var albumsPermission = dotnet9Group.AddPermission(Dotnet9Permissions.Albums.Default, L("Permission:Albums"));
        albumsPermission.AddChild(Dotnet9Permissions.Albums.Create, L("Permission:Albums.Create"));
        albumsPermission.AddChild(Dotnet9Permissions.Albums.Edit, L("Permission:Albums.Edit"));
        albumsPermission.AddChild(Dotnet9Permissions.Albums.Delete, L("Permission:Albums.Delete"));

        var aboutsPermission =
            dotnet9Group.AddPermission(Dotnet9Permissions.Abouts.Default, L("Permission:Abouts"));
        aboutsPermission.AddChild(Dotnet9Permissions.Abouts.Edit, L("Permission:Abouts.Edit"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<Dotnet9Resource>(name);
    }
}