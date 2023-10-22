import http from "~/utils/http";
import type { Pagination } from "./models/pagination";
import type { PageResultTalksOutput, TalkDetailOutput } from "./models";

class TalksApi {
  /**
   * 说说列表
   * @param params 查询参数
   * @returns
   */
  list = (params: Pagination) => {
    return http.get<PageResultTalksOutput>("/talks", { params });
  };

  /**
   * 说说详情信息
   * @param id 说说ID
   * @returns
   */
  talkDetail = (id: number) => {
    return http.get<TalkDetailOutput>("/talks/talkdetail", { params: { id } });
  };
}

export default new TalksApi();
