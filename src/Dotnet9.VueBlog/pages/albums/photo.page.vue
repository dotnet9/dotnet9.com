<template>
  <!-- banner -->
  <div
    class="banner"
    :style="{
      background: 'url(' + cover + ') center center / cover no-repeat',
    }"
  >
    <h1 class="banner-title">{{ name }}</h1>
  </div>
  <!-- 相册列表 -->
  <v-card class="blog-container">
    <div class="photo-wrap" id="photos">
      <img
        v-for="(item, index) of photos"
        class="photo"
        :key="index"
        :src="item.url!"
      />
    </div>
  </v-card>
</template>

<script setup lang="ts">
import { onMounted, onUnmounted, ref, nextTick } from "vue";
import Viewer from "viewerjs";
import "viewerjs/dist/viewer.css";
import type { PictureOutput } from "~/api/models";
defineProps<{ photos: PictureOutput[]; cover: string; name: string }>();
const viewer = ref<Viewer | null>(null);
onMounted(() => {
  nextTick(() => {
    viewer.value = new Viewer(document.getElementById("photos")!);
  });
});

onUnmounted(() => {
  viewer.value?.destroy();
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
