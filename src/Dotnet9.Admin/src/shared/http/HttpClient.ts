import axios, { AxiosError } from "axios";
import { ElMessage } from "element-plus"
import { router } from "../../router/index"

const http = axios.create({
    // headers: {
    //     "token": 'you token'
    // },
    timeout: 1000 * 60 * 10
})

const getCookie = (name: string) => {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}

http.interceptors.request.use((config) => {
    //自定义拦截请求逻辑处理
    config.headers['X-Requested-With'] = 'XMLHttpRequest'
    let token = getCookie('CSRF-TOKEN');
    config.headers["X-CSRF-TOKEN"] = token;
    return config;
})

http.interceptors.response.use((response) => {
    // console.log(response.data)
    return response.data
}, (error: AxiosError) => {
    //自定义拦截响应处理，比如500状态的时候，提示错误等
    console.log(error);
    console.log(error.response?.data);
    if (error.response?.status == 500) {

        let data = error.response?.data;
        let message = "服务器繁忙"
        if (data.message) {
            message = data.message
        }
        ElMessage({
            showClose: false,
            message: message,
            type: 'error'
        })

    }
    if(error.response.status==400){
        let data = error.response?.data;
        if(data.message){
            ElMessage({
                message:data.message,
                type:'error'
            })
        }else{
            ElMessage({
                message:'服务器请求失败',
                type:'error'
            })
        }
    }
    if (error.response?.status == 401) {
        ElMessage({
            showClose: false,
            message: "访问需要登录",
            type: 'warning'
        })
        router.push('/login')

    }
    if (error.response?.status == 404) {
        ElMessage({
            showClose: false,
            message: "Url地址错误！【404】",
            type: 'warning'
        })
    }
    return Promise.reject(error)
})

const get = async <T>(url: string, params: {}): Promise<T> => {
    let res = await http.get(url, { params: params })
    return res as any
}

const del = async <T>(url: string, params: {}): Promise<T> => {
    let res = await http.delete(url, { params: params })
    return res as any
}

const post = async <T>(url: string, body: {}): Promise<T> => {
    let res = await http.post(url, body)
    return res as any
}

const upload = async <T>(url: string, formData: FormData): Promise<T> => {
    let res = await http.post(url, formData)

    return res as any
}

const PageRequest = {
    index: 1,
    size: 10
}

export {
    http,
    get,
    post,
    del,
    PageRequest,
    upload
}
