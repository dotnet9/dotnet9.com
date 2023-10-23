<template>
  <!-- banner -->
  <div class="link-banner banner" :style="cover">
    <h1 class="banner-title">友情链接</h1>
  </div>
  <!-- 链接列表 -->
  <v-card class="blog-container">
    <div class="link-title mb-1">
      <v-icon color="blue">mdi-link-variant</v-icon> 友情链接
    </div>
    <v-row class="link-container">
      <v-col
        class="link-wrapper"
        md="4"
        cols="12"
        v-for="item in state.links"
        :key="item.id"
      >
        <a :href="item.link!" target="_blank">
          <v-avatar size="65" class="link-avatar" :image="item.logo!">
          </v-avatar>
          <div style="width: 100%; z-index: 10">
            <div class="link-name">{{ item.siteName }}</div>
            <div class="link-intro">{{ item.remark }}</div>
          </div>
        </a>
      </v-col>
    </v-row>
    <!-- 说明 -->
    <div class="link-title mt-4 mb-4">
      <span
        ><v-icon color="blue">mdi-dots-horizontal-circle</v-icon> 添加友链</span
      >
    </div>
    <blockquote>
      <div>名称：{{ blogSetting.siteName }}</div>
      <div>简介：{{ blogSetting.description }}</div>
      <div>头像：{{ blogSetting.logo ?? avatar }}</div>
    </blockquote>
    <div class="mt-5 mb-5">需要交换友链的可前往个人中心填写💖</div>
    <blockquote class="mb-10">
      友链信息展示需要，您的信息格式要包含：名称、介绍、链接、头像
    </blockquote>
    <!-- 评论 -->
    <Comment :type="0" />
  </v-card>
</template>

<script setup lang="ts">
import Comment from "../components/Comment.vue";
import { computed, onMounted, reactive } from "vue";
import { useApp } from "@/stores/app";
import AppApi from "@/api/AppApi";
import { storeToRefs } from "pinia";
import { FriendLinkOutput } from "@/api/models";
const appStore = useApp();
const { blogSetting } = storeToRefs(appStore);
const state = reactive({
  links: [] as FriendLinkOutput[],
});
const avatar = computed(() => {
  return location.origin + "/default.jpg";
});
// 封面图
const cover = computed(() => {
  return (
    "background: url(" +
    appStore.linkCover() +
    ") center center / cover no-repeat"
  );
});
onMounted(async () => {
  const { data, succeeded } = await AppApi.links();
  if (succeeded) {
    state.links = data ?? [];
  }
});
</script>

<style scoped>
blockquote {
  line-height: 2;
  margin: 0;
  font-size: 15px;
  border-left: 0.2rem solid #49b1f5;
  padding: 10px 1rem !important;
  background-color: #ecf7fe;
  border-radius: 4px;
}
.link-banner {
  /* background: url(https://www.static.talkxj.com/ne78w4%20.jpg) center center /
    cover no-repeat; */
  background-color: #49b1f5;
}
.link-title {
  color: #344c67;
  font-size: 21px;
  font-weight: bold;
  line-height: 2;
}
.link-container {
  margin: 10px 10px 0;
}
.link-wrapper {
  position: relative;
  transition: all 0.3s;
  border-radius: 8px;
}
.link-avatar {
  margin-top: 5px;
  margin-left: 10px;
  transition: all 0.5s;
}
@media (max-width: 759px) {
  .link-avatar {
    margin-left: 30px;
  }
}
.link-name {
  text-align: center;
  font-size: 1.25rem;
  font-weight: bold;
  z-index: 1000;
}
.link-intro {
  text-align: center;
  padding: 16px 10px;
  height: 50px;
  font-size: 13px;
  color: #1f2d3d;
  width: 100%;
  line-height: 16px;
  overflow: hidden;
}
.link-wrapper:hover a {
  color: #fff;
}
.link-wrapper:hover .link-intro {
  color: #fff;
}
.link-wrapper:hover .link-avatar {
  transform: rotate(360deg);
}
.link-wrapper a {
  color: #333;
  text-decoration: none;
  display: flex;
  height: 100%;
  width: 100%;
}
.link-wrapper:hover {
  box-shadow: 0 2px 20px #49b1f5;
}
.link-wrapper:hover:before {
  transform: scale(1);
}
.link-wrapper:before {
  position: absolute;
  border-radius: 8px;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  background: #49b1f5 !important;
  content: "";
  transition-timing-function: ease-out;
  transition-duration: 0.3s;
  transition-property: transform;
  transform: scale(0);
}
</style>
