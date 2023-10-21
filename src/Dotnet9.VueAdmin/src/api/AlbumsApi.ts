import { BaseApi } from './BaseApi';
import type { AddAlbumsInput, AlbumsPageOutput, UpdateAlbumsInput } from './models';
// 相册相关Api
class AlbumsApi extends BaseApi<AddAlbumsInput, UpdateAlbumsInput, AlbumsPageOutput> {
	constructor() {
		super('/albums/');
	}
}
export default new AlbumsApi();
