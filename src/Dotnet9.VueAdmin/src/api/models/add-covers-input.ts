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
import { CoverType } from './cover-type';
/**
 * 
 * @export
 * @interface AddCoversInput
 */
export interface AddCoversInput {
    /**
     * 模块封面名称
     * @type {string}
     * @memberof AddCoversInput
     */
    name: string;
    /**
     * 封面图
     * @type {string}
     * @memberof AddCoversInput
     */
    cover: string;
    /**
     * 
     * @type {CoverType}
     * @memberof AddCoversInput
     */
    type?: CoverType;
    /**
     * 
     * @type {AvailabilityStatus}
     * @memberof AddCoversInput
     */
    status?: AvailabilityStatus;
    /**
     * 排序值（值越小越靠前）
     * @type {number}
     * @memberof AddCoversInput
     */
    sort: number;
    /**
     * 备注
     * @type {string}
     * @memberof AddCoversInput
     */
    remark?: string | null;
    /**
     * 是否可见
     * @type {boolean}
     * @memberof AddCoversInput
     */
    isVisible?: boolean;
}
