import { Pagination } from "./pagination";

export interface PicturesQueryInput extends Pagination {
  /**
   * 相册ID
   */
  albumId: number;
}
