namespace Dotnet9.Tools.Web.Models;

public static class TreeItemExt
{
    public static TreeItem ToolToTreeItem(this ToolItem item)
    {
        var treeItem = new TreeItem(TreeItemKind.Tool, item.Name, item.Url, item);
        if (item.Children is not { Count: > 0 }) return treeItem;
        treeItem.Children = new List<TreeItem>();
        item.Children.ForEach(x => { treeItem.Children.Add(x.ToolToTreeItem()); });

        return treeItem;
    }

    public static TreeItem AlbumToTreeItem(this AlbumItem item)
    {
        var treeItem = new TreeItem(TreeItemKind.Album, item.Name, $"album?slug={item.Slug}", item);
        if (item.Children is not { Count: > 0 }) return treeItem;
        treeItem.Children = new List<TreeItem>();
        item.Children.ForEach(x => { treeItem.Children.Add(x.AlbumToTreeItem()); });

        return treeItem;
    }

    public static TreeItem CategoryToTreeItem(this CategoryItem item)
    {
        var treeItem = new TreeItem(TreeItemKind.Category, item.Name, $"cat?slug={item.Slug}", item);
        if (item.Children is not { Count: > 0 }) return treeItem;
        treeItem.Children = new List<TreeItem>();
        item.Children.ForEach(x => { treeItem.Children.Add(x.CategoryToTreeItem()); });

        return treeItem;
    }
}