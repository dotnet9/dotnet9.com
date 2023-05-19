import { serviceOptions } from '@/shared/service'
import axios, { AxiosError, AxiosResponse } from 'axios'
import { AdminRouter } from "../router/AdminRouter"
import { ElMessage } from "element-plus"
import { useRouter } from 'vue-router'


const axiosConfig = () => {
    serviceOptions.axios = axios.create({
        timeout: 1000 * 10,
        headers: {
            "client-lib": "axios"
        }
    })


    serviceOptions.axios.interceptors.response.use(
        (res: AxiosResponse) => {
            return res;
        }, (err: AxiosError) => {
            switch (err.response?.status) {
                case 401:
                    ElMessage({
                        message: '身份验证失败，请重新登录',
                        type: 'error'
                    })
                    AdminRouter.replace("/auth")

                    break;
                case 500:
                    let data = err.response.data as any;
                    if (data && data.message) {
                        ElMessage({
                            message: data.message,
                            type: 'error'
                        })
                    } else {
                        ElMessage({
                            message: '服务器繁忙! 500',
                            type: 'error'
                        })
                    }
                    break;
                default:
                    ElMessage('请求错误:' + err.response?.status)
                    break;
            }
            return Promise.reject(err)
        })
}

export {
    axiosConfig
}