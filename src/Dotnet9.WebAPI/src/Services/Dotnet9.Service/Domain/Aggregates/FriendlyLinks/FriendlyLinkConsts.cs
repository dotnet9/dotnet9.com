namespace Dotnet9.Service.Domain.Aggregates.FriendlyLinks;

/// <summary>
/// 友情链接关键字段长度限制
/// </summary>
public static class FriendlyLinkConsts
{
    public const int MaxNameLength = 32;
    public const int MinNameLength = 2;
    public const int MaxUrlLength = 256;
    public const int MinUrlLength = 2;
    public const int MaxDescriptionLength = 256;
}