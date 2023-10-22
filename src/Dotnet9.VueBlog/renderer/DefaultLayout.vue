<template>
  <v-app :theme="theme">
    <!-- 导航栏 -->
    <TopNavBar />
    <!-- 侧边导航栏 -->
    <SideNavBar />
    <v-main>
      <slot />
    </v-main>
    <!-- 脚部 -->
    <Footer v-if="showFooter" />
    <!-- 返回顶部 -->
    <BackTop />
  </v-app>
</template>

<script setup lang="ts">
import { storeToRefs } from "pinia";
import TopNavBar from "~/components/layout/TopNavBar.vue";
import SideNavBar from "~/components/layout/SideNavBar.vue";
import Footer from "~/components/layout/Footer.vue";
import BackTop from "~/components/BackTop.vue";
import { useThemeSettingStore } from "~/stores/themeSetting";
import { computed } from "vue";
import { usePageContext } from "./usePageContext";
const { theme } = storeToRefs(useThemeSettingStore());
const route = usePageContext();
const showFooter = computed(() => {
  return !route.urlPathname.startsWith("/message");
});
</script>
