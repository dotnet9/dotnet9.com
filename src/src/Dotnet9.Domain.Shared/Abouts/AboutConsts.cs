using System.Collections.Generic;

namespace Dotnet9.Abouts;

public static class AboutConsts
{
    public const int MaxDetailsLength = 5 * 1024;
}

public class ConfigurationConst
{
    public const string AboutFilePath = "F://github_gitee/dotnet9.com//doc/blog_contents/about.md";
    public const string BlogBaseFilePath = "F://github_gitee/dotnet9.com/doc/blog_contents/base.info";
    public const string BlogPostDirPath = "F://github_gitee/dotnet9.com/doc/blog_contents/uploads";
}

public class BaseItem
{
    public string Name { get; set; }

    public string Cover { get; set; }
}

public class BlogBaseDto
{
    public List<BaseItem> Albums { get; set; }
}