export { render };
export { onHydrationEnd };
export { onPageTransitionStart };
export { onPageTransitionEnd };

import { createApp } from "./app";
import type { PageContextClient } from "./types";
import NProgress from "nprogress";
import "nprogress/nprogress.css";
import "vue-toastification/dist/index.css";
import { useApp } from "~/stores/app";

// 使用客户端路由（如果使用服务端路由注释下面一行代码）
export const clientRouting = true;

let client: any;
// This render() hook only supports SSR, see https://vite-plugin-ssr.com/render-modes for how to modify render() to support SPA
async function render(pageContext: PageContextClient) {
  const { Page, meta } = pageContext;
  if (!Page) {
    throw new Error(
      "Client-side render() hook expects pageContext.Page to be defined"
    );
  }
  if (client) {
    client.changePage(pageContext);
    document.title = meta?.title ?? import.meta.env.VITE_SITE_NAME;
  } else {
    const { app, store } = createApp(pageContext);
    client = app;
    store.state.value = pageContext.initialStoreState;
    app.mount("#app");
  }
}

const onHydrationEnd = async () => {
  // 初始化基本信息
  const appStore = useApp();
  await appStore.init();
  await appStore.getSiteReport();
  //console.log('Hydration finished; page is now interactive.')
};
const onPageTransitionStart = () => {
  NProgress.start();
  // console.log('Page transition start')
};
const onPageTransitionEnd = () => {
  NProgress.done();
  // console.log('Page transition end')
};

/* To enable Client-side Routing:
export const clientRouting = true
// !! WARNING !! Before doing so, read https://vite-plugin-ssr.com/clientRouting */
