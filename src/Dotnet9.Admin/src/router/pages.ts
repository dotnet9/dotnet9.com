import { RouteRecordRaw } from "vue-router";

import Pages from '../views/blogs/pages/Pages.vue'
import PageWrite from '../views/blogs/pages/PageWrite.vue'

const pages: RouteRecordRaw[] = [
    {
        path: 'pages-write', component: PageWrite
    },
    {
        path: 'pages', component: Pages
    }
]

export {
    pages
}