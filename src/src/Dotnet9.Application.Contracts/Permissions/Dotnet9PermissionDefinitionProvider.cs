using Dotnet9.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dotnet9.Permissions
{
    public class Dotnet9PermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(Dotnet9Permissions.GroupName);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(Dotnet9Permissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<Dotnet9Resource>(name);
        }
    }
}
