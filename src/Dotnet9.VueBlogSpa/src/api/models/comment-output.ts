/* tslint:disable */
/* eslint-disable */
/**
 * 博客前端接口
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { PageResultReplyOutput } from './page-result-reply-output';
/**
 * 
 * @export
 * @interface CommentOutput
 */
export interface CommentOutput {
    /**
     * 评论ID
     * @type {number}
     * @memberof CommentOutput
     */
    id?: number;
    /**
     * 博主标识
     * @type {boolean}
     * @memberof CommentOutput
     */
    isBlogger?: boolean;
    /**
     * 评论人ID
     * @type {number}
     * @memberof CommentOutput
     */
    accountId?: number;
    /**
     * 昵称
     * @type {string}
     * @memberof CommentOutput
     */
    nickName?: string | null;
    /**
     * 头像
     * @type {string}
     * @memberof CommentOutput
     */
    avatar?: string | null;
    /**
     * 楼层
     * @type {number}
     * @memberof CommentOutput
     */
    index?: number;
    /**
     * 评论内容
     * @type {string}
     * @memberof CommentOutput
     */
    content?: string | null;
    /**
     * 回复条数
     * @type {number}
     * @memberof CommentOutput
     */
    replyCount?: number;
    /**
     * 点赞数量
     * @type {number}
     * @memberof CommentOutput
     */
    praiseTotal?: number;
    /**
     * 是否已点赞
     * @type {boolean}
     * @memberof CommentOutput
     */
    isPraise?: boolean;
    /**
     * Ip地址
     * @type {string}
     * @memberof CommentOutput
     */
    ip?: string | null;
    /**
     * Ip归属地
     * @type {string}
     * @memberof CommentOutput
     */
    geolocation?: string | null;
    /**
     * 评论时间
     * @type {Date}
     * @memberof CommentOutput
     */
    createdTime?: Date;
    /**
     * 
     * @type {PageResultReplyOutput}
     * @memberof CommentOutput
     */
    replyList?: PageResultReplyOutput;
}
