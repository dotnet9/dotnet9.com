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
import { PageResultTalksPageOutput } from './page-result-talks-page-output';
/**
 * 
 * @export
 * @interface RESTfulResultPageResultTalksPageOutput
 */
export interface RESTfulResultPageResultTalksPageOutput {
    /**
     * 
     * @type {number}
     * @memberof RESTfulResultPageResultTalksPageOutput
     */
    statusCode?: number | null;
    /**
     * 
     * @type {PageResultTalksPageOutput}
     * @memberof RESTfulResultPageResultTalksPageOutput
     */
    data?: PageResultTalksPageOutput;
    /**
     * 
     * @type {boolean}
     * @memberof RESTfulResultPageResultTalksPageOutput
     */
    succeeded?: boolean;
    /**
     * 
     * @type {any}
     * @memberof RESTfulResultPageResultTalksPageOutput
     */
    errors?: any | null;
    /**
     * 
     * @type {any}
     * @memberof RESTfulResultPageResultTalksPageOutput
     */
    extras?: any | null;
    /**
     * 
     * @type {number}
     * @memberof RESTfulResultPageResultTalksPageOutput
     */
    timestamp?: number;
}
