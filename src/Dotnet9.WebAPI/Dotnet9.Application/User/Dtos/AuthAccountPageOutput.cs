namespace Dotnet9.Application.User.Dtos;

public class AuthAccountPageOutput
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 昵称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 性别
    /// </summary>
    public Gender Gender { get; set; }
    /// <summary>
    /// 授权类型
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// 是否是博主
    /// </summary>
    public bool IsBlogger { get; set; }
    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }
    /// <summary>
    /// 注册时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}