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
 * @interface AddFriendLinkInput
 */
export interface AddFriendLinkInput {
    /**
     * 网站名称
     * @type {string}
     * @memberof AddFriendLinkInput
     */
    siteName: string;
    /**
     * 网站链接
     * @type {string}
     * @memberof AddFriendLinkInput
     */
    link: string;
    /**
     * 网站logo
     * @type {string}
     * @memberof AddFriendLinkInput
     */
    logo: string;
    /**
     * 对方博客友链的地址
     * @type {string}
     * @memberof AddFriendLinkInput
     */
    url?: string | null;
    /**
     * 是否忽略对方站点是否存在本站链接
     * @type {boolean}
     * @memberof AddFriendLinkInput
     */
    isIgnoreCheck?: boolean;
    /**
     * 备注
     * @type {string}
     * @memberof AddFriendLinkInput
     */
    remark?: string | null;
    /**
     * 排序值（值越小越靠前）
     * @type {number}
     * @memberof AddFriendLinkInput
     */
    sort: number;
    /**
     * 
     * @type {AvailabilityStatus}
     * @memberof AddFriendLinkInput
     */
    status?: AvailabilityStatus;
}
