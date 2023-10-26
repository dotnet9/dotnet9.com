import { Pagination } from "./pagination";

export interface PicturesQueryInput extends Pagination {
  /**
   * 模块ID
   */
  coverId: number;
}
