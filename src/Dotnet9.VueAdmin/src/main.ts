import { createApp } from 'vue'
import './style.scss'
import App from './App.vue'
import 'element-plus/dist/index.css'
import 'default-passive-events'

import { createPinia } from 'pinia'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'

import ElementPlus from 'element-plus'

import { AdminRouter } from '@/router/AdminRouter'

import { axiosConfig } from "@/shared/Axios.Config"
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
import zhCn from "element-plus/es/locale/lang/zh-cn";



axiosConfig()

const app = createApp(App)
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
}
const pinia = createPinia()
pinia.use(piniaPluginPersistedstate)
app.use(pinia)
app.use(AdminRouter)
app.use(ElementPlus, {
    locale: zhCn
})
app.mount('#app')

