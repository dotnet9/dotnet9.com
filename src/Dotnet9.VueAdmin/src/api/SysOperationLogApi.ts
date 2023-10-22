import http from '../utils/http';
import type { PageResultOperationLogPageOutput } from './models';

class SysOperationLogApi {
	/**
	 * 操作日志分页查询
	 * @param params
	 * @returns
	 */
	page = (params: any) => {
		return http.get<PageResultOperationLogPageOutput[]>('/sysoperationlog/list', { params });
	};

	/**
	 * 清除日志
	 * @returns
	 */
	clear = () => {
		return http.delete('/sysoperationlog/clear');
	};
}

export default new SysOperationLogApi();
