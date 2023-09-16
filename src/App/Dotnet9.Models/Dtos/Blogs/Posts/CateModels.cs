namespace Dotnet9.Models.Dtos.Blogs.Posts;

internal class CateModels
{
}

public class CateDtoModel
{
    public Guid Id { get; set; }

    public string CateName { get; set; }

    public int PostCount { get; set; }

    public DateTime CreateTime { get; set; }
}

public class CateRequest
{
    public Guid Id { get; set; }

    public string CateName { get; set; }
}