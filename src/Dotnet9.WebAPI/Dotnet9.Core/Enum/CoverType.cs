﻿namespace Dotnet9.Core.Enum;

/// <summary>
/// 博客封面类型
/// </summary>
public enum CoverType
{
    /// <summary>
    /// 首页封面
    /// </summary>
    [Description("首页封面图")] Home,

    /// <summary>
    /// 归档封面
    /// </summary>
    [Description("归档封面图")] Archives,

    /// <summary>
    /// 分类封面
    /// </summary>
    [Description("分类封面图")] Category,

    /// <summary>
    /// 专辑封面
    /// </summary>
    [Description("专辑封面图")] Album,

    /// <summary>
    /// 标签封面
    /// </summary>
    [Description("标签封面图")] Tag,

    /// <summary>
    /// 模块封面图
    /// </summary>
    [Description("模块封面图")] Cover,

    /// <summary>
    /// 说说封面图
    /// </summary>
    [Description("说说封面图")] Talk,

    /// <summary>
    /// 关于封面图
    /// </summary>
    [Description("关于封面图")] About,

    /// <summary>
    /// 留言封面图
    /// </summary>
    [Description("留言封面图")] Message,

    /// <summary>
    /// 个人中心封面图
    /// </summary>
    [Description("个人中心封面图")] User,

    /// <summary>
    /// 友情链接封面图
    /// </summary>
    [Description("友情链接封面图")] Link,

    /// <summary>
    /// 标签列表封面
    /// </summary>
    [Description("标签列表封面")] TagList,

    /// <summary>
    /// 分类列表封面
    /// </summary>
    [Description("分类列表封面")] Categories,

    /// <summary>
    /// 专辑列表封面
    /// </summary>
    [Description("专辑列表封面")] Albums
}