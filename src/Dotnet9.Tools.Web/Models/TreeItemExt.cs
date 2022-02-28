namespace Dotnet9.Tools.Web.Models;

public static class TreeItemExt
{
    public static TreeItem ToolToTreeItem(this ToolItem item)
    {
        var treeItem = new TreeItem(TreeKind.Tool, item.Name, item.Url, item);
        if (item.Children is not {Count: > 0}) return treeItem;
        treeItem.Children = new List<TreeItem>();
        item.Children.ForEach(x => { treeItem.Children.Add(x.ToolToTreeItem()); });

        return treeItem;
    }

    public static TreeItem AlbumToTreeItem(this AlbumItem item)
    {
        var treeItem = new TreeItem(TreeKind.Album, item.Name, $"album/{item.Slug}", item);
        if (item.Children is not {Count: > 0}) return treeItem;
        treeItem.Children = new List<TreeItem>();
        item.Children.ForEach(x => { treeItem.Children.Add(x.AlbumToTreeItem()); });

        return treeItem;
    }

    public static TreeItem CategoryToTreeItem(this CategoryItem item)
    {
        var treeItem = new TreeItem(TreeKind.Category, item.Name, $"cat/{item.Slug}", item);
        if (item.Children is not {Count: > 0}) return treeItem;
        treeItem.Children = new List<TreeItem>();
        item.Children.ForEach(x => { treeItem.Children.Add(x.CategoryToTreeItem()); });

        return treeItem;
    }
}