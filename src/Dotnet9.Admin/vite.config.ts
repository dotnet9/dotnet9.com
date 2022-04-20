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
      "/admin": "http://localhost:5133/api",
      "/upload": "http://localhost:5133",
      "/img": "http://localhost:5133"
    }
  },
  resolve: {
    alias: {
      "shared": path.resolve(__dirname, "./src/shared")
    }
  }
})
