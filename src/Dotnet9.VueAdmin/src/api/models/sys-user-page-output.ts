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
import { Gender } from './gender';
/**
 * 
 * @export
 * @interface SysUserPageOutput
 */
export interface SysUserPageOutput {
    /**
     * 主键
     * @type {number}
     * @memberof SysUserPageOutput
     */
    id?: number;
    /**
     * 姓名
     * @type {string}
     * @memberof SysUserPageOutput
     */
    name?: string | null;
    /**
     * 
     * @type {AvailabilityStatus}
     * @memberof SysUserPageOutput
     */
    status?: AvailabilityStatus;
    /**
     * 账户名
     * @type {string}
     * @memberof SysUserPageOutput
     */
    account?: string | null;
    /**
     * 生日
     * @type {string}
     * @memberof SysUserPageOutput
     */
    birthday?: string | null;
    /**
     * 手机号码
     * @type {string}
     * @memberof SysUserPageOutput
     */
    mobile?: string | null;
    /**
     * 
     * @type {Gender}
     * @memberof SysUserPageOutput
     */
    gender?: Gender;
    /**
     * 昵称
     * @type {string}
     * @memberof SysUserPageOutput
     */
    nickName?: string | null;
    /**
     * 创建时间
     * @type {Date}
     * @memberof SysUserPageOutput
     */
    createdTime?: Date;
    /**
     * 邮箱
     * @type {string}
     * @memberof SysUserPageOutput
     */
    email?: string | null;
}
