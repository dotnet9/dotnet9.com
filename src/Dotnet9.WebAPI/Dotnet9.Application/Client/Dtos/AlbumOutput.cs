namespace Dotnet9.Application.Client.Dtos;

public class AlbumOutput
{
    /// <summary>
    /// 专辑ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 专辑名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 专辑别名
    /// </summary>
    public string Slug { get; set; }

    /// <summary>
    /// 文章条数
    /// </summary>
    public int Total { get; set; }
}