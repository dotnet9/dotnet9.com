import http from "~/utils/http";
import type { ArticleListQueryInput } from "./models/article-list-query-input";
import type {
  TagsOutput,
  CategoryOutput,
  ArticleReportOutput,
  PageResultArticleOutput,
  ArticleInfoOutput,
  ArticleBasicsOutput,
} from "./models";

class ArticleApi {
  /**
   * 文章分页查询
   * @param params 查询参数
   * @returns
   */
  list = (params: ArticleListQueryInput) => {
    return http.get<PageResultArticleOutput>("/article", {
      params,
    });
  };

  /**
   * 所有标签
   * @returns
   */
  tags = () => {
    return http.get<Array<TagsOutput>>("/article/tags");
  };

  /**
   * 所有栏目
   * @returns
   */
  categories = () => {
    return http.get<Array<CategoryOutput>>("/article/categories");
  };

  /**
   * 博客统计
   * @returns
   */
  report = () => {
    return http.get<ArticleReportOutput>("/article/report");
  };
  /**
   * 文章详情
   * @param id 文章ID
   * @returns
   */
  info = (id: number) => {
    return http.get<ArticleInfoOutput>("/article/info", {
      params: { id },
    });
  };
  /**
   *  文章最新5条记录
   * @returns
   */
  latest = () => {
    return http.get<ArticleBasicsOutput[]>("/article/latest");
  };
}

export default new ArticleApi();
