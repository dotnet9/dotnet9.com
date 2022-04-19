import {RouteRecordRaw} from 'vue-router'

import PostWrite from "../views/blogs/posts/PostWrite.vue"
import PostList from "../views/blogs/posts/PostList.vue"

const post: RouteRecordRaw[] = [

    {
        path: 'write', component: PostWrite, name: '编辑'
    },
    {
        path: '',
        name: '随笔',
        component: PostList
    },

]
export {
    post
}