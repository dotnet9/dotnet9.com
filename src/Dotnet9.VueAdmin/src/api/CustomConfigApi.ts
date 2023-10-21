import { BaseApi } from './BaseApi';
import type { AddCustomConfigInput, UpdateCustomConfigInput, PageResultCustomConfigPageOutput, CustomConfigDetailOutput } from './models';

/**
 * 配置管理
 */
class CustomConfigApi extends BaseApi<AddCustomConfigInput, UpdateCustomConfigInput, PageResultCustomConfigPageOutput> {
	constructor() {
		super('/customconfig/');
	}

	/**
	 * 获取表单设计
	 * @param id 配置ID
	 * @param itemId 配置ID
	 * @returns
	 */
	getJson = (id: number, itemId?: number) => {
		return this.get<CustomConfigDetailOutput>(`${this.basePath}getformjson`, { params: { id, itemId } });
	};

	/**
	 * 修改配置设计
	 * @param json 表单json
	 * @returns
	 */
	setJson = (json: any) => {
		return this.patch(`${this.basePath}setjson`, json);
	};

	/**
	 * 生成配置类
	 * @param data 参数
	 * @returns
	 */
	generate = (id: number) => {
		return this.post(`${this.basePath}generate`, { id });
	};

	/**
	 * 删除配置class
	 * @param id 自定义配置ID
	 * @returns 
	 */
	deleteClass = (id: number) => {
		return this.patch(`${this.basePath}deleteclass`, { id });
	};
}
export default new CustomConfigApi();
