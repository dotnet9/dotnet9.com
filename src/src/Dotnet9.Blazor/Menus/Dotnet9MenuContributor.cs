using Dotnet9.Localization;
using Dotnet9.Permissions;
using System.Threading.Tasks;
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

            var dotnet9Menu = new ApplicationMenuItem(Dotnet9Menus.Blog, l["Menu:Blog"], icon: "fa fa-book"
            );

            context.Menu.AddItem(dotnet9Menu);

            if (await context.IsGrantedAsync(Dotnet9Permissions.Tags.Default))
            {
                dotnet9Menu.AddItem(new ApplicationMenuItem(Dotnet9Menus.AdminTag, l["Menu:Tags"], "/admin/tag"));
            }

            if (await context.IsGrantedAsync(Dotnet9Permissions.Albums.Default))
            {
                dotnet9Menu.AddItem(new ApplicationMenuItem(Dotnet9Menus.AdminAlbum, l["Menu:Albums"], "/admin/album"));
            }
            if (await context.IsGrantedAsync(Dotnet9Permissions.Categories.Default))
            {
                dotnet9Menu.AddItem(new ApplicationMenuItem(Dotnet9Menus.AdminCategory, l["Menu:Categories"], "/admin/category"));
            }

            if (await context.IsGrantedAsync(Dotnet9Permissions.Abouts.Default))
            {
                dotnet9Menu.AddItem(new ApplicationMenuItem(Dotnet9Menus.AdminAbout, l["Menu:Abouts"], "/admin/about"));
            }

            context.Menu.AddItem(new ApplicationMenuItem(Dotnet9Menus.PublicAbout, l["Menu:About"], icon: "fa fa-book",
                url: "/about"));
        }
    }
}