namespace Dotnet9.Models.Data.Blogs;

public class PostCates : BaseEntity<int>
{
    public PostCates()
    {
    }

    public PostCates(string cateName)
    {
        CateName = cateName;
    }

    public string CateName { get; set; }

    public List<PostCateRelation> PostCateRelations { get; set; }
}

public class PostCateRelation : BaseEntity<int>
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