export interface CommentPageQueryInput {
  /**
   * 对应模块ID或评论ID（null表留言，0代表友链的评论）
   */
  id?: number;
  /**
   * 当前页码
   */
  pageNo?: number;
  /**
   * 页码容量
   */
  pageSize?: number;
}
