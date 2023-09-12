namespace Dotnet9.Models.Data.Entitys;

/// <summary>
///     系统配置表
/// </summary>
public class SysConfig : BaseEntity<int>
{
    /// <summary>
    ///     key
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    ///     value
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    ///     描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     分组名称
    /// </summary>
    public string? GroupName { get; set; }
}

/// <summary>
///     配置
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ConfigDescAttribute : Attribute
{
    public ConfigDescAttribute(string description, object defaultValue)
    {
        Description = description;
        DefaultValue = defaultValue;
    }

    /// <summary>
    ///     描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     默认值
    /// </summary>
    public object DefaultValue { get; set; }
}