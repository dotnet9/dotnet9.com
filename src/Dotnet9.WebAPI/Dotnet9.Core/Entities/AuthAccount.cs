using Dotnet9.Core.Enum;

namespace Dotnet9.Core.Entities;
/// <summary>
/// 博客授权用户
/// </summary>
public class AuthAccount : Entity<long>, IUpdatedTime, ISoftDelete, ICreatedTime
{

    /// <summary>
    /// 授权唯一标识
    /// </summary>
    public string OAuthId { get; set; }

    /// <summary>
    /// 授权类型
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// 博主标识
    /// </summary>
    public bool IsBlogger { get; set; }

    /// <summary>
    /// 姓名（昵称）
    /// </summary>
    [SugarColumn(Length = 64)]
    public string? Name { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    [SugarColumn(Length = 256)]
    public string? Avatar { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(Length = 128)]
    public string? Email { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdatedTime { get; set; }

    /// <summary>
    /// 标记删除
    /// </summary>
    public bool DeleteMark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }

}