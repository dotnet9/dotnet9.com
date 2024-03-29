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
import { MenuType } from './menu-type';
/**
 * 
 * @export
 * @interface RouterMetaOutput
 */
export interface RouterMetaOutput {
    /**
     * 标题
     * @type {string}
     * @memberof RouterMetaOutput
     */
    title?: string | null;
    /**
     * 外链
     * @type {string}
     * @memberof RouterMetaOutput
     */
    isLink?: string | null;
    /**
     * 是否隐藏
     * @type {boolean}
     * @memberof RouterMetaOutput
     */
    isHide?: boolean;
    /**
     * 是否缓存
     * @type {boolean}
     * @memberof RouterMetaOutput
     */
    isKeepAlive?: boolean;
    /**
     * 是否固定
     * @type {boolean}
     * @memberof RouterMetaOutput
     */
    isAffix?: boolean;
    /**
     * 菜单
     * @type {string}
     * @memberof RouterMetaOutput
     */
    icon?: string | null;
    /**
     * 
     * @type {MenuType}
     * @memberof RouterMetaOutput
     */
    type?: MenuType;
}
