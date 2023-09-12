using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Models.Data.Entitys;

public class SysResource : BaseEntity<long>
{
    public Accounts? Account { get; set; }

    /// <summary>
    ///     路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    ///     后缀名
    /// </summary>
    [StringLength(20)]
    public string Suffix { get; set; }

    /// <summary>
    ///     上传Ip
    /// </summary>
    [StringLength(50)]
    public string ClientIp { get; set; }

    /// <summary>
    ///     大小
    /// </summary>
    [DefaultValue(0)]
    public long Size { get; set; }

    /// <summary>
    ///     上传类型
    /// </summary>
    public StorageType StorageType { get; set; }

    /// <summary>
    ///     存储域名
    /// </summary>
    [StringLength(200)]
    public string? StorageDomain { get; set; }
}

public enum StorageType
{
    [Description("本机")] Local = 0,
    [Description("对象存储")] Cos = 1
}