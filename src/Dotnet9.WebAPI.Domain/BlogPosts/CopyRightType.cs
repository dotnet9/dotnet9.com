namespace Dotnet9.WebAPI.Domain.BlogPosts;

public enum CopyRightType
{
    [EnumMember(Value = "default")] Default,
    [EnumMember(Value = "contribution")] Contribution,
    [EnumMember(Value = "reprint")] Reprint
}