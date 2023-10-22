export interface ArticleReportOutput {
  /**
   *文章总数
   */
  articleCount: number;
  /**
   * 栏目总数
   */
  categoryCount: number;
  /**
   * 标签总数
   */
  tagCount: number;

  /**
   * 友链数量
   */
  linkCount: number;

  /**
   * 用户数量
   */
  userCount: number;
}
