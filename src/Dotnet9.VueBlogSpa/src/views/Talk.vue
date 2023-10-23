<template>
  <!-- banner -->
  <div class="banner" :style="cover">
    <h1 class="banner-title">说说</h1>
  </div>
  <!-- 说说内容 -->
  <v-card class="blog-container">
    <div class="talk-wrapper">
      <!-- 用户信息 -->
      <div class="user-info-wrapper">
        <v-avatar size="36" class="user-avatar" :image="info.avatar!" />
        <div class="user-detail-wrapper">
          <div class="user-nickname">
            {{ info.nikeName }}
            <v-icon class="user-sign" size="20" color="#ffa51e">
              mdi-check-decagram
            </v-icon>
          </div>
          <!-- 发表时间 -->
          <div class="time">{{ state.talk.createdTime }}</div>
          <!-- 说说信息 -->
          <div class="talk-content" v-html="state.talk.content" />
          <!-- 图片列表 -->
          <v-row class="talk-images" v-if="state.talk.images">
            <v-col
              :md="4"
              :cols="6"
              v-for="(img, index) of state.talk.images.split(',')"
              :key="index"
            >
              <v-img
                class="images-items"
                :src="img"
                aspect-ratio="1"
                max-height="200"
                @click="previewImg"
              />
            </v-col>
          </v-row>
          <!-- 说说操作 -->
          <div class="talk-operation">
            <div class="talk-operation-item">
              <v-icon
                size="16"
                :class="state.talk.isPraise ? 'like-btn-active' : 'like-btn'"
                @click.prevent="like"
              >
                mdi-thumb-up
              </v-icon>
              <div class="operation-count">
                {{ state.talk.upvote ?? 0 }}
              </div>
            </div>
            <div class="talk-operation-item">
              <v-icon size="16" color="#999">mdi-chat</v-icon>
              <div class="operation-count">
                {{ state.commentCount }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- 评论 -->
    <Comment
      :type="state.id"
      @getCommentCount="getCommentCount"
      v-if="state.talk.isAllowComments"
    />
  </v-card>
</template>

<script setup lang="ts">
import Comment from "../components/Comment.vue";
import { reactive, computed, onMounted } from "vue";
import TalksApi from "@/api/TalksApi";
import { useRoute } from "vue-router";
import { useApp } from "@/stores/app";
import Viewer from "viewerjs";
import "viewerjs/dist/viewer.css";
import type { TalkDetailOutput } from "@/api/models";
import { storeToRefs } from "pinia";
import CommentApi from "@/api/CommentApi";
const appStore = useApp();
const { info } = storeToRefs(appStore);
const route = useRoute();
const state = reactive({
  commentCount: 0,
  id: 0,
  talk: {} as TalkDetailOutput,
});
const cover = computed(() => {
  return (
    "background: url(" +
    appStore.tagListCover() +
    ") center center / cover no-repeat"
  );
});

const getCommentCount = (count: number) => {
  state.commentCount = count;
};
const previewImg = (e: Event): void => {
  const viewer = new Viewer(e.target as HTMLElement, {
    // exit(){
    // }
  });
  viewer.show();
};
const like = async () => {
  const { succeeded, data } = await CommentApi.praise(state.id);
  if (succeeded) {
    state.talk.isPraise = data;
    state.talk.upvote = data ? state.talk.upvote! + 1 : state.talk.upvote! - 1;
  }
};
onMounted(async () => {
  state.id = route.params.talkId as never as number;
  const { data, succeeded } = await TalksApi.talkDetail(state.id);
  if (succeeded) {
    state.talk = data ?? {};
  }
});
</script>

<style scoped>
.col-xl,
.col-xl-auto,
.col-xl-12,
.col-xl-11,
.col-xl-10,
.col-xl-9,
.col-xl-8,
.col-xl-7,
.col-xl-6,
.col-xl-5,
.col-xl-4,
.col-xl-3,
.col-xl-2,
.col-xl-1,
.col-lg,
.col-lg-auto,
.col-lg-12,
.col-lg-11,
.col-lg-10,
.col-lg-9,
.col-lg-8,
.col-lg-7,
.col-lg-6,
.col-lg-5,
.col-lg-4,
.col-lg-3,
.col-lg-2,
.col-lg-1,
.col-md,
.col-md-auto,
.col-md-12,
.col-md-11,
.col-md-10,
.col-md-9,
.col-md-8,
.col-md-7,
.col-md-6,
.col-md-5,
.col-md-4,
.col-md-3,
.col-md-2,
.col-md-1,
.col-sm,
.col-sm-auto,
.col-sm-12,
.col-sm-11,
.col-sm-10,
.col-sm-9,
.col-sm-8,
.col-sm-7,
.col-sm-6,
.col-sm-5,
.col-sm-4,
.col-sm-3,
.col-sm-2,
.col-sm-1,
.col,
.col-auto,
.col-12,
.col-11,
.col-10,
.col-9,
.col-8,
.col-7,
.col-6,
.col-5,
.col-4,
.col-3,
.col-2,
.col-1 {
  width: 100%;
  padding: 2px !important;
}
:deep(.comment-title) {
  margin-top: 20px;
}
.talk-wrapper {
  padding: 16px 20px;
  border-radius: 10px;
  background: rgba(255, 255, 255, 0.1);
  box-shadow: 0 3px 8px 6px rgb(7 17 27 / 6%);
  transition: all 0.3s ease 0s;
}
.talk-wrapper:hover {
  box-shadow: 0 5px 10px 8px rgb(7 17 27 / 16%);
  transform: translateY(-3px);
}
.user-info-wrapper {
  width: 100%;
  display: flex;
}
.user-avatar {
  border-radius: 50%;
}
.user-avatar {
  transition: all 0.5s;
}
.user-avatar:hover {
  transform: rotate(360deg);
}
.user-detail-wrapper {
  margin-left: 10px;
  width: 100%;
  flex: 1;
  width: 0;
}
.user-nickname {
  font-size: 15px;
  font-weight: bold;
  vertical-align: middle;
}
.user-sign {
  margin-left: 4px;
}
.time {
  color: #999;
  margin-top: 2px;
  font-size: 12px;
}
.talk-content {
  margin-top: 8px;
  font-size: 14px;
  white-space: pre-line;
  word-wrap: break-word;
  word-break: break-all;
}
.talk-images {
  padding: 0 10px;
  margin-top: 8px;
}
.images-items {
  cursor: pointer;
  border-radius: 4px;
}
.talk-operation {
  display: flex;
  align-items: center;
  margin-top: 10px;
}
.talk-operation-item {
  display: flex;
  align-items: center;
  margin-right: 40px;
  font-size: 12px;
}
.operation-count {
  margin-left: 4px;
}
.load-wrapper {
  margin-top: 20px;
  display: flex;
  justify-content: center;
  align-items: center;
}
.load-wrapper button {
  background-color: #49b1f5;
  color: #fff;
}
.comment-wrapper {
  margin-top: 20px;
}
.like-btn {
  color: #b3b3b3;
}
.like-btn:hover {
  color: #eb5055 !important;
}
.like-btn-active {
  color: #eb5055 !important;
}
</style>
