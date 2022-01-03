using System.Threading.Tasks;
using Dotnet9.Localization;
using Dotnet9.MultiTenancy;
using Dotnet9.Permissions;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;

namespace Dotnet9.Blazor.Menus
{
    public class Dotnet9MenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<Dotnet9Resource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    Dotnet9Menus.Home,
                    l["Menu:Home"],
                    "/",
                    icon: "fas fa-home",
                    order: 0
                )
            );

            var dotnet9Menu = new ApplicationMenuItem("Blog", l["Menu:Blog"], icon: "fa fa-book"
            );

            context.Menu.AddItem(dotnet9Menu);

            if (await context.IsGrantedAsync(Dotnet9Permissions.Tags.Default))
            {
                dotnet9Menu.AddItem(new ApplicationMenuItem("Dotnet9.Tags", l["Menu:Tags"], "/admin/tags"));
            }
        }
    }
}