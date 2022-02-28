using Newtonsoft.Json;

namespace Dotnet9.Tools.Web.Models;

public static class ConstDatas
{
    private static List<AlbumItem>? _albumItems;

    private static List<CategoryItem>? _categoryItems;

    private static List<PostInfo>? _postItems;

    public static List<AlbumItem> AlbumItems
    {
        get
        {
            if (_albumItems != null) return _albumItems;

            var albumJson = File.ReadAllText(Path.Combine("wwwroot", "doc", "blog_contents", "album.json"));
            _albumItems = JsonConvert.DeserializeObject<List<AlbumItem>>(albumJson);

            return _albumItems;
        }
    }

    public static List<CategoryItem> CategoryItems
    {
        get
        {
            if (_categoryItems != null) return _categoryItems;

            var categoryJson =
                File.ReadAllText(Path.Combine("wwwroot", "doc", "blog_contents", "category.json"));
            _categoryItems = JsonConvert.DeserializeObject<List<CategoryItem>>(categoryJson);

            return _categoryItems;
        }
    }

    public static List<PostInfo> PostItems
    {
        get
        {
            if (_postItems != null) return _postItems;

            var allPostFiles = Directory.GetFiles(Path.Combine(), "*.info", SearchOption.AllDirectories);
            _postItems = allPostFiles.Select(x =>
            {
                var post = JsonConvert.DeserializeObject<PostInfo>(x);
                post.Content = File.ReadAllText(x.Replace(".info", ".md"));
                return post;
            }).ToList();

            return _postItems;
        }
    }
}