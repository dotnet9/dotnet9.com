import { RouteRecordRaw } from "vue-router";
import Dash from "../views/dashboard/Dash.vue";
import Home from "../views/dashboard/Home.vue";

import Post from "../views/blogs/posts/post.vue";
import { post } from "./post";
import { setting } from "./setting";

import TagList from "../views/blogs/tag/tag-list.vue";
import TagEdit from "../views/blogs/tag/tag-edit.vue";

import TabItemContainer from "../views/TabItemContainer.vue";

import Albums from "../views/blogs/albums/Albums.vue"
import EditAlbum from "../views/blogs/albums/EditAlbum.vue";

import Categories from "../views/blogs/categories/Categories.vue"
import EditCategory from "../views/blogs/categories/EditCategory.vue";

import UrlLink from "../views/blogs/urllink/UrlLink.vue";

const dashRoute: RouteRecordRaw[] = [
    {
        path: "/admin",
        component: Dash,
        children: [
            {
                path: "post",
                component: Post,
                children: [...post],
            },
            {
                path: "dash",
                component: Home,
            },
            {
                path: "tag",
                component: TabItemContainer,
                children: [
                    {
                        path: "edit",
                        component: TagEdit,
                        name: "标签编辑",
                    },
                    {
                        path: "",
                        component: TagList,
                        name: "标签",
                    },
                ],
            },
            {
                path: "albums",
                component: Albums,
            },
            {
                path: "editalbum",
                name: 'editalbum',
                component: EditAlbum,
            },
            {
                path: "categories",
                component: Categories,
            },
            {
                path: "editcate",
                name: 'editcate',
                component: EditCategory,
            },
            {
                path: "urllink",
                component: UrlLink,
            },
            ...setting,
        ],
    },
];

export { dashRoute };
