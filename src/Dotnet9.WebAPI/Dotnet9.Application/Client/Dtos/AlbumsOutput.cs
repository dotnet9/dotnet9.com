namespace Dotnet9.Application.Client.Dtos;

public class AlbumsOutput
{
    /// <summary>
    /// 相册ID
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 相册名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 相册封面
    /// </summary>
    public string Cover { get; set; }
    
    /// <summary>
    /// 相册描述
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}