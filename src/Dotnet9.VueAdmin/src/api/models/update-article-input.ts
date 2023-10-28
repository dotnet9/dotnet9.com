/* tslint:disable */
/* eslint-disable */
/**
 * 博客后端接口
 * Dotnet9后端
 *
 * OpenAPI spec version: 0.0.1
 * Contact: 632871194@qq.com
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { AvailabilityStatus } from './availability-status';
import { CreationType } from './creation-type';
/**
 * 
 * @export
 * @interface UpdateArticleInput
 */
export interface UpdateArticleInput {
    /**
     * 标题
     * @type {string}
     * @memberof UpdateArticleInput
     */
    title: string;
    /**
     * 别名
     * @type {string}
     * @memberof UpdateArticleInput
     */
    slug: string;
    /**
     * 概要
     * @type {string}
     * @memberof UpdateArticleInput
     */
    summary: string;
    /**
     * 封面图
     * @type {string}
     * @memberof UpdateArticleInput
     */
    cover: string;
    /**
     * 是否置顶
     * @type {boolean}
     * @memberof UpdateArticleInput
     */
    isTop?: boolean;
    /**
     * 作者
     * @type {string}
     * @memberof UpdateArticleInput
     */
    author: string;
    /**
     * 原文地址
     * @type {string}
     * @memberof UpdateArticleInput
     */
    link?: string | null;
    /**
     * 
     * @type {CreationType}
     * @memberof UpdateArticleInput
     */
    creationType?: CreationType;
    /**
     * 文章正文（Html或markdown）
     * @type {string}
     * @memberof UpdateArticleInput
     */
    content: string;
    /**
     * 文章正文是否为html代码
     * @type {boolean}
     * @memberof UpdateArticleInput
     */
    isHtml?: boolean;
    /**
     * 发布时间
     * @type {Date}
     * @memberof UpdateArticleInput
     */
    publishTime?: Date;
    /**
     * 
     * @type {AvailabilityStatus}
     * @memberof UpdateArticleInput
     */
    status?: AvailabilityStatus;
    /**
     * 排序值（值越小越靠前）
     * @type {number}
     * @memberof UpdateArticleInput
     */
    sort: number;
    /**
     * 是否允许评论
     * @type {boolean}
     * @memberof UpdateArticleInput
     */
    isAllowComments?: boolean;
    /**
     * 过期时间（过期后文章不显示）
     * @type {Date}
     * @memberof UpdateArticleInput
     */
    expiredTime?: Date | null;
    /**
     * 标签
     * @type {Array<number>}
     * @memberof UpdateArticleInput
     */
    tags: Array<number>;
    /**
     * 分类ID
     * @type {number}
     * @memberof UpdateArticleInput
     */
    categoryId: number;
    /**
     * 专辑ID
     * @type {number}
     * @memberof UpdateArticleInput
     */
    albumId: number;
    /**
     * 文章ID
     * @type {number}
     * @memberof UpdateArticleInput
     */
    id: number;
}
