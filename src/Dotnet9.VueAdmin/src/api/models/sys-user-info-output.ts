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
import { Gender } from './gender';
/**
 * 
 * @export
 * @interface SysUserInfoOutput
 */
export interface SysUserInfoOutput {
    /**
     * 姓名
     * @type {string}
     * @memberof SysUserInfoOutput
     */
    name?: string | null;
    /**
     * 账户名
     * @type {string}
     * @memberof SysUserInfoOutput
     */
    account?: string | null;
    /**
     * 头像
     * @type {string}
     * @memberof SysUserInfoOutput
     */
    avatar?: string | null;
    /**
     * 生日
     * @type {string}
     * @memberof SysUserInfoOutput
     */
    birthday?: string | null;
    /**
     * 邮箱
     * @type {string}
     * @memberof SysUserInfoOutput
     */
    email?: string | null;
    /**
     * 
     * @type {Gender}
     * @memberof SysUserInfoOutput
     */
    gender?: Gender;
    /**
     * 昵称
     * @type {string}
     * @memberof SysUserInfoOutput
     */
    nickName?: string | null;
    /**
     * 备注
     * @type {string}
     * @memberof SysUserInfoOutput
     */
    remark?: string | null;
    /**
     * 最后登录ip
     * @type {string}
     * @memberof SysUserInfoOutput
     */
    lastLoginIp?: string | null;
    /**
     * 最后登录IP所属地址
     * @type {string}
     * @memberof SysUserInfoOutput
     */
    lastLoginAddress?: string | null;
    /**
     * 手机号码
     * @type {string}
     * @memberof SysUserInfoOutput
     */
    mobile?: string | null;
    /**
     * 机构id
     * @type {number}
     * @memberof SysUserInfoOutput
     */
    orgId?: number;
    /**
     * 机构名称
     * @type {string}
     * @memberof SysUserInfoOutput
     */
    orgName?: string | null;
    /**
     * 授权按钮
     * @type {Array<string>}
     * @memberof SysUserInfoOutput
     */
    authBtnList?: Array<string> | null;
}
