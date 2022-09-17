namespace Dotnet9.WebAPI.Domain.Links;

public enum LinkKind
{
    [Description("私密")] [EnumMember(Value = "private")]
    Private,

    [Description("网站相关")] [EnumMember(Value = "owner")]
    Owner,

    [Description("友情链接")] [EnumMember(Value = "friend")]
    Friend,

    [Description("课程链接")] [EnumMember(Value = "course")]
    Course
}