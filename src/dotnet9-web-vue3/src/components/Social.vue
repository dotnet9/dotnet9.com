<template>
  <ul class="flex flex-row justify-evenly flex-wrap w-full py-4 px-2 text-center items-center">
    <a :href="websiteConfig.github" target="_blank" ref="github" v-if="websiteConfig.github">
      <li class="diamond-clip-path diamond-icon">
        <svg-icon icon-class="github" class="fill-current" />
      </li>
    </a>
    <a :href="websiteConfig.gitee" target="_blank" ref="github" v-if="websiteConfig.gitee">
      <li class="diamond-clip-path diamond-icon">
        <svg-icon icon-class="gitee" class="fill-current" />
      </li>
    </a>
    <a :href="websiteConfig.ownerWechat" target="_blank" ref="wechat" v-if="websiteConfig.ownerWechat">
      <li class="diamond-clip-path diamond-icon">
        <svg-icon icon-class="wechat" class="fill-current" />
      </li>
    </a>
    <a :href="websiteConfig.qq" target="_blank" ref="qq" v-if="websiteConfig.qq">
      <li class="diamond-clip-path diamond-icon">
        <svg-icon icon-class="qq" class="fill-current" />
      </li>
    </a>
    <a :href="websiteConfig.csdn" target="_blank" ref="csdn" v-if="websiteConfig.csdn">
      <li class="diamond-clip-path diamond-icon">
        <svg-icon icon-class="csdn" class="fill-current" />
      </li>
    </a>
    <a :href="websiteConfig.zhiHu" target="_blank" ref="zhifu" v-if="websiteConfig.zhiHu">
      <li class="diamond-clip-path diamond-icon">
        <svg-icon icon-class="zhifu" class="fill-current" />
      </li>
    </a>
  </ul>
</template>

<script lang="ts">
import { computed, defineComponent, toRefs, toRef } from 'vue'
import { useAppStore } from '@/stores/app'

export default defineComponent({
  name: 'Social',
  setup() {
    let appStore = useAppStore()
    return {
      websiteConfig: computed(() => {
        return appStore.websiteConfig
      })
    }
  }
})
</script>

<style scoped>
.custom-social-svg-icon {
  width: 1em;
  height: 1em;
  font-size: 1em;
  vertical-align: -0.15em;
  fill: var(--text-bright);
  stroke: var(--background-primary);
  overflow: hidden;
}
</style>
