import { UpdateCurrentUserInput } from './models/update-current-user-input';
import { BaseApi } from './BaseApi';
import {
	PageResultSysUserPageOutput,
	AddSysUserInput,
	UpdateSysUserInput,
	ResetPasswordInput,
	SysUserInfoOutput,
	ChangePasswordOutput,
} from './models';
class SysUserApi extends BaseApi<AddSysUserInput, UpdateSysUserInput, PageResultSysUserPageOutput> {
	constructor() {
		super('/sysuser/');
	}

	/**
	 * 获取系统用户详情
	 * @param id 系统用户id
	 * @returns
	 */
	getSysUserDetail = (id: number) => {
		return this.get(`${this.basePath}detail`, { params: { id } });
	};

	/**
	 * 重置系统用户密码
	 * @param data 密码
	 * @returns
	 */
	resetPassword = (data: ResetPasswordInput) => {
		return this.patch(`${this.basePath}reset`, data);
	};

	/**
	 * 获取当前登录用户的信息
	 * @returns
	 */
	getCurrentUserInfo = () => {
		return this.get<SysUserInfoOutput>(`${this.basePath}currentuserinfo`);
	};

	/**
	 * 用户修改自己的密码
	 * @param data 密码信息
	 * @returns
	 */
	changePassword = (data: ChangePasswordOutput) => {
		return this.patch(`${this.basePath}changepassword`, data);
	};

	/**
	 * 修改个人头像
	 * @param url 头像地址
	 * @returns
	 */
	setAvatar = (url: string) => {
		return this.patch(`${this.basePath}uploadavatar`, url);
	};

	/**
	 * 修改个人信息
	 * @param data 个人信息
	 * @returns
	 */
	updateCurrentUser = (data: UpdateCurrentUserInput) => {
		return this.patch(`${this.basePath}updatecurrentuser`, data);
	};
}

export default new SysUserApi();
