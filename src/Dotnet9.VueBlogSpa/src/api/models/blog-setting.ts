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
 * @interface BlogSetting
 */
export interface BlogSetting {
    /**
     * 网站Logo
     * @type {string}
     * @memberof BlogSetting
     */
    logo?: string | null;
    /**
     * 站点图标
     * @type {string}
     * @memberof BlogSetting
     */
    favicon?: string | null;
    /**
     * 开启打赏
     * @type {boolean}
     * @memberof BlogSetting
     */
    isRewards?: boolean | null;
    /**
     * 支付宝
     * @type {string}
     * @memberof BlogSetting
     */
    aliPay?: string | null;
    /**
     * 微信收款码
     * @type {string}
     * @memberof BlogSetting
     */
    wxPay?: string | null;
    /**
     * 允许留言
     * @type {boolean}
     * @memberof BlogSetting
     */
    isAllowMessage?: boolean | null;
    /**
     * 允许评论
     * @type {boolean}
     * @memberof BlogSetting
     */
    isAllowComments?: boolean | null;
    /**
     * 公告
     * @type {string}
     * @memberof BlogSetting
     */
    announcement?: string | null;
    /**
     * 站点名称
     * @type {string}
     * @memberof BlogSetting
     */
    siteName?: string | null;
    /**
     * 首页格言
     * @type {string}
     * @memberof BlogSetting
     */
    motto?: string | null;
    /**
     * 网站运营时间
     * @type {Date}
     * @memberof BlogSetting
     */
    runTime?: Date;
    /**
     * 站点版权
     * @type {string}
     * @memberof BlogSetting
     */
    copyright?: string | null;
    /**
     * 站点描述
     * @type {string}
     * @memberof BlogSetting
     */
    description?: string | null;
    /**
     * 关键词
     * @type {string}
     * @memberof BlogSetting
     */
    keyword?: string | null;
    /**
     * 备案号
     * @type {string}
     * @memberof BlogSetting
     */
    filing?: string | null;
}
