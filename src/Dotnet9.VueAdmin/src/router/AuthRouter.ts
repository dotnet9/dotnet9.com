import { RouteRecordRaw } from "vue-router";

import Login from "@/views/auth/Login.vue"

const auth: RouteRecordRaw[] = [
    {
        path: '', component: Login,
        meta:{
            title:'登录'
        }
    },
    {
        path: 'login', component: Login,
        meta:{
            title:'登录'
        }
    }
]

export {
    auth
}