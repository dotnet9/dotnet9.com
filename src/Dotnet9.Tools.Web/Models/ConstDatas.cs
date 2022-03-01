using Newtonsoft.Json;

namespace Dotnet9.Tools.Web.Models;

public static class ConstDatas
{
    private static List<AlbumItem>? _albumTreeItems;

    private static List<CategoryItem>? _categoryTreeItems;

    private static List<CategoryItem>? _categoryNotTreeItems;

    private static List<BlogPost>? _blogPostItems;

    public static List<AlbumItem> AlbumTreeItems
    {
        get
        {
            if (_albumTreeItems != null) return _albumTreeItems;

            var albumJson = File.ReadAllText(Path.Combine("wwwroot", "doc", "blog_contents", "album.json"));
            _albumTreeItems = JsonConvert.DeserializeObject<List<AlbumItem>>(albumJson);

            return _albumTreeItems;
        }
    }

    public static List<CategoryItem> CategoryTreeItems
    {
        get
        {
            if (_categoryTreeItems != null) return _categoryTreeItems;

            var categoryJson =
                File.ReadAllText(Path.Combine("wwwroot", "doc", "blog_contents", "category.json"));
            _categoryTreeItems = JsonConvert.DeserializeObject<List<CategoryItem>>(categoryJson);

            return _categoryTreeItems;
        }
    }

    public static List<CategoryItem> CategoryNotTreeItems
    {
        get
        {
            if (_categoryNotTreeItems != null) return _categoryNotTreeItems;
            _categoryNotTreeItems = new List<CategoryItem>();
            CategoryTreeItems.ForEach(x => ReadChildren(x, _categoryNotTreeItems));

            return _categoryNotTreeItems;
        }
    }

    private static void ReadChildren(CategoryItem sourceItem, List<CategoryItem> destItemList)
    {
        destItemList.Add(sourceItem);
        if (sourceItem.Children != null && sourceItem.Children.Count > 0)
        {
            sourceItem.Children.ForEach(x => ReadChildren(x, destItemList));
        }
    }

    public static List<BlogPost> BlogPostItems
    {
        get
        {
            if (_blogPostItems != null) return _blogPostItems;

            var allPostFiles = Directory.GetFiles(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", "doc", "blog_contents", "uploads"),
                "*.info",
                SearchOption.AllDirectories);
            _blogPostItems = allPostFiles.Select(x =>
            {
                var post = JsonConvert.DeserializeObject<BlogPost>(File.ReadAllText(x));
                post.Content = File.ReadAllText(x.Replace(".info", ".md"));
                return post;
            }).ToList();

            return _blogPostItems;
        }
    }
}