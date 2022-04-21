import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

import * as path from 'path'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  build: {
    outDir: '../Dotnet9.Web/wwwroot/admin'
  },
  server: {
    proxy: {
      "/admin": {
        target: "http://localhost:5133/api",
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/admin/, "")
      },
      "/upload": {
        target: "http://localhost:5133/upload",
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/upload/, "")
      },
      "/img": {
        target: "http://localhost:5133/img",
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/img/, "")
      },
    }
  },
  resolve: {
    alias: {
      "shared": path.resolve(__dirname, "./src/shared")
    }
  }
})
