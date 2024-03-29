import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import NProgress from "nprogress";
import "nprogress/nprogress.css";
import Index from "../views/Index.vue";
import pinia from "@/stores";
import { useApp } from "@/stores/app";

const appStore = useApp(pinia);
const routes: Array<RouteRecordRaw> = [
  {
    name: "home",
    path: "/:code?",
    component: Index,
    meta: {
      title: "首页",
    },
  },
  {
    name: "archives",
    path: "/archives",
    component: () => import("../views/article/Archives.vue"),
    meta: {
      title: "归档",
    },
  },
  {
    name: "categories",
    path: "/cats",
    component: () => import("../views/article/Category.vue"),
    meta: {
      title: "分类",
    },
  },
  {
    name: "category",
    path: "/cat/:categorySlug",
    component: () => import("../views/article/CategoryList.vue"),
    meta: {
      title: "分类",
    },
  },
  {
    name: "albums",
    path: "/albums",
    component: () => import("../views/article/Album.vue"),
    meta: {
      title: "专辑",
    },
  },
  {
    name: "album",
    path: "/album/:albumSlug",
    component: () => import("../views/article/CategoryList.vue"),
    meta: {
      title: "专辑",
    },
  },
  {
    name: "tags",
    path: "/tags",
    component: () => import("../views/article/Tag.vue"),
    meta: {
      title: "标签",
    },
  },
  {
    name: "tag",
    path: "/tag/:name",
    component: () => import("../views/article/CategoryList.vue"),
    meta: {
      title: "标签",
    },
  },
  {
    name: "detail1",
    path: "/:year/:month/:slug",
    component: () => import("../views/article/Detail.vue"),
    meta: {
      title: "详情1",
    },
  },
  {
    name: "detail2",
    path: "/:shortSlug",
    component: () => import("../views/article/Detail.vue"),
    meta: {
      title: "详情2",
    },
  },
  {
    name: "message",
    path: "/message",
    component: () => import("../views/Message.vue"),
    meta: {
      title: "留言板",
    },
  },
  {
    name: "talks",
    path: "/talks",
    component: () => import("../views/Talks.vue"),
    meta: {
      title: "说说",
    },
  },
  {
    name: "talkInfo",
    path: "/talks/:talkId",
    component: () => import("../views/Talk.vue"),
    meta: {
      title: "说说",
    },
  },
  {
    name: "covers",
    path: "/covers",
    component: () => import("../views/cover/Cover.vue"),
    meta: {
      title: "模块封面",
    },
  },
  {
    name: "photo",
    path: "/covers/:id",
    component: () => import("../views/cover/Photo.vue"),
    meta: {
      title: "图片",
    },
  },
  {
    name: "links",
    path: "/links",
    component: () => import("../views/Link.vue"),
    meta: {
      title: "友情链接",
    },
  },
  {
    name: "about",
    path: "/about",
    component: () => import("../views/About.vue"),
    meta: {
      title: "关于",
    },
  },
  {
    name: "donation",
    path: "/donation",
    component: () => import("../views/Donation.vue"),
    meta: {
      title: "赞助",
    },
  },
  {
    name: "user",
    path: "/user",
    component: () => import("../views/User.vue"),
    meta: {
      title: "用户中心",
    },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

//路由器安置守卫
router.beforeEach(async (to, from, next) => {
  NProgress.start();
  if (!appStore.isInit) {
    await appStore.init();
  }
  if (to.meta.title) {
    document.title = to.meta.title as string;
  }
  next();
});

router.afterEach(() => {
  window.scrollTo({
    top: 0,
  });
  NProgress.done();
});

export default router;
