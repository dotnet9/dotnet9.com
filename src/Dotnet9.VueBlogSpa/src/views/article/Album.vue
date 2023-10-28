<template>
  <!-- banner -->
  <div class="album-banner banner" :style="cover">
    <h1 class="banner-title">专辑</h1>
  </div>
  <!-- 专辑列表 -->
  <v-card class="blog-container">
    <div class="album-title">专辑 - {{ state.report.albumCount }}</div>
    <ul class="album-list">
      <li
        class="album-list-item"
        v-for="item of state.categories"
        :key="item.id"
      >
        <router-link :to="'/categories/' + item.id">
          {{ item.name }}
          <span class="album-count">({{ item.total }})</span>
        </router-link>
      </li>
    </ul>
  </v-card>
</template>

<script setup lang="ts">
import { reactive, computed, onMounted } from "vue";
import ArticleApi from "@/api/ArticleApi";
import { useApp } from "@/stores/app";
import type { ArticleReportOutput, AlbumOutput } from "@/api/models";
const appStore = useApp();
const state = reactive({
  categories: [] as AlbumOutput[],
  report: {} as ArticleReportOutput,
});
const cover = computed(() => {
  return (
    "background: url(" +
    appStore.albumCover() +
    ") center center / cover no-repeat"
  );
});

onMounted(async () => {
  const [c, r] = await Promise.all([
    ArticleApi.albums(),
    ArticleApi.report(),
  ]);
  state.categories = c.data ?? [];
  state.report = r.data ?? {
    articleCount: 0,
    tagCount: 0,
    albumCount: 0,
    linkCount: 0,
    userCount: 0,
  };
});
</script>

<style scoped>
.album-banner {
  /* background: url(https://static.talkxj.com/config/83be0017d7f1a29441e33083e7706936.jpg)
    center center / cover no-repeat; */
  background-color: #49b1f5;
}
.album-title {
  text-align: center;
  font-size: 36px;
  line-height: 2;
}
@media (max-width: 759px) {
  .album-title {
    font-size: 28px;
  }
}
.album-list {
  margin: 0 1.8rem;
  list-style: none;
}
.album-list-item {
  padding: 8px 1.8rem 8px 0;
}
.album-list-item:before {
  display: inline-block;
  position: relative;
  left: -0.75rem;
  width: 12px;
  height: 12px;
  border: 0.2rem solid #49b1f5;
  border-radius: 50%;
  background: #fff;
  content: "";
  transition-duration: 0.3s;
}
.album-list-item:hover:before {
  border: 0.2rem solid #ff7242;
}
.album-list-item a:hover {
  transition: all 0.3s;
  color: #8e8cd8;
}
.album-list-item a:not(:hover) {
  transition: all 0.3s;
}
.album-count {
  margin-left: 0.5rem;
  font-size: 0.75rem;
  color: #858585;
}
</style>
