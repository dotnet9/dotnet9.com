namespace Dotnet9.Application.Organization.Dtos;

public class AddOrgInput
{
    /// <summary>
    /// 父级Id
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    [MaxLength(32, ErrorMessage = "组织机构名称限制32个字符内")]
    [Required(ErrorMessage = "部门名称为必填项")]
    public string Name { get; set; }

    /// <summary>
    /// 部门编码
    /// </summary>
    [MaxLength(64, ErrorMessage = "组织机构编码限制64个字符内")]
    public string Code { get; set; }

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }

    /// <summary>
    /// 排序值（值越小越靠前）
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(200, ErrorMessage = "备注限制200个字符内")]
    public string Remark { get; set; }
}