namespace Dotnet9.WebAPI.ViewModel.BlogPosts;

public enum CopyRightType
{
    [EnumMember(Value = "default")] Default,
    [EnumMember(Value = "contribution")] Contribution,
    [EnumMember(Value = "reprint")] Reprint
}