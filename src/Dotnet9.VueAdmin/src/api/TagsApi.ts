import { BaseApi } from './BaseApi';
import type { AddTagInput, SelectOutput, TagsPageOutput, UpdateTagInput } from './models';

class TagsApi extends BaseApi<AddTagInput, UpdateTagInput, TagsPageOutput> {
	constructor() {
		super('/tags/');
	}
	/**
	 * 标签下拉选项
	 * @returns
	 */
	select = () => {
		return this.get<SelectOutput[]>(`${this.basePath}select`);
	};
}
export default new TagsApi();
