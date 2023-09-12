namespace Dotnet9.Models.Data.Entitys;

public class AccountLoginRecord : BaseEntity<int>
{
    /// <summary>
    ///     账户
    /// </summary>
    public Accounts Account { get; set; }

    public string UA { get; set; }

    public string Ip { get; set; }

    /// <summary>
    ///     信息
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    ///     是否登录成功
    /// </summary>
    public bool IsLogin { get; set; }
}