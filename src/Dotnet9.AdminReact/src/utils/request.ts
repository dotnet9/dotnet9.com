import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from "axios";
import { Notification } from "@douyinfe/semi-ui";

const baseURL = process.env.NODE_ENV === "development" ? "http://localhost:5005" : "https://api.dotnet9.com";


class Http {
  public axiosInstance: AxiosInstance;

  constructor(baseURL: string) {
    var token = localStorage.getItem("token");
    this.axiosInstance = axios.create({
      baseURL,
      timeout: 60000 * 5,
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    });

    this.axiosInstance.interceptors.request.use(
      (config: any) => {
        // 在请求发送之前做一些处理，比如添加 token
        return config;
      },
      (error: any) => {
        // 处理请求错误
        return Promise.reject(error);
      }
    );

    this.axiosInstance.interceptors.response.use(
      (response: AxiosResponse) => {
        // 在响应之前做一些处理，比如解析响应数据
        return response;
      },
      (error: any) => {
        // 处理响应错误
        return Promise.reject(error);
      }
    );
  }

  public async get<T>(url: string, params?: any): Promise<T> {
    const response = await this.axiosInstance.get(url, { params });
    return response.data;
  }

  public async postForm<T>(url: string, data: FormData, headers: AxiosRequestConfig<any>): Promise<T> {
    const response = await this.axiosInstance.post(url, data, headers);
    return response.data;
  }

  public async post<T>(url: string, data?: any): Promise<T> {
    const response = await this.axiosInstance.post(url, data);
    return response.data;
  }

  public async put<T>(url: string, data?: any): Promise<T> {
    const response = await this.axiosInstance.put(url, data);
    return response.data;
  }

  public async delete<T>(url: string): Promise<T> {
    const response = await this.axiosInstance.delete(url);
    return response.data;
  }

  public async fetchAsStream(
    url: string,
    data: object
  ): Promise<AsyncIterable<string>> {
    const response = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${localStorage.getItem("token")}`
      },
      body: JSON.stringify(data),
    });
    if (!response.ok) {
      const reader = response.body?.getReader();
      const { done, value } = await reader!.read();
      Notification.error({
        content: JSON.parse(new TextDecoder("utf-8").decode(value)).message,
      })
      throw new Error(
        `Failed to fetch ${url}: ${response.status} ${response.statusText}`
      );
    }
    if (!response.body) {
      throw new Error("ReadableStream not supported in this browser.");
    }
    const reader = response.body.getReader();
    return {
      [Symbol.asyncIterator]() {
        return {
          async next() {
            const { done, value } = await reader.read();
            if (done) {
              return { done: true, value: null };
            }
            return {
              done: false,
              value: new TextDecoder("utf-8").decode(value),
            };
          },
        };
      },
    };

  }
}

// eslint-disable-next-line import/no-anonymous-default-export
export default new Http(baseURL);
