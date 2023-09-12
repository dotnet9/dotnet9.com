import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import path from "path";
import vueJsx from '@vitejs/plugin-vue-jsx'


// https://vitejs.dev/config/
export default defineConfig({

  plugins: [vue(), vueJsx({

  })],
  resolve: {
    alias: {
      "@/shared": path.resolve(__dirname, "src/shared"),
      "@/views": path.resolve(__dirname, "src/views"),
      "@/router": path.resolve(__dirname, "src/router"),
      "@/styles": path.resolve(__dirname, "src/styles"),
      "@/store": path.resolve(__dirname, "src/store"),
      "@/components": path.resolve(__dirname, "src/components")
    },

  },
  server: {
    proxy: {
      '/admin': {
        target: 'http://localhost:51775', //'http://localhost:51775',
        changeOrigin: true
      },
      '/file': {
        target: 'http://localhost:51775',
        changeOrigin: true
      }
    }
  },
  build: {
    outDir: '../Server/PandaSite/wwwroot/admin',
  }

})
