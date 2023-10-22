<template>
  <div
    class="banner"
    :style="{
      background: 'url(/cover/default.jpg) center center / cover no-repeat',
    }"
  >
    <h1 class="banner-title">
      {{ is404 ? "404 Page Not Found" : "500 Internal Error" }}
    </h1>
  </div>
  <div class="blog-container">
    <img :src="is404 ? img404 : img50x" />
  </div>
</template>

<script lang="ts" setup>
import img404 from "~/assets/images/404.png";
import img50x from "~/assets/images/50x.png";
defineProps(["is404"]);
</script>
