using Dotnet9.Core.Enum;

namespace Dotnet9.Core.Entities;

/// <summary>
/// 系统用户表
/// </summary>
public class SysUser : Entity<long>, IUpdatedTime, IAvailability, ICreatedUserId, ISoftDelete, ICreatedTime
{
    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(Length = 32)]
    public string Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [SugarColumn(Length = 64)]
    public string Password { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [SugarColumn(Length = 32)]
    public string Name { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// 组织机构id
    /// </summary>
    public long OrgId { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    [SugarColumn(Length = 32)]
    public string? NickName { get; set; }

    /// <summary>
    /// 生日
    /// </summary>
    public DateOnly? Birthday { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    [SugarColumn(Length = 256)]
    public string? Avatar { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    [SugarColumn(Length = 16)]
    public string? Mobile { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(Length = 64)]
    public string? Email { get; set; }

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }

    /// <summary>
    /// 最后一次登录IP地址
    /// </summary>
    [SugarColumn(Length = 32)]
    public string? LastLoginIp { get; set; }

    /// <summary>
    /// 最后一次登录位置
    /// </summary>
    public string? LastLoginAddress { get; set; }

    /// <summary>
    /// 最后登录时间
    /// </summary>
    public DateTime? LastLoginTime { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(Length = 256)]
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public long CreatedUserId { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdatedTime { get; set; }

    /// <summary>
    /// 标记删除
    /// </summary>
    public bool DeleteMark { get; set; }

    /// <summary>
    /// 账号锁定过期时间
    /// </summary>
    public DateTime? LockExpired { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}