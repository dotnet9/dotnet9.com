/// <reference types="vite/client" />

declare module '*.vue' {
  import type { DefineComponent } from 'vue'
  const component: DefineComponent<{}, {}, any>
  export default component
}

import 'vue-router'
declare module 'vue-router' {
  interface RouteMeta {
    /**
     * 标题
     */
    title: string
    /**
     *  keepalive缓存名称，需要和组件名字一样
     */
    keepName?: string
  }
}
