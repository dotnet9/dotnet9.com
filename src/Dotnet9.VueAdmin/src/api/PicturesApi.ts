import type { PageResultPicturesPageOutput } from './models/page-result-pictures-page-output';
import type { AddPictureInput } from './models/add-picture-input';
import http from '../utils/http';

class PicturesApi {
	/**
	 * 相册图片列表
	 * @param params
	 * @returns
	 */
	page = (params: any) => {
		return http.get<PageResultPicturesPageOutput>('/pictures/page', { params });
	};

	/**
	 * 添加相册图片
	 * @param pic 图片信息
	 * @returns
	 */
	add = (pic: AddPictureInput) => {
		return http.post('/pictures/add', pic);
	};
	/**
	 * 删除图片
	 * @param id 图片id
	 * @returns
	 */
	delete = (id: number) => {
		return http.delete('/pictures/delete', { data: { id } });
	};
}

export default new PicturesApi();
