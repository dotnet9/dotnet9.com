import http from '../utils/http';

class AuthAccountApi {
	/**
	 * 博客用户列表
	 * @param params
	 * @returns
	 */
	page = (params: any) => {
		return http.get('/authaccount/list', { params });
	};

	/**
	 * 设置博主
	 * @param id 用户ID
	 * @returns
	 */
	setBlogger = (id: number) => {
		return http.patch('/authaccount/setblogger', { id });
	};

	/**
	 * 删除用户
	 * @param id 用户ID
	 * @returns
	 */
	delete = (id: number) => {
		return http.delete('/authaccount/delete', { data: { id } });
	};
}

export default new AuthAccountApi();
