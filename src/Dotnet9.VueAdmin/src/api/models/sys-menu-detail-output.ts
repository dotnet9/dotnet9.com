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
import { MenuType } from './menu-type';
/**
 * 
 * @export
 * @interface SysMenuDetailOutput
 */
export interface SysMenuDetailOutput {
    /**
     * 菜单Id
     * @type {number}
     * @memberof SysMenuDetailOutput
     */
    id?: number;
    /**
     * 菜单名称
     * @type {string}
     * @memberof SysMenuDetailOutput
     */
    name?: string | null;
    /**
     * 父级id
     * @type {number}
     * @memberof SysMenuDetailOutput
     */
    parentId?: number | null;
    /**
     * 
     * @type {AvailabilityStatus}
     * @memberof SysMenuDetailOutput
     */
    status?: AvailabilityStatus;
    /**
     * 权限标识
     * @type {string}
     * @memberof SysMenuDetailOutput
     */
    code?: string | null;
    /**
     * 排序
     * @type {number}
     * @memberof SysMenuDetailOutput
     */
    sort?: number;
    /**
     * 组件路径
     * @type {string}
     * @memberof SysMenuDetailOutput
     */
    component?: string | null;
    /**
     * 图标
     * @type {string}
     * @memberof SysMenuDetailOutput
     */
    icon?: string | null;
    /**
     * 是否固定
     * @type {boolean}
     * @memberof SysMenuDetailOutput
     */
    isFixed?: boolean;
    /**
     * 是否内嵌
     * @type {boolean}
     * @memberof SysMenuDetailOutput
     */
    isIframe?: boolean;
    /**
     * 是否缓存
     * @type {boolean}
     * @memberof SysMenuDetailOutput
     */
    isKeepAlive?: boolean;
    /**
     * 是否可见
     * @type {boolean}
     * @memberof SysMenuDetailOutput
     */
    isVisible?: boolean;
    /**
     * 外链
     * @type {string}
     * @memberof SysMenuDetailOutput
     */
    link?: string | null;
    /**
     * 备注
     * @type {string}
     * @memberof SysMenuDetailOutput
     */
    remark?: string | null;
    /**
     * 路由地址
     * @type {string}
     * @memberof SysMenuDetailOutput
     */
    path?: string | null;
    /**
     * 重定向地址
     * @type {string}
     * @memberof SysMenuDetailOutput
     */
    redirect?: string | null;
    /**
     * 路由名称
     * @type {string}
     * @memberof SysMenuDetailOutput
     */
    routeName?: string | null;
    /**
     * 
     * @type {MenuType}
     * @memberof SysMenuDetailOutput
     */
    type?: MenuType;
}
