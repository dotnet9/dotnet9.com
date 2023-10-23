<template>
  <div class="about-banner banner" :style="cover">
    <h1 class="banner-title">关于我</h1>
  </div>
  <!-- 关于我内容 -->
  <v-card class="blog-container">
    <div class="my-wrapper">
      <v-avatar size="110" class="author-avatar" :image="info.avatar ?? img">
      </v-avatar>
    </div>
    <div class="about-content markdown-body" v-html="info.about" />
  </v-card>
</template>

<script setup lang="ts">
import { useApp } from "~/stores/app";
import img from "~/assets/images/1.jpg";
import { computed } from "vue";
import { BloggerInfo } from "~/api/models";
defineProps<{ info: BloggerInfo }>();
const appStore = useApp();
const cover = computed(() => {
  return (
    "background: url(" +
    appStore.aboutCover() +
    ") center center / cover no-repeat"
  );
});
</script>

<style scoped>
.about-banner {
  /* background: url(https://static.talkxj.com/config/2a56d15dd742ff8ac238a512d9a472a1.jpg) center center / cover
      no-repeat; */
  background-color: #49b1f5;
}
.about-content {
  word-break: break-word;
  line-height: 1.8;
  font-size: 14px;
}
.my-wrapper {
  text-align: center;
}
.author-avatar {
  transition: all 0.5s;
}
.author-avatar:hover {
  transform: rotate(360deg);
}
.about-content {
  margin-top: 20px;
}
</style>
