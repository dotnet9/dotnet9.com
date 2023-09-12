using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Models.Dtos.Blogs.Posts;

internal class PostModel
{
}

public class PostEditRequest
{
    public int Id { get; set; }

    /// <summary>
    ///     标题
    /// </summary>
    [Required]
    public string Title { get; set; }

    /// <summary>
    ///     缩略图
    /// </summary>
    public string? Thumb { get; set; }

    /// <summary>
    ///     内容
    /// </summary>
    [Required]
    public string Content { get; set; }

    /// <summary>
    ///     摘要
    /// </summary>
    [Required]
    public string Snippet { get; set; }

    /// <summary>
    ///     标签
    /// </summary>
    public string TagsStr { get; set; }

    /// <summary>
    ///     标签
    /// </summary>
    public List<string> Tags { get; set; } = new();

    /// <summary>
    ///     分类
    /// </summary>
    public List<int> Cates { get; set; } = new();


    /// <summary>
    ///     是否发布
    /// </summary>
    public bool IsPublish { get; set; }

    public void TagStringToList()
    {
        if (string.IsNullOrWhiteSpace(TagsStr) == false)
        {
            Tags = TagsStr.Split("|").ToList();
        }
    }

    public void TagListToStr()
    {
        TagsStr = string.Join("|", Tags);
    }
}