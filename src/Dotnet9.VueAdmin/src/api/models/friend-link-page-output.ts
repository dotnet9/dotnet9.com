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
/**
 * 
 * @export
 * @interface FriendLinkPageOutput
 */
export interface FriendLinkPageOutput {
    /**
     * 友情链接主键
     * @type {number}
     * @memberof FriendLinkPageOutput
     */
    id?: number;
    /**
     * 
     * @type {AvailabilityStatus}
     * @memberof FriendLinkPageOutput
     */
    status?: AvailabilityStatus;
    /**
     * 站点名称
     * @type {string}
     * @memberof FriendLinkPageOutput
     */
    siteName?: string | null;
    /**
     * 创建时间
     * @type {Date}
     * @memberof FriendLinkPageOutput
     */
    createdTime?: Date;
    /**
     * 忽略站点检查
     * @type {boolean}
     * @memberof FriendLinkPageOutput
     */
    isIgnoreCheck?: boolean;
    /**
     * 友链
     * @type {string}
     * @memberof FriendLinkPageOutput
     */
    link?: string | null;
    /**
     * Logo链接
     * @type {string}
     * @memberof FriendLinkPageOutput
     */
    logo?: string | null;
    /**
     * 对方博客友情链接地址
     * @type {string}
     * @memberof FriendLinkPageOutput
     */
    url?: string | null;
    /**
     * 排序
     * @type {number}
     * @memberof FriendLinkPageOutput
     */
    sort?: number;
    /**
     * 描述
     * @type {string}
     * @memberof FriendLinkPageOutput
     */
    remark?: string | null;
}
