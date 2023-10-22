namespace Dotnet9.Core.Enum;
/// <summary>
/// 性别
/// </summary>
public enum Gender
{
    /// <summary>
    /// 男
    /// </summary>
    [Description("男")]
    Male,
    /// <summary>
    /// 女
    /// </summary>
    [Description("女")]
    Female,
    /// <summary>
    /// 未知
    /// </summary>
    [Description("保密")]
    Unknown
}