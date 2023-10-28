import { Pagination } from "./pagination";

export interface ArticleListQueryInput extends Pagination {
  /**
   * 标签ID
   */
  tagId?: number;

  /**
   * 分类ID
   */
  categoryId?: number;
  /**
   * 关键词
   */
  keyword?: string;
}
