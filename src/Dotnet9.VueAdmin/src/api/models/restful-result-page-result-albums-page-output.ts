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
import { PageResultAlbumsPageOutput } from './page-result-albums-page-output';
/**
 * 
 * @export
 * @interface RESTfulResultPageResultAlbumsPageOutput
 */
export interface RESTfulResultPageResultAlbumsPageOutput {
    /**
     * 
     * @type {number}
     * @memberof RESTfulResultPageResultAlbumsPageOutput
     */
    statusCode?: number | null;
    /**
     * 
     * @type {PageResultAlbumsPageOutput}
     * @memberof RESTfulResultPageResultAlbumsPageOutput
     */
    data?: PageResultAlbumsPageOutput;
    /**
     * 
     * @type {boolean}
     * @memberof RESTfulResultPageResultAlbumsPageOutput
     */
    succeeded?: boolean;
    /**
     * 
     * @type {any}
     * @memberof RESTfulResultPageResultAlbumsPageOutput
     */
    errors?: any | null;
    /**
     * 
     * @type {any}
     * @memberof RESTfulResultPageResultAlbumsPageOutput
     */
    extras?: any | null;
    /**
     * 
     * @type {number}
     * @memberof RESTfulResultPageResultAlbumsPageOutput
     */
    timestamp?: number;
}
