import type { SysUserInfoOutput } from './../api/models/sys-user-info-output';
import { defineStore } from 'pinia';
// import Cookies from 'js-cookie';
// import { Session } from '/@/utils/storage';
import { computed, reactive } from 'vue';
import SysUserApi from '../api/SysUserApi';

/**
 * 用户信息
 * @methods setUserInfos 设置用户信息
 */
export const useUserInfo = defineStore('userInfo', () => {
	const userInfoState = reactive({
		userInfo: {} as SysUserInfoOutput,
	});

	//用户信息
	const userInfo = computed(() => userInfoState.userInfo);

	/**
	 * 获取当前用户基本信息
	 */
	const getUserInfo = async () => {
		const { data } = await SysUserApi.getCurrentUserInfo();
		userInfoState.userInfo = data!;
	};
	return { userInfoState, userInfo, getUserInfo };
});
