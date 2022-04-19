import { RouteRecordRaw} from "vue-router";
import Login from '../views/auth/login.vue'
import InitAccount from "../views/auth/InitAccount.vue"


const authRoute: RouteRecordRaw[] = [
    {
        path: '/', component: Login
    },
    {
        path: '/login',
        component: Login
    },
    {
        path:'/initAccount',
        component:InitAccount
    }
]

export {
    authRoute
}