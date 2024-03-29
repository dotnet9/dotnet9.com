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
 * @interface SysRolePageOutput
 */
export interface SysRolePageOutput {
    /**
     * 主键
     * @type {number}
     * @memberof SysRolePageOutput
     */
    id?: number;
    /**
     * 角色名称
     * @type {string}
     * @memberof SysRolePageOutput
     */
    name?: string | null;
    /**
     * 创建时间
     * @type {Date}
     * @memberof SysRolePageOutput
     */
    createdTime?: Date;
    /**
     * 
     * @type {AvailabilityStatus}
     * @memberof SysRolePageOutput
     */
    status?: AvailabilityStatus;
    /**
     * 角色编码
     * @type {string}
     * @memberof SysRolePageOutput
     */
    code?: string | null;
    /**
     * 排序值
     * @type {number}
     * @memberof SysRolePageOutput
     */
    sort?: number;
    /**
     * 备注
     * @type {string}
     * @memberof SysRolePageOutput
     */
    remark?: string | null;
}
