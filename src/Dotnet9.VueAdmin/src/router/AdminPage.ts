import { RouteRecordRaw } from "vue-router";

import BlankLayout from "@/views/admin/layout/BlankLayout.vue";

const blogTags: RouteRecordRaw[] = [
    {
        path: "tag",
        name: "标签",
        meta: {
            groupName: '博客',
        },
        component: () => import("@/views/admin/blogs/tag/TagList.vue"),
    },
];

const user: RouteRecordRaw[] = [
    {
        path: "",
        name: "用户管理",
        component: () => import("@/views/auth/accounts/AccountList.vue"),
        meta: {
            leftMenu: true,
        },
    },
    {
        path: "role",
        name: "角色管理",
        component: () => import("@/views/admin/users/RoleManager.vue"),
        meta: {},
    },
];

const site: RouteRecordRaw[] = [

    {
        path: "setting",
        name: "系统设置",
        meta: {
            leftMenu: true,
        },
        component: () => import("@/views/setting/Setting.vue"),
    },
    {
        path: "file-manager",
        name: "文件管理",
        meta: {
            leftMenu: true,
        },
        component: () => import("@/views/admin/regular/FileManager.vue"),
    },
];

const test: RouteRecordRaw[] = [
    {
        path: "test-form",
        name: "测试表单",
        meta: {
            keepName: "SimpleFormDemo",
            title: "测试表单",
        },
        component: () => import("@/views/example/SimpleFormDemo.vue"),
    },
    {
        path: "test-table",
        name: "测试表格1",
        meta: {
            title: "测试表格",
            keepName: "SimpleTableDemo",
        },
        component: () => import("@/views/example/SimpleTableDemo.vue"),
    },
];

const routeList: RouteRecordRaw[] = [
    {
        path: "blog-post",
        component: BlankLayout,
        children: [
            {
                path: "",
                name: "文章",
                component: () => import("@/views/admin/blogs/post/PosList.vue"),
                meta: {
                    groupName: '博客',
                    leftMenu: true
                },
            },
            {
                path: "edit",
                name: "编辑文章",
                component: () => import("@/views/admin/blogs/post/PostEdit.vue")
            },
        ],
        name: "博客",
        meta: {
            leftMenu: true,
        },
    },
    ...blogTags,
    {
        path: "blog-cate",
        component: BlankLayout,
        children: [
            {
                path: "",
                name: "文章分类",
                component: () => import("@/views/admin/blogs/cate/CateList.vue"),
            },
            {
                path: "cate-edit",
                name: "编辑分类",
                meta: {},
                component: () => import("@/views/admin/blogs/cate/CateEdit.vue"),
            },
        ],
        name: "",
        meta: {
            leftMenu: true,
        },
    },
    {
        path: "user",
        component: BlankLayout,
        children: [...user],
        name: "用户",
        meta: {
            leftMenu: true,
        },
    },
    {
        path: "setting",
        component: BlankLayout,
        children: [...site],
        name: "系统",
        meta: {
        },
    },
    {
        path: 'friendlink',
        component: BlankLayout,
        children: [
            {
                path: "",
                name: "友情链接",
                component: () =>
                    import("@/views/admin/blogs/friendlink/FriendLinkList.vue"),
            },
            {
                path: "edit",
                name: "友情链接编辑",
                component: () =>
                    import("@/views/admin/blogs/friendlink/FriendLinkEdit.vue"),
            },
        ]
    }
    // ...test,
];

export { routeList };
