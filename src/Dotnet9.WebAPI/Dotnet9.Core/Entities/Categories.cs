﻿using Dotnet9.Core.Enum;

namespace Dotnet9.Core.Entities;

/// <summary>
/// 文章分类表
/// </summary>
public class Categories : Entity<long>, IAvailability, ISortable, ICreatedUserId, ISoftDelete, ICreatedTime
{
    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(Length = 32)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 别名
    /// </summary>
    [SugarColumn(Length = 128)]
    public string Slug { get; set; } = null!;

    /// <summary>
    /// 父级id
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 封面图
    /// </summary>
    [SugarColumn(Length = 256)]
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
    [SugarColumn(Length = 256)]
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public long CreatedUserId { get; set; }

    /// <summary>
    /// 标记删除
    /// </summary>
    public bool DeleteMark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// 子分类
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public List<Categories> Children { get; set; } = new();
}