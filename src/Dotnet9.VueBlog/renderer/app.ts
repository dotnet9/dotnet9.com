import { createSSRApp, defineComponent, h, markRaw, reactive } from "vue";
import DefaultLayout from "./DefaultLayout.vue";
import { setPageContext } from "./usePageContext";
import type { PageContext } from "./types";
import { registerPlugins } from "~/plugins";
import { createPinia } from "pinia";
import "animate.css";
import "~/assets/css/index.css";
import "~/assets/css/iconfont.css";
import "~/assets/css/markdown.css";
import "highlight.js/styles/atom-one-dark.css";
import "vue3-cute-component/dist/style.css";
import Toast from "vue-toastification";
import * as pkg from "vue3-cute-component";
export { createApp };

function createApp(pageContext: PageContext) {
  let rootComponent: any;
  const PageWithLayout = defineComponent({
    data: () => ({
      Page: markRaw(pageContext.Page),
      pageProps: markRaw(pageContext.pageProps || {}),
      Layout: markRaw(pageContext.exports.Layout || DefaultLayout), //布局页文档示例：https://github.com/brillout/vite-plugin-ssr/blob/main/examples/layouts-vue/renderer/app.js
    }),
    created() {
      rootComponent = this;
    },
    render() {
      return h(
        this.Layout,
        {},
        {
          default: () => {
            return h(this.Page, this.pageProps);
          },
        }
      );
    },
  });

  const app = createSSRApp(PageWithLayout);

  //注册组件
  registerPlugins(app);

  app.use(Toast);
  app.use(pkg.plugin);
  const store = createPinia();
  app.use(store);

  const pageContextReactive = reactive(pageContext);
  // We use `app.changePage()` to do Client Routing, see `_default.page.client.js`
  //示例地址：https://github.com/brillout/vite-plugin-ssr/blob/main/examples/vue-pinia/renderer/app.js

  Object.assign(app, {
    changePage: (pageContext: PageContext) => {
      Object.assign(pageContextReactive, pageContext);
      rootComponent.Page = markRaw(pageContext.Page);
      rootComponent.pageProps = markRaw(pageContext.pageProps || {});
      // rootComponent.Layout = markRaw(
      //   pageContext.exports.Layout || DefaultLayout
      // );
    },
  });

  // Make pageContext available from any Vue component
  setPageContext(app, pageContextReactive);

  return { app, store };
}
