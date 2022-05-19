import { RouteRecordRaw } from "vue-router";


const authRoute: RouteRecordRaw[] = [
    {
        path: '/', component: () =>
            import(
                "../views/auth/index.vue"
            ),
    },
]

export {
    authRoute
}