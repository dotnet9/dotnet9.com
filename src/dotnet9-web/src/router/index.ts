import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import Home from '../views/Home.vue'
import BlogPostList from '../views/BlogPostList.vue'
import BlogPostDetails from '../views/BlogPostDetails.vue'
import BlogPostCreate from '../views/BlogPostCreate.vue'
import Login from '../views/Login.vue'

const routes: Array<RouteRecordRaw> = [
    {
        path: "/",
        name: "Home",
        component: Home,
    },
    {
      path: "/BlogPostList",
      name: "BlogPostList",
      component: BlogPostList,
      meta: {
        //requireAuth: true, 
      },
    },
    {
        path: "/BlogPostDetails/:id",
        name: "BlogPostDetails",
        props: true,
        component: BlogPostDetails,
        meta: {
            //requireAuth: true
        }
    },
    {
        path: "/BlogPostCreate",
        name: "BlogPostCreate",
        component: BlogPostCreate,
        meta: {
            //requireAuth: true
        }
    },
    {
        path: "/Login",
        name: "Login",
        component: Login,
    },
    {
        path: "/about",
        name: "about",
        component: () => import ('../views/About.vue'),
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