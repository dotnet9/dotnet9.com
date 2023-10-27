import { BaseApi } from './BaseApi';
import type { AddAlbumInput, SelectOutput, AlbumsPageOutput, UpdateAlbumInput } from './models';

class AlbumsApi extends BaseApi<AddAlbumInput, UpdateAlbumInput, AlbumsPageOutput> {
	constructor() {
		super('/albums/');
	}
	/**
	 * 专辑下拉选项
	 * @returns
	 */
	select = () => {
		return this.get<SelectOutput[]>(`${this.basePath}select`);
	};
}
export default new AlbumsApi();
