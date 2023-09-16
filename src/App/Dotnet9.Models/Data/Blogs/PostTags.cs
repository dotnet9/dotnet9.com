namespace Dotnet9.Models.Data.Blogs;

public class PostTags : BaseEntity<Guid>
{
    public PostTags()
    {
    }

    public PostTags(string tagName)
    {
        Id = Guid.NewGuid();
        TagName = tagName;
    }

    public string TagName { get; set; }

    public List<PostTagRelation> TagRelation { get; set; }
}

public class PostTagRelation : BaseEntity<Guid>
{
    public PostTagRelation()
    {
    }

    public PostTagRelation(Posts posts, PostTags postTags)
    {
        Post = posts;
        PostTags = postTags;
    }

    public Posts Post { get; set; }

    public PostTags PostTags { get; set; }
}