namespace Dotnet9.Models.Data.Blogs;

public class PostVisitRecord : BaseEntity<Guid>
{
    public Guid PostId { get; set; }

    public Posts Post { get; set; }

    public string IP { get; set; }

    public string UA { get; set; }

    public string UId { get; set; }
}