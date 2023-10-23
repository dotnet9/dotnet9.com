<template>
  <v-navigation-drawer
    v-model="drawer"
    width="250"
    disable-resize-watcher
    location="right"
    overlay-opacity="0.8"
    temporary
    style="top: 0; z-index: 1010; height: 100vh"
  >
    <!-- 博主介绍 -->
    <div class="blogger-info">
      <v-avatar size="110" :image="info.avatar!" style="margin-bottom: 0.5rem">
      </v-avatar>
    </div>
    <!-- 博客信息 -->
    <div class="blog-info-wrapper">
      <div class="blog-info-data">
        <a href="/articles">
          <div style="font-size: 0.875rem">文章</div>
          <div style="font-size: 1.125rem">{{ report.articleCount }}</div>
        </a>
      </div>
      <div class="blog-info-data">
        <a href="/category">
          <div style="font-size: 0.875rem">分类</div>
          <div style="font-size: 1.125rem">{{ report.categoryCount }}</div>
        </a>
      </div>
      <div class="blog-info-data">
        <a href="/tags">
          <div style="font-size: 0.875rem">标签</div>
          <div style="font-size: 1.125rem">{{ report.tagCount }}</div>
        </a>
      </div>
    </div>
    <hr />
    <!-- 页面导航 -->
    <div class="menu-container">
      <div class="menus-item">
        <a href="/">
          <!-- <i class="iconfont iconzhuye" />  -->
          <v-icon size="small">mdi mdi-home</v-icon>
          首页
        </a>
      </div>
      <div class="menus-item">
        <a href="/archives">
          <!-- <i class="iconfont iconguidang" />  -->
          <v-icon size="small">mdi mdi-text-box</v-icon>
          归档
        </a>
      </div>
      <div class="menus-item">
        <a href="/albums">
          <!-- <i class="iconfont iconxiangce1" />  -->
          <v-icon size="small">mdi mdi-image-size-select-actual</v-icon>
          相册
        </a>
      </div>
      <div class="menus-item">
        <a href="/talks">
          <!-- <i class="iconfont iconpinglun" />  -->
          <v-icon size="small">mdi mdi-message-processing</v-icon>
          说说
        </a>
      </div>
      <div class="menus-item">
        <a href="/category">
          <!-- <i class="iconfont iconfenlei" />  -->
          <v-icon size="small">mdi mdi-view-grid</v-icon>
          分类
        </a>
      </div>
      <div class="menus-item">
        <a href="/tags">
          <!-- <i class="iconfont iconbiaoqian" />  -->
          <v-icon size="small">mdi mdi-tag-multiple</v-icon>
          标签
        </a>
      </div>
      <div class="menus-item">
        <a href="/links">
          <!-- <i class="iconfont iconlianjie" />  -->
          <v-icon size="small">mdi mdi-camera-iris</v-icon>
          友链
        </a>
      </div>
      <div class="menus-item">
        <a href="/about">
          <!-- <i class="iconfont iconzhifeiji" />  -->
          <v-icon
            size="small"
            style="
              transform: rotate(-45deg);
              margin-bottom: 3px;
              margin-right: 1.5em;
            "
            >mdi mdi-send-variant</v-icon
          >
          关于
        </a>
      </div>
      <div class="menus-item" v-if="blogSetting.isAllowMessage">
        <a href="/message">
          <!-- <i class="iconfont iconpinglunzu" />  -->
          <v-icon size="small">mdi mdi-message-bulleted</v-icon>
          留言
        </a>
      </div>
      <div v-if="!authStore.info" class="menus-item">
        <!-- <a><i class="iconfont icondenglu" /> 登录 </a> -->
        <a @click="handleLogin">
          <!-- <i class="iconfont iconqq" />  -->
          <v-icon size="small">mdi mdi-qqchat</v-icon>
          登录
        </a>
      </div>
      <template v-else>
        <div class="menus-item">
          <a href="/user">
            <!-- <i class="iconfont icongerenzhongxin" />  -->
            <v-icon size="small">mdi mdi-account-circle</v-icon>
            个人中心
          </a>
        </div>
        <div class="menus-item">
          <a @click="handleLoginOut">
            <!-- <i class="iconfont icontuichu" />  -->
            <v-icon size="small">mdi mdi-logout</v-icon>
            退出</a
          >
        </div>
      </template>
    </div>
  </v-navigation-drawer>
</template>

<script setup lang="ts">
import { storeToRefs } from "pinia";
import { useDrawerSettingStore } from "../../stores/drawerSetting";
import { useApp } from "../../stores/app";
import { useAuth } from "../../stores/auth";
import OAuthApi from "../../api/OAuthApi";
const authStore = useAuth();
const { drawer } = storeToRefs(useDrawerSettingStore());
const appStore = useApp();
const { blogSetting, info, report } = storeToRefs(appStore);
// 跳转登录
const handleLogin = async () => {
  const { data } = await OAuthApi.get();
  location.href = data!;
};
// 退出登录
const handleLoginOut = () => {
  authStore.logout();
};
</script>

<style scoped>
.blogger-info {
  padding: 26px 30px 0;
  text-align: center;
}
.blog-info-wrapper {
  display: flex;
  align-items: center;
  padding: 12px 10px 0;
}
.blog-info-data {
  flex: 1;
  line-height: 2;
  text-align: center;
}
hr {
  border: 2px dashed #d2ebfd;
  margin: 20px 0;
}
.menu-container {
  padding: 0 10px 40px;
  animation: 0.8s ease 0s 1 normal none running sidebarItem;
}
.menus-item a {
  padding: 6px 30px;
  display: block;
  line-height: 2;
}
.menus-item i {
  margin-right: 2rem;
}
@keyframes sidebarItem {
  0% {
    transform: translateX(200px);
  }
  100% {
    transform: translateX(0);
  }
}
</style>
