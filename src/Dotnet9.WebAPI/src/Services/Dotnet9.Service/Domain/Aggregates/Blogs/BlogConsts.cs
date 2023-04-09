namespace Dotnet9.Service.Domain.Aggregates.Blogs;

public static class BlogConsts
{
    public const int MaxTitleLength = 64;

    public const int MinTitleLength = 5;

    public const int MaxSlugLength = 256;

    public const int MinSlugLength = 2;

    public const int MaxDescriptionLength = 256;

    public const int MinDescriptionLength = 2;

    public const int MaxCoverLength = 128;
    public const int MinCoverLength = 2;

    public const int MaxContentLength = int.MaxValue;
    public const int MinContentLength = 10;

    public const int MaxOriginalLength = 128;

    public const int MaxOriginalAvatarLength = 128;

    public const int MaxOriginalTitleLength = 128;

    public const int MaxOriginalLinkLength = 256;
}