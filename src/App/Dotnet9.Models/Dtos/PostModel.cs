namespace Dotnet9.Models.Dtos;

internal class PostModel
{
}

public class PostCateModel
{
    public string CateName { get; set; }

    public Guid Id { get; set; }
}

public class PostTagModel
{
    public Guid Id { get; set; }

    public string TagName { get; set; }
}

public class PostDetailModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string Thumb { get; set; }

    public string Snippet { get; set; }
    public List<PostTagModel> Tags { get; set; }


    public List<PostCateModel> Cates { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int ReadCount { get; set; }

    public bool IsPublish { get; set; }
}