import { BaseApi } from './BaseApi';
import { AddOrgInput, SysOrgPageOutput, TreeSelectOutput, UpdateOrgInput } from './models';
class SysOrganizationApi extends BaseApi<AddOrgInput, UpdateOrgInput, SysOrgPageOutput> {
	constructor() {
		super('/sysorganization/');
	}
	/**
	 * 机构下拉选项
	 * @returns
	 */
	getTreeSelect = () => {
		return this.get<TreeSelectOutput[]>(`${this.basePath}treeselect`);
	};
}

export default new SysOrganizationApi();
