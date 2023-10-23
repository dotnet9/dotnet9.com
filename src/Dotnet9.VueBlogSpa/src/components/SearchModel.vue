<template>
  <!-- 搜索框 -->
  <v-dialog
    v-bind:model-value="isShow"
    @update:model-value="handlerUpdateValue"
    max-width="600"
    :fullscreen="isMobile"
    scroll-strategy="none"
  >
    <v-card class="search-wrapper" style="border-radius: 4px">
      <div class="mb-3">
        <span class="search-title">本地搜索</span>
        <!-- 关闭按钮 -->
        <v-icon class="float-right" @click="closeHandle"> mdi-close </v-icon>
      </div>
      <!-- 输入框 -->
      <div class="search-input-wrapper">
        <v-icon>mdi-magnify</v-icon>
        <input v-model="keywords" placeholder="输入文章标题或内容..." />
      </div>
      <!-- 搜索结果 -->
      <div class="search-result-wrapper">
        <hr class="divider" />
        <ul>
          <li
            class="search-reslut"
            v-for="item of articleList.list"
            :key="item.id"
          >
            <!-- 文章标题 -->
            <a @click="goTo(item.id)" v-html="item.title" />
            <!-- 文章内容 -->
            <p
              class="search-reslut-content text-justify"
              v-html="item.summary"
            />
          </li>
        </ul>
        <!-- 搜索结果不存在提示 -->
        <div
          v-show="keywords.trim().length === 0 && articleList.list.length == 0"
          style="font-size: 0.875rem"
        >
          找不到您查询的内容：{{ keywords }}
        </div>
      </div>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
import { computed, ref, watch, reactive } from "vue";
import { useRouter } from "vue-router";
import ArticleApi from "@/api/ArticleApi";
import { ArticleOutput } from "@/api/models";
defineProps<{
  isShow: boolean;
}>();
const router = useRouter();
const keywords = ref<string>("");
const emit = defineEmits<{ (e: "update:isShow", isShow: boolean): void }>();
const closeHandle = () => {
  emit("update:isShow", false);
};
const goTo = (id: any) => {
  emit("update:isShow", false);
  articleList.list = [];
  router.push({
    name: "detail",
    params: {
      id,
    },
  });
};
const handlerUpdateValue = (v: boolean) => {
  emit("update:isShow", v);
};
const articleList = reactive({
  list: [] as ArticleOutput[],
});

const isMobile = computed(() => {
  const clientWidth = document.documentElement.clientWidth;
  if (clientWidth > 960) {
    return false;
  }
  return true;
});
watch(keywords, async (val: string) => {
  if (val.trim().length === 0) {
    articleList.list = [];
  } else {
    const { data } = await ArticleApi.list({
      keyword: val,
      pageNo: 1,
      pageSize: 10,
    });
    articleList.list = data?.rows ?? [];
  }
});
watch(isMobile, () => {
  emit("update:isShow", false);
});
</script>
<style scoped>
.search-wrapper {
  padding: 1.25rem;
  height: 100%;
  background: #fff !important;
}
.search-title {
  color: #49b1f5;
  font-size: 1.25rem;
  line-height: 1;
}
.search-input-wrapper {
  display: flex;
  padding: 5px;
  height: 35px;
  width: 100%;
  border: 2px solid #8e8cd8;
  border-radius: 2rem;
}
.search-input-wrapper input {
  width: 100%;
  margin-left: 5px;
  outline: none;
}
@media (min-width: 960px) {
  .search-result-wrapper {
    padding-right: 5px;
    height: 450px;
    overflow: auto;
  }
}
@media (max-width: 959px) {
  .search-result-wrapper {
    height: calc(100vh - 110px);
    overflow: auto;
  }
}
.search-reslut a {
  color: #555;
  font-weight: bold;
  border-bottom: 1px solid #999;
  text-decoration: none;
}
.search-reslut-content {
  color: #555;
  cursor: pointer;
  border-bottom: 1px dashed #ccc;
  padding: 5px 0;
  line-height: 2;
  overflow: hidden;
  text-overflow: ellipsis;
  display: -webkit-box;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
}
.divider {
  margin: 20px 0;
  border: 2px dashed #d2ebfd;
}
</style>
