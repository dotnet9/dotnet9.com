namespace Dotnet9.Service.Domain.Aggregates.Blogs;

public enum CopyRightType
{
    [EnumMember(Value = "default")] Default,
    [EnumMember(Value = "contribution")] Contribution,
    [EnumMember(Value = "reprint")] Reprint
}

public static class CopyRightTypeExtensions
{
    public static string GetDescription(this CopyRightType copyRightType)
    {
        return copyRightType switch
        {
            CopyRightType.Default => "原创",
            CopyRightType.Reprint => "转载",
            _ => "投稿"
        };
    }
}