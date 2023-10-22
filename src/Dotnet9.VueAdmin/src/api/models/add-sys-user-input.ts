/* tslint:disable */
/* eslint-disable */
/**
 * 规范化接口演示
 * 让 .NET 开发更简单，更通用，更流行。
 *
 * OpenAPI spec version: 1.0.0
 * Contact: monksoul@outlook.com
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
 * @interface AddSysUserInput
 */
export interface AddSysUserInput {
    /**
     * 用户名
     * @type {string}
     * @memberof AddSysUserInput
     */
    account: string;
    /**
     * 姓名
     * @type {string}
     * @memberof AddSysUserInput
     */
    name: string;
    /**
     * 
     * @type {Gender}
     * @memberof AddSysUserInput
     */
    gender?: Gender;
    /**
     * 组织机构id
     * @type {number}
     * @memberof AddSysUserInput
     */
    orgId?: number;
    /**
     * 昵称
     * @type {string}
     * @memberof AddSysUserInput
     */
    nickName?: string | null;
    /**
     * 生日
     * @type {string}
     * @memberof AddSysUserInput
     */
    birthday?: string | null;
    /**
     * 手机号码
     * @type {string}
     * @memberof AddSysUserInput
     */
    mobile?: string | null;
    /**
     * 
     * @type {AvailabilityStatus}
     * @memberof AddSysUserInput
     */
    status?: AvailabilityStatus;
    /**
     * 邮箱
     * @type {string}
     * @memberof AddSysUserInput
     */
    email?: string | null;
    /**
     * 备注
     * @type {string}
     * @memberof AddSysUserInput
     */
    remark?: string | null;
    /**
     * 角色
     * @type {Array<number>}
     * @memberof AddSysUserInput
     */
    roles?: Array<number> | null;
}
