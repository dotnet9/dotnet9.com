namespace Dotnet9.Models.Dtos.Blogs;

public class PostItemModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Thumb { get; set; }

    public string Content { get; set; }

    public string Snippet { get; set; }


    public DateTime LastUpdateTime { get; set; }

    public int ReadCount { get; set; }

    public int CommentCount { get; set; }

    public List<TagItem> TagItems { get; set; } = new();

    public List<CateItem> CateItems { get; set; } = new();

    public bool IsTop { get; set; }

    public bool IsPublish { get; set; }
}

public class TagItem
{
    public Guid Id { get; set; }

    public string TagName { get; set; }
}

public class TagCountItem : TagItem
{
    public int Count { get; set; }
}

public class CateItem
{
    public Guid Id { get; set; }
    public string CateName { get; set; }
}

public class CateCountItem : CateItem
{
    public int Count { get; set; }
}