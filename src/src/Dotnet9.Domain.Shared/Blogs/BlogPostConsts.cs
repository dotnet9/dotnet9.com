namespace Dotnet9.Blogs;

public static class BlogPostConsts
{
    public static int MaxTitleLength { get; set; } = 64;

    public static int MaxShortDescriptionLength { get; set; } = 256;

    public static int MaxContentLength { get; set; } = int.MaxValue;

    public const string EntityType = "BlogPost";
}