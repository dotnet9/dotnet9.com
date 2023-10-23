import App from "./App.vue";

// Composables
import { createApp } from "vue";

// Plugins
import { registerPlugins } from "@/plugins";
import "animate.css";
import "./assets/css/index.css";
import "./assets/css/iconfont.css";
import "./assets/css/markdown.css";
import "highlight.js/styles/atom-one-dark.css";
import "vue-toastification/dist/index.css";
import "vue3-cute-component/dist/style.css";
import InfiniteLoading from "vue-infinite-loading";
import formatDateTime from "./plugins/formatDateTime";
import Toast, { PluginOptions } from "vue-toastification";

const app = createApp(App);
registerPlugins(app);
const options: PluginOptions = {
  // You can set your default options here
};
app.use(InfiniteLoading).use(Toast, options).use(formatDateTime).mount("#app");
