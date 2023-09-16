namespace Dotnet9.Models.Dtos.Blogs.Posts;

internal class TagModels
{
}

public class TagDtoModel
{
    public Guid Id { get; set; }

    public string TagName { get; set; }

    public int Count { get; set; }
}

public class TagListRequest : BasePageModel
{
}