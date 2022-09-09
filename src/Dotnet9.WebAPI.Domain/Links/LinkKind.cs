namespace Dotnet9.WebAPI.Domain.Links;

public enum LinkKind
{
    [Description("私密")] Private,
    [Description("网站相关")] Owner,
    [Description("友情链接")] Friend,
    [Description("课程链接")] Course
}