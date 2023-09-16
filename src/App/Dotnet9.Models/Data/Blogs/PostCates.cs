namespace Dotnet9.Models.Data.Blogs;

public class PostCates : BaseEntity<Guid>
{
    public PostCates()
    {
    }

    public PostCates(string cateName)
    {
        Id = Guid.NewGuid();
        CateName = cateName;
    }

    public string CateName { get; set; }

    public List<PostCateRelation> PostCateRelations { get; set; }
}

public class PostCateRelation : BaseEntity<Guid>
{
    public PostCateRelation()
    {
    }

    public PostCateRelation(PostCates postCate, Posts posts)
    {
        PostCate = postCate;
        Post = posts;
    }

    public PostCates PostCate { get; set; }

    public Posts Post { get; set; }
}