﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Converters;

namespace Dotnet9.Application.Blog.Dtos;

public class ArticleDetailOutput
{
    /// <summary>
    /// 文章ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// 别名
    /// </summary>
    public string Slug { get; set; } = null!;

    /// <summary>
    /// 简介
    /// </summary>
    public string Summary { get; set; } = null!;

    /// <summary>
    /// 封面
    /// </summary>
    public string Cover { get; set; } = null!;

    /// <summary>
    /// 状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }

    /// <summary>
    /// 原文地址
    /// </summary>
    public string? Link { get; set; }

    /// <summary>
    /// 是否置顶
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    public string Author { get; set; } = null!;

    /// <summary>
    /// 文章内容
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// 是否允许评论
    /// </summary>
    public bool IsAllowComments { get; set; }

    /// <summary>
    /// 是个是HTML
    /// </summary>
    public bool IsHtml { get; set; }

    /// <summary>
    /// 创作类型
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public CreationType CreationType { get; set; }

    /// <summary>
    /// 过期时间（过期后文章不显示）
    /// </summary>
    public DateTime? ExpiredTime { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    public List<long>? Tags { get; set; }

    /// <summary>
    /// 分类ID
    /// </summary>
    public long? CategoryId { get; set; }

    /// <summary>
    /// 专辑ID
    /// </summary>
    public long? AlbumId { get; set; }

    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime PublishTime { get; set; }
}