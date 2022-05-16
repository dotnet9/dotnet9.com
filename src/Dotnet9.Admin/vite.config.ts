import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
debugger
import * as path from 'path'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  build: {
    outDir: '../Dotnet9.Web/wwwroot/admin'
  },
  server: {
    proxy: {
      "/api": {
        target: "http://localhost:5133",
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, "api")
      },
      "/upload": {
        target: "https://localhost:5133/upload",
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/upload/, "")
      },
      "/img": {
        target: "https://localhost:5133/img",
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
