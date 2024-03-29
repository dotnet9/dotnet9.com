﻿namespace Dotnet9.Application.Blog.Dtos;

public class AddCategoryInput
{
    /// <summary>
    /// 分类名称
    /// </summary>
    [MaxLength(32, ErrorMessage = "分类名称限制32个字符内")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 分类别名
    /// </summary>
    [Required(ErrorMessage = "分类别名为必填项")]
    [MaxLength(128, ErrorMessage = "分类别名制128个字符内")]
    public string Slug { get; set; } = null!;

    /// <summary>
    /// 父级id
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 封面图
    /// </summary>
    [MaxLength(256)]
    public string Cover { get; set; } = null!;

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
    public string? Remark { get; set; }
}