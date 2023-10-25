import { BaseApi } from './BaseApi';
import type { AddCategoryInput, CategoryPageOutput, TreeSelectOutput, UpdateCategoryInput } from './models';

class CategoryApi extends BaseApi<AddCategoryInput, UpdateCategoryInput, CategoryPageOutput> {
	constructor() {
		super('/category/');
	}
	/**
	 * 分类树形下拉
	 * @returns
	 */
	treeSelect = () => {
		return this.get<TreeSelectOutput[]>(`${this.basePath}treeselect`);
	};
}
export default new CategoryApi();
