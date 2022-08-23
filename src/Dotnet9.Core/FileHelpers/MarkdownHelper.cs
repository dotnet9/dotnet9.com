namespace Dotnet9.Core.FileHelpers;

public static class MarkdownHelper
{
    private const string Title = "title: ";
    private const string Slug = "slug: ";
    private const string Description = "description: ";
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

        if (postOfMarkdown.LastModifyDate != default(DateTime))
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
        File.AppendAllText(markdownAbsolutePath, postOfMarkdown.Content);
    }
}