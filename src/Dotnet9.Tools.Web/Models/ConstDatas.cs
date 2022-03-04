using Newtonsoft.Json;

namespace Dotnet9.Tools.Web.Models;

internal static class ConstData
{
    public const string DynamicImageApi = "https://api.ixiaowai.cn/gqapi/gqapi.php";
    private static List<AlbumItem>? _albumTreeItems;

    private static List<CategoryItem>? _categoryTreeItems;

    private static List<CategoryItem>? _categoryNotTreeItems;

    private static List<BlogPost>? _blogPostItems;

    private static List<string>? _blogTagItems;

    private static readonly Random CusRandom = new(DateTime.Now.Millisecond);

    private static readonly string[] AllColors =
        {"indigo", "teal", "primary", "secondary", "error", "success", "pink", "red", "green", "amber", "orange"};

    private static readonly string[] AllIcons =
    {
        "mdi-calendar-text", "mdi-magnify", "mdi-star", "mdi-book-variant", "mdi-airballoon", "mdi-buffer",
        "mdi-clipboard-text", "mdi-gesture-tap-button"
    };

    public static List<AlbumItem> AlbumTreeItems
    {
        get

        {
            if (_albumTreeItems != null) return _albumTreeItems;

            var albumJson = File.ReadAllText(Path.Combine("wwwroot", "doc", "blog_contents", "album.json"));
            _albumTreeItems = JsonConvert.DeserializeObject<List<AlbumItem>>(albumJson)!;

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
            _categoryTreeItems = JsonConvert.DeserializeObject<List<CategoryItem>>(categoryJson)!;

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
            _categoryNotTreeItems.RemoveAll(x =>
                !BlogPostItems.Exists(b => b.Categories != null && b.Categories.Contains(x.Name)));

            return _categoryNotTreeItems;
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
                var post = JsonConvert.DeserializeObject<BlogPost>(File.ReadAllText(x))!;
                post.Content = File.ReadAllText(x.Replace(".info", ".md"));
                return post;
            }).ToList();

            return _blogPostItems;
        }
    }

    public static List<string> BlogTagItems
    {
        get
        {
            if (_blogTagItems != null) return _blogTagItems;

            var tags = new HashSet<string>();
            BlogPostItems.ForEach(x =>
            {
                if (x.Tags is {Length: > 0}) x.Tags.ToList().ForEach(y => tags.Add(y));
            });
            _blogTagItems = tags.ToList();

            return _blogTagItems;
        }
    }

    public static string RandomColor => AllColors[CusRandom.Next(AllColors.Length)];

    public static string RandomIcon => AllIcons[CusRandom.Next(AllIcons.Length)];

    private static void ReadChildren(CategoryItem sourceItem, ICollection<CategoryItem> destItemList)
    {
        destItemList.Add(sourceItem);
        if (sourceItem.Children != null && sourceItem.Children.Count > 0)
            sourceItem.Children.ForEach(x => ReadChildren(x, destItemList));
    }

    private const string OwnerSiteUrl = "https://tool.dotnet9.com";
    public static string FormatOwnerSite(string url) => url.StartsWith("/") ? $"{OwnerSiteUrl}{url}" : url;
}