<template>
  <div>
    <!-- banner -->
    <div class="banner" :style="cover">
      <h1 class="banner-title">归档</h1>
    </div>
    <!-- 归档列表 -->
    <v-card class="blog-container">
      <timeline>
        <timeline-title> 目前共计{{ total }}篇文章，继续加油 </timeline-title>
        <timeline-item v-for="item of articles" :key="item.id">
          <v-card style="padding: 20px 20px">
            <!-- 日期 -->
            <div class="time">{{ item.publishTime }}</div>
            <!-- 文章标题 -->
            <a
              :href="'/articles/' + item.id"
              style="color: #666; text-decoration: none"
              :title="item.title!"
            >
              {{ item.title }}
            </a>
          </v-card>
        </timeline-item>
      </timeline>
      <!-- 分页按钮 -->
      <v-pagination
        v-if="pages > 1"
        size="x-small"
        :length="pages"
        active-color="#00C4B6"
        v-model="pageNo"
        :total-visible="3"
        variant="elevated"
      ></v-pagination>
    </v-card>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch } from "vue";
import { useApp } from "~/stores/app";
import type { ArticleOutput } from "~/api/models";
import { navigate } from "vite-plugin-ssr/client/router";
const props = defineProps<{
  articles: ArticleOutput[];
  pages: number;
  total: number;
  pageNo: number;
}>();
const appStore = useApp();
const cover = computed(
  () =>
    `background: url(${appStore.archivesCover()}) center center / cover no-repeat`
);
const pageNo = ref(props.pageNo);

watch(pageNo, async () => {
  await navigate(`/archives?page=${pageNo.value}`);
  // await loadData();
});
</script>

<style scoped>
.time {
  font-size: 0.75rem;
  color: #555;
  margin-right: 1rem;
}
</style>
