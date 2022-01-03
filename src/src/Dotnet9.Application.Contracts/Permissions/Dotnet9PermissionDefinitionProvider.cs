using Dotnet9.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dotnet9.Permissions
{
    public class Dotnet9PermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var dotnet9Group = context.AddGroup(Dotnet9Permissions.GroupName, L("Permission:Dotnet9"));

            var tagsPermission = dotnet9Group.AddPermission(Dotnet9Permissions.Tags.Default, L("Permission:Tags"));
            tagsPermission.AddChild(Dotnet9Permissions.Tags.Create, L("Permission:Tags.Create"));
            tagsPermission.AddChild(Dotnet9Permissions.Tags.Edit, L("Permission:Tags.Edit"));
            tagsPermission.AddChild(Dotnet9Permissions.Tags.Delete, L("Permission:Tags.Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<Dotnet9Resource>(name);
        }
    }
}