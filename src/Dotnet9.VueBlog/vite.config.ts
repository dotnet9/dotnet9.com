import vue from "@vitejs/plugin-vue";
import ssr from "vite-plugin-ssr/plugin";
import { UserConfig } from "vite";
import vuetify, { transformAssetUrls } from "vite-plugin-vuetify";
// 让项目支持require导入模块 import vitePluginRequire from 'vite-plugin-require'
import requireTransform from "vite-plugin-require-transform";

const config: UserConfig = {
  plugins: [
    vue({
      template: { transformAssetUrls },
    }),
    ssr(),
    vuetify({
      autoImport: true,
    }),
    requireTransform({
      fileRegex: /.ts$|.tsx$|.vue$/,
    }),
  ],
  ssr: {
    noExternal: ["vuetify"],
  },
  resolve: {
    alias: {
      "~": __dirname,
    },
  },
  server: {
    port: 3001,
    host: "0.0.0.0",
    hmr: true,
    proxy: {
      "/blog": {
        target: "http://localhost:5000",
        ws: true,
        changeOrigin: true,
        secure: false, //解决target使用https出错问题
        rewrite: (path) => path.replace(/^\/blog/, "/api"),
      },
    },
  },
  css: {
    // css预处理器
    preprocessorOptions: {
      scss: {
        charset: false,
      },
    },
  },
};

export default config;
