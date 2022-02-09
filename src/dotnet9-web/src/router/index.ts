import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import Home from '../views/Home.vue'

const routes: Array<RouteRecordRaw> = [
    {
        path: "/",
        name: "Home",
        component: Home,
    }
];

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
});

import store from '../store';
var storeTemp = store;
router.beforeEach((to, from, next) => {
    if (!storeTemp.state.token) {
        storeTemp.commit("saveToken", window.localStorage.Token);
        storeTemp.commit("saveUserInfo", window.localStorage.UserInfo);
    }
    if (to.meta.requireAuth) {
        if (storeTemp.state.token) {
            next();
        } else {
            next({
                path: "/login",
            })
        }
    } else {
        next();
    }
});

export default router;