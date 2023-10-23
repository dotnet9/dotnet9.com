<template>
  <!-- 标签或分类名 -->
  <div class="banner" :style="cover">
    <h1 class="banner-title animated fadeInDown">分类 - {{ name }}</h1>
  </div>
  <div class="article-list-wrapper">
    <v-row>
      <v-col md="4" cols="12" v-for="item of articles" :key="item.id">
        <!-- 文章 -->
        <v-card class="animated zoomIn article-item-card">
          <div class="article-item-cover">
            <a :href="'/articles/' + item.id" :title="item.title!">
              <!-- 缩略图 -->
              <v-img
                class="on-hover"
                width="100%"
                height="100%"
                :src="item.cover ?? ''"
                :cover="true"
              />
            </a>
          </div>
          <div class="article-item-info">
            <!-- 文章标题 -->
            <div>
              <a :href="'/articles/' + item.id" :title="item.title!">
                {{ item.title }}
              </a>
            </div>
            <div style="margin-top: 0.375rem">
              <!-- 发表时间 -->
              <v-icon size="20">mdi-clock-outline</v-icon>
              {{ item.publishTime }}
              <!-- 文章分类 -->
              <a
                class="float-right"
                :href="'/categories/' + item.categoryId"
                :title="item.categoryName!"
              >
                <v-icon>mdi-bookmark</v-icon>{{ item.categoryName }}
              </a>
            </div>
          </div>
          <!-- 分割线 -->
          <v-divider></v-divider>
          <!-- 文章标签 -->
          <div class="tag-wrapper">
            <a
              class="tag-btn"
              v-for="tag of item.tags ?? []"
              :href="'/tags/' + tag.id"
              :title="tag.name!"
              :key="tag.id"
            >
              {{ tag.name }}
            </a>
          </div>
        </v-card>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-pagination
          v-if="pages > 1"
          v-model="page"
          style="margin: 20px 0"
          size="x-small"
          :length="pages"
          active-color="#00C4B6"
          :total-visible="3"
          variant="elevated"
        ></v-pagination>
      </v-col>
    </v-row>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch } from "vue";
import { useApp } from "~/stores/app";
import type { ArticleOutput } from "~/api/models";
import { usePageContext } from "~/renderer/usePageContext";
import { navigate } from "vite-plugin-ssr/client/router";
const props = defineProps<{
  articles: ArticleOutput[];
  pages: number;
  pageNo: number;
  name: string;
  cover: string;
}>();
const route = usePageContext();
const page = ref(props.pageNo);
const cover = computed(() => {
  return "background: url(" + props.cover + ") center center / cover no-repeat";
});

watch(page, async () => {
  await navigate(`/categories/${route.routeParams?.id}?page=${page}`);
});
</script>

<style scoped>
@media (min-width: 760px) {
  .article-list-wrapper {
    max-width: 1106px;
    margin: 300px auto 20px auto !important;
  }
  .article-item-card:hover {
    transition: all 0.3s;
    box-shadow: 0 4px 12px 12px rgba(7, 17, 27, 0.15);
  }
  .article-item-card:not(:hover) {
    transition: all 0.3s;
  }
  .article-item-card:hover .on-hover {
    transition: all 0.6s;
    transform: scale(1.1);
  }
  .article-item-card:not(:hover) .on-hover {
    transition: all 0.6s;
  }
  .article-item-info {
    line-height: 1.7;
    padding: 15px 15px 12px 18px;
    font-size: 15px;
  }
  :deep(.v-pagination) {
    margin-top: 0px !important;
    margin-bottom: 0px !important;
  }
}
@media (max-width: 759px) {
  .article-list-wrapper {
    margin-top: 230px;
    padding: 0 12px;
  }
  .article-item-info {
    line-height: 1.7;
    padding: 15px 15px 12px 18px;
  }
}
.article-item-card {
  border-radius: 8px !important;
  box-shadow: 0 4px 8px 6px rgba(7, 17, 27, 0.06);
}
.article-item-card a {
  transition: all 0.3s;
}
.article-item-cover {
  height: 220px;
  overflow: hidden;
}
.article-item-card a:hover {
  color: #8e8cd8;
}
.tag-wrapper {
  padding: 10px 15px 10px 18px;
}
.tag-wrapper a {
  color: #fff !important;
}
.tag-btn {
  display: inline-block;
  font-size: 0.725rem;
  line-height: 22px;
  height: 22px;
  border-radius: 10px;
  padding: 0 12px !important;
  background: linear-gradient(to right, #bf4643 0%, #6c9d8f 100%);
  opacity: 0.6;
  margin-right: 0.5rem;
}
</style>
