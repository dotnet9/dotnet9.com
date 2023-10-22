namespace Dotnet9.Application.Config.Dtos;

public class CustomConfigPageOutput
{
    /// <summary>
    /// 自定义配置Id
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
    /// <summary>
    /// 配置名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 唯一编码
    /// </summary>
    public string Code { get; set; }
    /// <summary>
    /// 是否是多项配置
    /// </summary>
    public bool IsMultiple { get; set; }
    /// <summary>
    /// 是否允许创建实体
    /// </summary>
    public bool AllowCreationEntity { get; set; }
    /// <summary>
    /// 是否已生成实体
    /// </summary>
    public bool IsGenerate { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}