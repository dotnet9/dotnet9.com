using Dotnet9.Tools.Web.Models;
using Dotnet9.Tools.Web.Shared;
using Microsoft.AspNetCore.Components;

namespace Dotnet9.Tools.Web.Pages.Public;

public partial class ItemList
{
    [Parameter] public string? Title { get; set; }
    [Parameter] public List<ItemInfo>? Items { get; set; }
    [CascadingParameter] public PublicLayout? MainLayout { get; set; }

    public string T(string key)
    {
        return MainLayout?.T(key) ?? string.Empty;
    }
}