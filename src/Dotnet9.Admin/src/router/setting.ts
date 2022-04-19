
import { RouteRecordRaw } from 'vue-router'
import Setting from '../views/setting/Setting.vue'
import Site from "../views/setting/Site-Setting.vue"
import system from "../views/setting/System-Setting.vue"
import Cos from '../views/setting/Cos-Setting.vue'

const setting: RouteRecordRaw[] = [
    {
        path: 'setting',
        component: Setting,
        children: [
            {
                path: 'cos',
                component: Cos,
                name: '腾讯云Cos配置'
            },
            {
                path:'site',
                component:Site,
                name:'网站配置'
            },
            {
                path:'system',
                component:system,
                name:'系统配置'
            }
        ]
    }
]

export {
    setting
}