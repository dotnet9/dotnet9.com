<template>
  <!-- banner -->
  <div class="banner" :style="cover">
    <h1 class="banner-title">{{ photos.photoAlbumName }}</h1>
  </div>
  <!-- 相册列表 -->
  <v-card class="blog-container">
    <div class="photo-wrap" id="photos">
      <img
        v-for="(item, index) of photos.photoList"
        class="photo"
        :key="index"
        :src="item"
      />
    </div>
    <!-- 无限加载 -->
    <!-- <infinite-loading @infinite="infiniteHandler">
      <div slot="no-more" />
      <div slot="no-results" />
    </infinite-loading> -->
  </v-card>
</template>

<script setup lang="ts">
import { photos } from "../../api/data";
import { computed, onMounted, onUnmounted, ref } from "vue";
import Viewer from "viewerjs";
import "viewerjs/dist/viewer.css";
const viewer = ref<Viewer | null>(null);
onMounted(() => {
  viewer.value = new Viewer(document.getElementById("photos") as HTMLElement);
});

onUnmounted(() => {
  viewer.value?.destroy();
});

const cover = computed(() => {
  return (
    "background: url(" +
    photos.photoAlbumCover +
    ") center center / cover no-repeat"
  );
});
</script>

<style scoped>
.photo-wrap {
  display: flex;
  flex-wrap: wrap;
}
.photo {
  margin: 3px;
  cursor: pointer;
  flex-grow: 1;
  object-fit: cover;
  height: 200px;
}
.photo-wrap::after {
  content: "";
  display: block;
  flex-grow: 9999;
}
@media (max-width: 759px) {
  .photo {
    width: 100%;
  }
}
</style>
