<script setup lang="ts">
import { BrandGithub, BrandTwitter, InfoCircle, Moon, Sun } from '@vicons/tabler';
import { useStyleStore } from '@/stores/style.store';

const styleStore = useStyleStore();
const { isDarkTheme } = toRefs(styleStore);
</script>

<template>
  <c-tooltip tooltip="Github仓库" position="bottom">
    <c-button
      circle
      variant="text"
      href="https://github.com/dotnet9/Dotnet9"
      target="_blank"
      rel="noopener noreferrer"
      aria-label="Dotnet工具箱GitHub仓库"
    >
      <n-icon size="25" :component="BrandGithub" />
    </c-button>
  </c-tooltip>

  <c-tooltip tooltip="关于 Dotnet工具箱" position="bottom">
    <c-button circle variant="text" to="/about" aria-label="About">
      <n-icon size="25" :component="InfoCircle" />
    </c-button>
  </c-tooltip>
  <c-tooltip :tooltip="isDarkTheme ? '浅色模式' : '深色模式'" position="bottom">
    <c-button circle variant="text" aria-label="切换 深色/浅色 模式" @click="() => styleStore.toggleDark()">
      <n-icon v-if="isDarkTheme" size="25" :component="Sun" />
      <n-icon v-else size="25" :component="Moon" />
    </c-button>
  </c-tooltip>
</template>

<style lang="less" scoped>
.n-button {
  &:not(:last-child) {
    margin-right: 5px;
  }
}
</style>
