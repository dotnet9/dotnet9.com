import http from '../utils/http';

class SysSigninLogApi {
	/**
	 * 登录日志分页查询
	 * @param params 查询参数
	 * @returns
	 */
	page = (params: any) => {
		return http.get('/syssigninlog/list', { params });
	};
}

export default new SysSigninLogApi();
