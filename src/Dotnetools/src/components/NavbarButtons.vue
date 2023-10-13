<script setup lang="ts">
import { BrandGithub, BrandTwitter, InfoCircle, Moon, Sun } from '@vicons/tabler';
import { useStyleStore } from '@/stores/style.store';

const styleStore = useStyleStore();
const { isDarkTheme } = toRefs(styleStore);
</script>

<template>
  <n-tooltip trigger="hover">
    <template #trigger>
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
    </template>
    Github仓库
  </n-tooltip>

  <n-tooltip trigger="hover">
    <template #trigger>
      <c-button circle variant="text" to="/about" aria-label="About">
        <n-icon size="25" :component="InfoCircle" />
      </c-button>
    </template>
    关于
  </n-tooltip>
  <n-tooltip trigger="hover">
    <template #trigger>
      <c-button circle variant="text" aria-label="Toggle dark/light mode" @click="() => styleStore.toggleDark()">
        <n-icon v-if="isDarkTheme" size="25" :component="Sun" />
        <n-icon v-else size="25" :component="Moon" />
      </c-button>
    </template>
    <span v-if="isDarkTheme">浅色模式</span>
    <span v-else>深色模式</span>
  </n-tooltip>
</template>

<style lang="less" scoped>
.n-button {
  &:not(:last-child) {
    margin-right: 5px;
  }
}
</style>
