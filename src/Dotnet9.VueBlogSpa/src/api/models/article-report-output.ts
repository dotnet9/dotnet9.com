/* tslint:disable */
/* eslint-disable */
/**
 * 博客前端接口
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
/**
 * 
 * @export
 * @interface ArticleReportOutput
 */
export interface ArticleReportOutput {
    /**
     * 文章数量
     * @type {number}
     * @memberof ArticleReportOutput
     */
    articleCount?: number;
    /**
     * 标签数量
     * @type {number}
     * @memberof ArticleReportOutput
     */
    tagCount?: number;
    /**
     * 分类数量
     * @type {number}
     * @memberof ArticleReportOutput
     */
    categoryCount?: number;
    /**
     * 专辑数量
     * @type {number}
     * @memberof ArticleReportOutput
     */
    albumCount?: number;
    /**
     * 用户量
     * @type {number}
     * @memberof ArticleReportOutput
     */
    userCount?: number;
    /**
     * 友链数量
     * @type {number}
     * @memberof ArticleReportOutput
     */
    linkCount?: number;
}
