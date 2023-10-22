import axios, { AxiosInstance } from 'axios';
import type { AxiosRequestConfig } from 'axios';
import { ElMessage, ElMessageBox } from 'element-plus';
import { Session } from '/@/utils/storage';
import qs from 'qs';

// token 键定义
export const accessTokenKey = 'access-token';
export const refreshAccessTokenKey = `x-${accessTokenKey}`;

// 清除 token
export const clearAccessTokens = () => {
	Session.remove(accessTokenKey);
	Session.remove(refreshAccessTokenKey);
};

// 配置新建一个 axios 实例
const service: AxiosInstance = axios.create({
	baseURL: import.meta.env.VITE_API_URL,
	timeout: 50000,
	headers: { 'Content-Type': 'application/json' },
	paramsSerializer: {
		serialize(params) {
			return qs.stringify(params, { allowDots: true });
		},
	},
});

// 添加请求拦截器
service.interceptors.request.use(
	(config) => {
		const accessToken = Session.get<string>(accessTokenKey);
		if (accessToken) {
			// 将 token 添加到请求报文头中
			config.headers!['Authorization'] = `Bearer ${accessToken}`;

			// 判断 accessToken 是否过期
			const jwt: any = decryptJWT(accessToken);
			const exp = getJWTDate(jwt.exp as number);
			//token已过期
			if (new Date() >= exp) {
				// 获取刷新 token
				const refreshAccessToken = Session.get<string>(refreshAccessTokenKey);
				// 携带刷新 token
				if (refreshAccessToken) {
					config.headers!['X-Authorization'] = `Bearer ${refreshAccessToken}`;
				}
			}
		}
		return config;
	},
	(error) => {
		// 对请求错误做些什么
		return Promise.reject(error);
	}
);

// 添加响应拦截器
service.interceptors.response.use(
	(response) => {
		// 检查并存储授权信息
		checkAndStoreAuthentication(response);
		const res = response.data;
		if (response.status === 401 || res.statusCode === 401) {
			clearAccessTokens();
			window.location.href = '/'; // 去登录页
			ElMessageBox.alert('您已被登出，请重新登录', '提示', {})
				.then(() => {})
				.catch(() => {});
			return response;
		}
		if (res.statusCode === 403) {
			ElMessage.error('无权访问');
			return response;
		}
		// 处理规范化结果错误
		if (res.statusCode !== 200) {
			var message = JSON.stringify(res.errors);
			ElMessage.error(message);
		}
		return response;
	},
	(error) => {
		// 对响应错误做点什么
		if (error.message.indexOf('timeout') != -1) {
			ElMessage.error('网络超时');
		} else if (error.message == 'Network Error') {
			ElMessage.error('网络连接错误');
		} else {
			if (error.response.data) ElMessage.error(error.response.statusText);
			else ElMessage.error('接口路径找不到');
		}
		return Promise.reject(error);
	}
);

/**
 * 检查并存储授权信息
 * @param res 响应对象
 */
export function checkAndStoreAuthentication(res: any): void {
	// 读取响应报文头 token 信息
	var accessToken = res.headers[accessTokenKey];
	var refreshAccessToken = res.headers[refreshAccessTokenKey];

	// 判断是否是无效 token
	if (accessToken === 'invalid_token') {
		clearAccessTokens();
	}
	// 判断是否存在刷新 token，如果存在则存储在本地
	else if (refreshAccessToken && accessToken && accessToken !== 'invalid_token') {
		Session.set(accessTokenKey, accessToken);
		Session.set(refreshAccessTokenKey, refreshAccessToken);
	}
}

/**
 * 解密 JWT token 的信息
 * @param token jwt token 字符串
 * @returns <any>object
 */
export function decryptJWT(token: string): any {
	token = token.replace(/_/g, '/').replace(/-/g, '+');
	var json = decodeURIComponent(escape(window.atob(token.split('.')[1])));
	return JSON.parse(json);
}

/**
 * 将 JWT 时间戳转换成 Date
 * @description 主要针对 `exp`，`iat`，`nbf`
 * @param timestamp 时间戳
 * @returns Date 对象
 */
export function getJWTDate(timestamp: number): Date {
	return new Date(timestamp * 1000);
}

// 导出 axios 实例
export default service;

/**
 * 常用请求方式
 */
export const http = {
	/**
	 * get请求
	 * @param url 请求地址
	 * @param config 请求配置
	 * @returns
	 */
	get: <T = any>(url: string, config?: AxiosRequestConfig): Promise<ApiResult<T>> => {
		return new Promise((resolve, reject) => {
			service
				.get(url, config)
				.then((res) => {
					resolve(res.data);
				})
				.catch((err) => {
					reject(err);
					return err;
				});
		});
	},
	/**
	 * post请求
	 * @param url 请求地址
	 * @param config 请求配置
	 * @returns
	 */
	post: <T = any>(url: string, data: any, config?: AxiosRequestConfig): Promise<ApiResult<T>> => {
		return new Promise((resolve, reject) => {
			service
				.post(url, data, config)
				.then((res) => {
					resolve(res.data);
				})
				.catch((err) => {
					reject(err);
					return err;
				});
		});
	},
	/**
	 * put请求
	 * @param url 请求地址
	 * @param config 请求配置
	 * @returns
	 */
	put: <T = any>(url: string, data: any, config?: AxiosRequestConfig): Promise<ApiResult<T>> => {
		return new Promise((resolve, reject) => {
			service
				.put(url, data, config)
				.then((res) => {
					resolve(res.data);
				})
				.catch((err) => {
					reject(err);
					return err;
				});
		});
	},
	/**
	 * put请求
	 * @param url 请求地址
	 * @param config 请求配置
	 * @returns
	 */
	delete: <T = any>(url: string, config?: AxiosRequestConfig): Promise<ApiResult<T>> => {
		return new Promise((resolve, reject) => {
			service
				.delete(url, config)
				.then((res) => {
					resolve(res.data);
				})
				.catch((err) => {
					reject(err);
					return err;
				});
		});
	},
	/**
	 * patch请求
	 * @param url 请求地址
	 * @param config 请求配置
	 * @returns
	 */ patch: <T = any>(url: string, data: any, config?: AxiosRequestConfig): Promise<ApiResult<T>> => {
		return new Promise((resolve, reject) => {
			service
				.patch(url, data, config)
				.then((res) => {
					resolve(res.data);
				})
				.catch((err) => {
					reject(err);
					return err;
				});
		});
	},
};
