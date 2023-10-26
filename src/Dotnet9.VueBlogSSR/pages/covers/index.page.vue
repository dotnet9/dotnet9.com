<template>
  <div>
    <!-- banner -->
    <div class="banner" :style="cover">
      <h1 class="banner-title">模块</h1>
    </div>
    <!-- 模块内容 -->
    <v-card class="blog-container">
      <v-row>
        <v-col
          :md="6"
          v-for="item of covers"
          :key="item.id"
          style="flex-basis: auto"
        >
          <div class="cover-item">
            <v-img class="cover-cover" :src="item.cover!" cover />
            <a
              class="cover-wrapper"
              :href="'/covers/' + item.id"
              :title="item.name!"
            >
              <div class="cover-name">{{ item.name }}</div>
              <div class="cover-desc">{{ item.remark ?? item.name }}</div>
            </a>
          </div>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-pagination
            v-if="pages > 1"
            v-model="page"
            size="x-small"
            :length="pages"
            active-color="#00C4B6"
            :total-visible="3"
            variant="elevated"
          ></v-pagination>
        </v-col>
      </v-row>
      <!-- <div class="load-wrapper" v-if="state.pages > 1">
          <v-btn outlined> 加载更多... </v-btn>
        </div> -->
    </v-card>
  </div>
</template>

<script setup lang="ts">
import { computed, ref, watch } from "vue";
import { useApp } from "~/stores/app";
import type { CoversOutput } from "~/api/models";
import { navigate } from "vite-plugin-ssr/client/router";
const props = defineProps<{
  pages: number;
  pageNo: number;
  total: 0;
  covers: CoversOutput[];
}>();
const page = ref(props.pageNo);
const appStore = useApp();
const cover = computed(() => {
  return (
    "background: url(" +
    appStore.coverCover() +
    ") center center / cover no-repeat"
  );
});
watch(page, async () => {
  await navigate("/covers?page=" + page.value);
});
</script>

<style lang="scss" scoped>
.cover-item {
  overflow: hidden;
  position: relative;
  cursor: pointer;
  background: #000;
  border-radius: 0.5rem !important;
}
.cover-cover {
  position: relative;
  max-width: none;
  width: calc(100% + 1.25rem);
  height: 250px;
  opacity: 0.8;
  transition: opacity 0.35s, transform 0.35s;
  transform: translate3d(-10px, 0, 0);
  object-fit: cover;
}
.cover-wrapper {
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  padding: 1.8rem 2rem;
  color: #fff !important;
}
.cover-item:hover .cover-cover {
  transform: translate3d(0, 0, 0);
  opacity: 0.4;
}
.cover-item:hover .cover-name:after {
  transform: translate3d(0, 0, 0);
}
.cover-item:hover .cover-desc {
  opacity: 1;
  filter: none;
  transform: translate3d(0, 0, 0);
}
.cover-name {
  font-weight: bold;
  font-size: 1.25rem;
  overflow: hidden;
  padding: 0.7rem 0;
  position: relative;
}
.cover-name:after {
  position: absolute;
  bottom: 0;
  left: 0;
  width: 100%;
  height: 2px;
  background: #fff;
  content: "";
  transition: transform 0.35s;
  transform: translate3d(-101%, 0, 0);
}
.cover-desc {
  margin: 0;
  padding: 0.4rem 0 0;
  line-height: 1.5;
  opacity: 0;
  transition: opacity 0.35s, transform 0.35s;
  transform: translate3d(100%, 0, 0);
}

.load-wrapper {
  margin-top: 20px;
  display: flex;
  justify-content: center;
  align-items: center;
  button {
    background-color: #49b1f5;
    color: #fff;
  }
}
</style>
