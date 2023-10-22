import { BaseApi } from './BaseApi';
import { AddSysRoleInput, PageResultSysRolePageOutput, TreeSelectOutput, UpdateSysRoleInput } from './models';
class SysRoleApi extends BaseApi<AddSysRoleInput, UpdateSysRoleInput, PageResultSysRolePageOutput> {
	constructor() {
		super('sysrole');
	}
	/**
	 * 获取角色菜单和按钮权限id
	 * @param id 角色id
	 * @returns 菜单按钮id数组
	 */
	getRuleMenu = (id: number) => {
		return this.get<number[]>('/sysrole/getrulemenu', { params: { id } });
	};

	/**
	 * 角色下拉选项
	 * @returns
	 */
	getRoleSelect = () => {
		return this.get<TreeSelectOutput[]>('/sysrole/roleselect');
	};
}
export default new SysRoleApi();
