using Dotnet9.Tools.Web.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace Dotnet9.Tools.Web.Shared;

public partial class PublicCategorySidebar
{
    private List<TreeItem> _items = new();
    [CascadingParameter] public PublicLayout? MainLayout { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var toolJson = await File.ReadAllTextAsync(Path.Combine("wwwroot", "tool.json"));
        var toolListItem = JsonConvert.DeserializeObject<List<ToolItem>>(toolJson);

        var toolTreeItem = new TreeItem(TreeKind.Tool, T("Tools"), Guid.NewGuid().ToString())
        {
            Children = new List<TreeItem>()
        };
        toolListItem.ForEach(x => toolTreeItem.Children.Add(x.ToolToTreeItem()));

        var albumTreeItem = new TreeItem(TreeKind.Tool, T("Album"), Guid.NewGuid().ToString())
        {
            Children = new List<TreeItem>()
        };
        ConstDatas.AlbumItems?.ForEach(x => albumTreeItem.Children.Add(x.AlbumToTreeItem()));

        var blogTreeItem = new TreeItem(TreeKind.Tool, T("Blog"), Guid.NewGuid().ToString())
        {
            Children = new List<TreeItem>()
        };
        ConstDatas.CategoryItems?.ForEach(x => blogTreeItem.Children.Add(x.CategoryToTreeItem()));

        _items.Clear();
        _items.Add(toolTreeItem);
        _items.Add(albumTreeItem);
        _items.Add(blogTreeItem);

        await base.OnInitializedAsync();
    }

    public string T(string key)
    {
        return MainLayout?.T(key) ?? string.Empty;
    }
}