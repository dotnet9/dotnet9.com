import { AdminLoginInput } from './models';
import { http } from '../utils/request';

/**
 * 登录
 * @param data 登录参数
 * @returns
 */
export const login = (data: AdminLoginInput) => {
	return http.post('/auth/signin', data);
};
