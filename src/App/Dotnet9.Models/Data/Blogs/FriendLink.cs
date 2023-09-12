namespace Dotnet9.Models.Data.Blogs;

public class FriendLink : BaseEntity<int>
{
    public string Name { get; set; }

    public string Url { get; set; }

    public int Order { get; set; }

    /// <summary>
    ///     是否发布
    /// </summary>
    public bool IsPublish { get; set; }
}