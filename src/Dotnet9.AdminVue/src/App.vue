<script setup lang="ts">

import { useRouter } from 'vue-router'
import 'nprogress/nprogress.css'

import NProgress from 'nprogress'

import zhCn from 'element-plus/lib/locale/lang/zh-cn'

const router = useRouter();

router.beforeEach(guard => {
  NProgress.start();
})

router.afterEach((to, from) => {
  NProgress.done()
})


</script>

<template>
  <el-config-provider :locale="zhCn">
    <router-view></router-view>
  </el-config-provider>
</template>
<style lang="scss">
@import "@/styles/common.scss";

html,
body {
  height: 100%;
  box-sizing: border-box;
  padding: 0;
  margin: 0;
}

#app {
  padding: 0;
  width: 100%;
  height: 100%;
  margin: 0 auto;
}

.el-pagination {
  margin-top: 20px;
}

.main {
  max-width: 100%;
  overflow-x: hidden;
}

.title-text {
  font-size: 22px;
}

.el-table {
  margin-top: 20px;
}
</style>