import http from "~/utils/http";
import type { PageResultCoversOutput, PageResultPictureOutput } from "./models";
import type { Pagination } from "./models/pagination";

class CoversApi {
  /**
   * 模块分页查询
   * @returns
   */
  list = (params: Pagination) => {
    return http.get<PageResultCoversOutput>("/covers", { params });
  };

  /**
   * 模块下的图片
   * @returns
   */
  pictures = (params: {
    pageNo: number;
    pageSize: number;
    coverId?: number;
  }) => {
    return http.get<PageResultPictureOutput>("/covers/pictures", { params });
  };
}

export default new CoversApi();
