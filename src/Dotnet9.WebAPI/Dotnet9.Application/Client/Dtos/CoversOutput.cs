namespace Dotnet9.Application.Client.Dtos;

public class CoversOutput
{
    /// <summary>
    /// 模块ID
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 模块名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 模块封面
    /// </summary>
    public string Cover { get; set; }
    
    /// <summary>
    /// 模块描述
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}