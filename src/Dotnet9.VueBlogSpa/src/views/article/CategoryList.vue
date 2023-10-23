<template>
  <!-- 标签或分类名 -->
  <div class="banner" :style="cover">
    <h1 class="banner-title animated fadeInDown">
      {{ state.query.categoryId ? "分类" : "标签" }} - {{ state.name }}
    </h1>
  </div>
  <div class="article-list-wrapper">
    <v-row>
      <v-col md="4" cols="12" v-for="item of state.articles" :key="item.id">
        <!-- 文章 -->
        <v-card class="animated zoomIn article-item-card">
          <div class="article-item-cover">
            <router-link :to="'/articles/' + item.id">
              <!-- 缩略图 -->
              <v-img
                class="on-hover"
                width="100%"
                height="100%"
                :src="item.cover ?? ''"
                :cover="true"
              />
            </router-link>
          </div>
          <div class="article-item-info">
            <!-- 文章标题 -->
            <div>
              <router-link :to="'/articles/' + item.id">
                {{ item.title }}
              </router-link>
            </div>
            <div style="margin-top: 0.375rem">
              <!-- 发表时间 -->
              <v-icon size="20">mdi-clock-outline</v-icon>
              {{ item.publishTime }}
              <!-- 文章分类 -->
              <router-link
                :to="'/categories/' + item.categoryId"
                class="float-right"
              >
                <v-icon>mdi-bookmark</v-icon>{{ item.categoryName }}
              </router-link>
            </div>
          </div>
          <!-- 分割线 -->
          <v-divider></v-divider>
          <!-- 文章标签 -->
          <div class="tag-wrapper">
            <router-link
              :to="'/tags/' + tag.id"
              class="tag-btn"
              v-for="tag of item.tags ?? []"
              :key="tag.id"
            >
              {{ tag.name }}
            </router-link>
          </div>
        </v-card>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-pagination
          v-if="state.pages > 1"
          v-model="state.query.pageNo"
          style="margin: 20px 0"
          size="x-small"
          :length="state.pages"
          active-color="#00C4B6"
          :total-visible="3"
          variant="elevated"
        ></v-pagination>
      </v-col>
    </v-row>
  </div>
</template>

<script setup lang="ts">
import { reactive, computed, onMounted, watch } from "vue";
import ArticleApi from "@/api/ArticleApi";
import { useApp } from "@/stores/app";
import { useRoute } from "vue-router";
import type { ArticleListQueryInput } from "@/api/models/article-list-query-input";
import type { ArticleOutput } from "@/api/models";
const route = useRoute();
const appStore = useApp();
const state = reactive({
  query: {
    pageNo: 1,
    pageSize: 10,
    categoryId: route.params.id as never,
    tagId: route.params.tid as never,
  } as ArticleListQueryInput,
  name: "", //标签名或栏目名称
  cover: "",
  pages: 0,
  articles: [] as ArticleOutput[],
});
const cover = computed(() => {
  return "background: url(" + state.cover + ") center center / cover no-repeat";
});

const loadData = async () => {
  const { data, succeeded, extras } = await ArticleApi.list(state.query);
  if (succeeded) {
    state.articles = data?.rows ?? [];
    state.pages = data?.pages ?? 0;
    state.name = extras.name;
    state.cover = extras.cover;
  }
};

watch(
  () => state.query.pageNo,
  async () => {
    window.scrollTo(0, 0);
    await loadData();
  }
);
onMounted(async () => {
  await loadData();
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
