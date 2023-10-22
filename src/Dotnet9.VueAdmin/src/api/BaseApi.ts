import type { AxiosInstance, AxiosRequestConfig } from 'axios';
import http from '../utils/http';
import { AvailabilityDto } from './models';
type config = { url: string; requestConfig?: AxiosRequestConfig };
interface BaseApiConfig {
	add: config;
	edit: config;
	delete: Pick<config, 'url'>;
	list: Pick<config, 'url'>;
	setStatus: config;
}
/**
 * 通用api增删改查 （A:添加的参数类型，E:编辑的参数类型,P：列表查询的返回结果类型）
 */
export class BaseApi<A = any, E = any, P = any> {
	basePath: string;
	config: BaseApiConfig;
	readonly axios: AxiosInstance;
	constructor(
		basePath: string,
		config: BaseApiConfig = {
			add: { url: 'add' },
			edit: { url: 'edit' },
			delete: { url: 'delete' },
			list: { url: 'page' },
			setStatus: { url: 'setstatus' },
		}
	) {
		this.basePath = basePath.startsWith('/') ? basePath : `/${basePath}/`;
		this.config = config;
		this.axios = http.instance;
	}
	/**
	 * 拼接api地址
	 * @param action 接口操作
	 * @returns
	 */
	protected combine = (action: string) => {
		return `${this.basePath}${action}`;
	};

	/**
	 * get请求
	 * @param url 请求地址
	 * @param config 配置参数
	 * @returns
	 */
	protected get = <T = any>(url: string, config?: AxiosRequestConfig): Promise<ApiResult<T>> => {
		return http.get(url, config);
	};

	/**
	 * post请求
	 * @param url 请求地址
	 * @param data 请求数据
	 * @param config 配置参数
	 * @returns
	 */
	protected post = <T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResult<T>> => {
		return http.post(url, data, config);
	};

	/**
	 * put请求
	 * @param url 请求地址
	 * @param data 请求数据
	 * @param config 配置参数
	 * @returns
	 */
	protected put = <T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResult<T>> => {
		return http.put(url, data, config);
	};

	/**
	 * patch请求
	 * @param url 请求地址
	 * @param data 请求数据
	 * @param config 配置参数
	 * @returns
	 */
	protected patch = <T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<ApiResult<T>> => {
		return http.patch(url, data, config);
	};

	/**
	 * delete请求
	 * @param url 请求地址
	 * @param config 配置参数
	 * @returns
	 */
	protected remove = <T = any>(url: string, config?: AxiosRequestConfig): Promise<ApiResult<T>> => {
		return http.delete(url, config);
	};

	/**
	 * 编辑
	 * @param data 参数
	 * @returns
	 */
	add = (data: A) => {
		return http.post(`${this.basePath}${this.config.add.url}`, data, this.config.add.requestConfig);
	};

	/**
	 * 编辑
	 * @param data 参数
	 * @returns
	 */
	edit = (data: E) => {
		return http.put(`${this.basePath}${this.config.edit.url}`, data, this.config.edit.requestConfig);
	};

	/**
	 *
	 * @param params 分页查询
	 * @returns
	 */
	page = (params?: any) => {
		return http.get<P>(`${this.basePath}${this.config.list.url}`, { params });
	};

	/**
	 * 删除
	 * @param data 参数
	 * @returns
	 */
	delete = (data?: Record<string, any>) => {
		return http.delete(`${this.basePath}${this.config.delete.url}`, { data });
	};

	/**
	 * 设置状态
	 * @param data 参数
	 * @returns
	 */
	setStatus = (data: AvailabilityDto) => {
		return http.patch(`${this.basePath}${this.config.setStatus.url}`, data, this.config.setStatus.requestConfig);
	};
}
