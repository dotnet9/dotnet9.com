namespace Dotnet9.Commons.FileHelpers;

public static class MarkdownHelper
{
    private const string Title = "title: ";
    private const string Slug = "slug: ";
    private const string Description = "description: ";
    private const string Banner = "banner: ";
    private const string Date = "date: ";
    private const string LastModifyDate = "lastmod: ";
    private const string Copyright = "copyright: ";
    private const string Author = "author: ";
    private const string OriginalTitle = "originaltitle: ";
    private const string OriginalLink = "originallink: ";
    private const string Draft = "draft: ";
    private const string Cover = "cover: ";
    private const string Albums = "albums: ";
    private const string Categories = "categories: ";
    private const string Tags = "tags: ";

    public static PostOfMarkdown Read(string markdownAbsolutePath)
    {
        var postInfo = new PostOfMarkdown();
        var lines = File.ReadAllLines(markdownAbsolutePath);
        var isReadStart = false;
        for (var i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("---"))
            {
                if (isReadStart == false)
                {
                    isReadStart = true;
                    continue;
                }

                var contentLineStartOfAllLines = i + 2;
                var contentLineLength = lines.Length - contentLineStartOfAllLines;
                var contentLines = new string[contentLineLength];
                Array.Copy(lines, contentLineStartOfAllLines, contentLines, 0, contentLineLength);
                postInfo.Content = string.Join("\n", contentLines);
                return postInfo;
            }

            if (lines[i].StartsWith(Title))
            {
                postInfo.Title = lines[i][Title.Length..];
            }
            else if (lines[i].StartsWith(Slug))
            {
                postInfo.Slug = lines[i][Slug.Length..];
            }
            else if (lines[i].StartsWith(Description))
            {
                postInfo.Description = lines[i][Description.Length..];
            }
            else if (lines[i].StartsWith(Banner))
            {
                postInfo.Banner = bool.Parse(lines[i][Banner.Length..]);
            }
            else if (lines[i].StartsWith(Date))
            {
                postInfo.Date = DateTime.Parse(lines[i][Date.Length..]);
            }
            else if (lines[i].StartsWith(LastModifyDate))
            {
                postInfo.LastModifyDate = DateTime.Parse(lines[i][LastModifyDate.Length..]);
            }
            else if (lines[i].StartsWith(Copyright))
            {
                postInfo.Copyright = (CopyRightType)Enum.Parse(typeof(CopyRightType), lines[i][Copyright.Length..]);
            }
            else if (lines[i].StartsWith(Author))
            {
                postInfo.Author = lines[i][Author.Length..];
            }
            else if (lines[i].StartsWith(OriginalTitle))
            {
                postInfo.OriginalTitle = lines[i][OriginalTitle.Length..];
            }
            else if (lines[i].StartsWith(OriginalLink))
            {
                postInfo.OriginalLink = lines[i][OriginalLink.Length..];
            }
            else if (lines[i].StartsWith(Draft))
            {
                postInfo.Draft = bool.Parse(lines[i][Draft.Length..]);
            }
            else if (lines[i].StartsWith(Cover))
            {
                postInfo.Cover = lines[i][Cover.Length..];
            }
            else if (lines[i].StartsWith(Albums))
            {
                postInfo.Albums = lines[i][Albums.Length..].Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
            else if (lines[i].StartsWith(Categories))
            {
                postInfo.Categories = lines[i][Categories.Length..].Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
            else if (lines[i].StartsWith(Tags))
            {
                postInfo.Tags = lines[i][Tags.Length..].Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
        }

        return postInfo;
    }

    public static void Write(string markdownAbsolutePath, PostOfMarkdown postOfMarkdown)
    {
        var lines = new List<string>();
        lines.Add("---");

        if (!string.IsNullOrWhiteSpace(postOfMarkdown.Title))
        {
            lines.Add($"{Title}{postOfMarkdown.Title}");
        }

        if (!string.IsNullOrWhiteSpace(postOfMarkdown.Slug))
        {
            lines.Add($"{Slug}{postOfMarkdown.Slug}");
        }

        if (!string.IsNullOrWhiteSpace(postOfMarkdown.Description))
        {
            lines.Add($"{Description}{postOfMarkdown.Description}");
        }

        if (postOfMarkdown.Date != default)
        {
            lines.Add($"{Date}{postOfMarkdown.Date:yyyy-MM-dd HH:mm:ss}");
        }

        if (postOfMarkdown.LastModifyDate != null && postOfMarkdown.LastModifyDate != default(DateTime))
        {
            lines.Add($"{LastModifyDate}{postOfMarkdown.LastModifyDate:yyyy-MM-dd HH:mm:ss}");
        }

        lines.Add($"{Copyright}{postOfMarkdown.Copyright}");
        if (!string.IsNullOrWhiteSpace(postOfMarkdown.Author))
        {
            lines.Add($"{Author}{postOfMarkdown.Author}");
        }

        if (!string.IsNullOrWhiteSpace(postOfMarkdown.OriginalTitle))
        {
            lines.Add($"{OriginalTitle}{postOfMarkdown.OriginalTitle}");
        }

        if (!string.IsNullOrWhiteSpace(postOfMarkdown.OriginalLink))
        {
            lines.Add($"{OriginalLink}{postOfMarkdown.OriginalLink}");
        }

        lines.Add($"{Draft}{postOfMarkdown.Draft}");

        if (!string.IsNullOrWhiteSpace(postOfMarkdown.Cover))
        {
            lines.Add($"{Cover}{postOfMarkdown.Cover}");
        }

        if (postOfMarkdown.Albums is { Length: > 0 })
        {
            lines.Add($"{Albums}{string.Join(',', postOfMarkdown.Albums)}");
        }

        if (postOfMarkdown.Categories is { Length: > 0 })
        {
            lines.Add($"{Categories}{string.Join(',', postOfMarkdown.Categories)}");
        }

        if (postOfMarkdown.Tags is { Length: > 0 })
        {
            lines.Add($"{Tags}{string.Join(',', postOfMarkdown.Tags)}");
        }

        lines.Add("---");

        if (File.Exists(markdownAbsolutePath))
        {
            File.Delete(markdownAbsolutePath);
        }

        File.WriteAllLines(markdownAbsolutePath, lines);
        File.AppendAllText(markdownAbsolutePath, $"\n{postOfMarkdown.Content}");
    }

    public static void UpgradeMarkdown2(string sourceDir)
    {
        var markdownV1InfoFiles = Directory.GetFiles(sourceDir, "*.info", SearchOption.AllDirectories);
        foreach (var markdownV1InfoFile in markdownV1InfoFiles)
        {
            var markdownV1ContentFile = markdownV1InfoFile.Replace(".info", ".md");
            var infoJson = File.ReadAllText(markdownV1InfoFile);
            var contentMarkdown = File.ReadAllText(markdownV1ContentFile);
            var infoObj = JsonSerializer.Deserialize<PostOfMarkdownV1>(infoJson);
            infoObj!.Content = contentMarkdown;

            var markdownV2 = MarkdownV1ToMarkdownV2(infoObj!);
            File.Delete(markdownV1InfoFile);
            File.Delete(markdownV1ContentFile);
            var markdownV2FilePath = $"{Path.GetDirectoryName(markdownV1InfoFile)}/{infoObj.Slug}.md";
            Write(markdownV2FilePath, markdownV2);
        }
    }

    public static PostOfMarkdown MarkdownV1ToMarkdownV2(PostOfMarkdownV1 postOfMarkdownV1)
    {
        return new PostOfMarkdown
        {
            Title = postOfMarkdownV1.Title,
            Slug = postOfMarkdownV1.Slug,
            Description = postOfMarkdownV1.BriefDescription,
            Date = DateTime.Parse(postOfMarkdownV1.CreateDate!),
            Banner = postOfMarkdownV1.Banner,
            Copyright = (CopyRightType)Enum.Parse(typeof(CopyRightType), postOfMarkdownV1.CopyrightType!),
            Author = postOfMarkdownV1.CopyrightType == default ? "沙漠尽头的狼" : postOfMarkdownV1.Original,
            OriginalTitle = postOfMarkdownV1.CopyrightType == default ? null : postOfMarkdownV1.Title,
            OriginalLink = postOfMarkdownV1.OriginalLink,
            Draft = false,
            Cover = postOfMarkdownV1.Cover,
            Albums = postOfMarkdownV1.Albums,
            Categories = postOfMarkdownV1.Categories,
            Tags = postOfMarkdownV1.Tags,
            Content = postOfMarkdownV1.Content
        };
    }
}