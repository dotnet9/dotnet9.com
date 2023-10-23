<template>
  <!-- banner -->
  <div class="tag-banner banner" :style="cover">
    <h1 class="banner-title">标签</h1>
  </div>
  <!-- 标签列表 -->
  <v-card class="blog-container">
    <div class="tag-cloud-title">标签 - {{ tags.length }}</div>
    <div class="tag-cloud">
      <a
        :style="{ 'font-size': Math.floor(Math.random() * 10) + 18 + 'px' }"
        v-for="item of tags"
        :key="item.id"
        :href="'/tags/' + item.id"
      >
        {{ item.name }}
      </a>
    </div>
  </v-card>
</template>

<script setup lang="ts">
import { computed } from "vue";
import { useApp } from "~/stores/app";
import type { TagsOutput } from "~/api/models";
const appStore = useApp();
defineProps<{ tags: TagsOutput[] }>();
const cover = computed(() => {
  return (
    "background: url(" +
    appStore.tagCover() +
    ") center center / cover no-repeat"
  );
});
</script>

<style scoped lang="scss">
.tag-banner {
  // background: url(https://www.static.talkxj.com/73lleo.png) center center /
  //   cover no-repeat;
  background-color: #49b1f5;
}
.tag-cloud-title {
  line-height: 2;
  font-size: 36px;
  text-align: center;
}
@media (max-width: 759px) {
  .tag-cloud-title {
    font-size: 25px;
  }
}
.tag-cloud {
  text-align: center;
  a {
    color: #616161;
    display: inline-block;
    text-decoration: none;
    padding: 0 8px;
    line-height: 2;
    transition: all 0.3s;
  }
  a:hover {
    color: #03a9f4 !important;
    transform: scale(1.1);
  }
}
</style>
