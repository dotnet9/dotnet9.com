import { AddSysMenuInput, RouterOutput, SysMenuDetailOutput, SysMenuPageOutput, TreeSelectOutput, UpdateSysMenuInput } from './models';
import { http } from '../utils/request';
import { BaseApi } from './BaseApi';
class SysMenuApi extends BaseApi<AddSysMenuInput, UpdateSysMenuInput, SysMenuPageOutput> {
	constructor() {
		super('sysmenu');
	}
	/**
	 * 菜单树形下拉框
	 * @returns
	 */
	getTreeSelect = () => {
		return this.get<TreeSelectOutput>(this.combine('treeselect'));
	};

	/**
	 * 获取菜单详情
	 * @param id 菜单id
	 * @returns
	 */
	getMenuDetail = (id: number) => {
		return this.get<SysMenuDetailOutput>(this.combine('detail'), {
			params: {
				id,
			},
		});
	};

	/**
	 * 获取当前用户的菜单
	 * @returns
	 */
	getMenus = () => {
		return this.get<RouterOutput[]>(this.combine('permissionmenus'));
	};

	/**
	 * 菜单按钮树
	 * @returns
	 */
	getTreeMenuButton = () => {
		return this.get<TreeSelectOutput[]>(this.combine('treemenubutton'));
	};
}
export default new SysMenuApi();
