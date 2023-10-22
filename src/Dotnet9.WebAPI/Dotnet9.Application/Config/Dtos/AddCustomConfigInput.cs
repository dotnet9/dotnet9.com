namespace Dotnet9.Application.Config.Dtos;

public class AddCustomConfigInput
{
    /// <summary>
    /// 配置名称
    /// </summary>
    [MaxLength(32, ErrorMessage = "配置名称限制32个字符内")]
    [Required(ErrorMessage = "配置名称为必填项")]
    public string Name { get; set; }

    /// <summary>
    /// 配置唯一编码（类名）
    /// </summary>
    [MaxLength(32, ErrorMessage = "编码限制32个字符内")]
    [Required(ErrorMessage = "编码为必填项")]
    public string Code { get; set; }

    /// <summary>
    /// 是否是多项（集合）
    /// </summary>
    public bool IsMultiple { get; set; }

    /// <summary>
    /// 是否允许创建实体
    /// </summary>
    public bool AllowCreationEntity { get; set; }

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(200, ErrorMessage = "备注限制200字符内")]
    public string Remark { get; set; }
}