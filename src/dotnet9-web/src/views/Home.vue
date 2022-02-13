<template>
  <div class="home">
    <div class="app-content">
      <main class="home-container">
        <a-row :gutter="20" style="margin-top: 24px">
          <a-col :offset="2" :span="12">
            <div class="home-swiper-wrapper swiper-slide">
              <img
                src="../assets/6fd7f53b8f9b4f6caff05dfb981707a7.jpg"
                style="height: 340px; width: 100%"
              />
            </div>
          </a-col>

          <a-col :span="6">
            <a-card
              v-if="userInfo == null"
              style="padding: 24px; text-align: center"
            >
              <img src="../assets/052bf99.svg" alt="缺省图" />

              <div style="font-size: 20px; text-align: center">
                <span>加入</span>
                <span
                  style="color: #18ad91; font-size: xx-large; font-weight: 500"
                  >社区</span
                >
              </div>

              <div style="margin-top: 10px">
                与百万开发者一起探讨技术、实践创新
              </div>

              <a-row
                style="margin: 5px auto 0"
                :gutter="20"
                type="flex"
                justify="space-around"
                align="middle"
              >
                <a-col :span="12">
                  <router-link to="/Login" style="float: right">
                    <a-radio-button
                      style="
                        background-color: rgb(24, 173, 145);
                        border-color: rgb(24, 173, 145);
                        color: aliceblue;
                      "
                    >
                      登录
                    </a-radio-button>
                  </router-link>
                </a-col>
                <a-col :span="12">
                  <router-link to="/Register" style="float: left">
                    <a-radio-button>注册</a-radio-button>
                  </router-link>
                </a-col>
              </a-row>
            </a-card>

            <a-card title="用户信息" v-else>
              <a-row>
                <a-col :span="6">
                  <a-avatar :size="64">
                    <template #icon>
                      <img src="../assets/608144857.jpg" /> </template
                  ></a-avatar>
                </a-col>
                <a-col :span="18" style="line-height: 64px">
                  <h2>{{ userInfo.userName }}</h2>
                </a-col>
              </a-row>

              <h3 style="margin-top: 22px">
                个人介绍：{{ userInfo.introduction }}
              </h3>
            </a-card>
          </a-col>
        </a-row>

        <a-row :gutter="20" style="margin-top: 24px">
          <a-col :offset="2" :span="12">
            <a-card title="技术问答">
              <template #extra>
                <router-link to="/BlogPostList">问答首页</router-link>
              </template>

              <b-question
                v-for="item in questionList"
                :key="item.id"
                :comments="item.comments"
                :tag="item.tag"
                :title="item.title"
              >
              </b-question>
            </a-card>
          </a-col>
          <a-col :span="6">
            <a-card class="box-card" style="text-align: center">
              <a-row :gutter="20">
                <a-col :span="12">
                  <div class="grid-content bg-purple">
                    <img src="../assets/2ff4e61.svg" alt="发表文章icon" />
                    <router-link to="blogPostCreate">
                      <div class="action-text">发表文章</div>
                    </router-link>
                  </div>
                </a-col>
                <a-col :span="12">
                  <div class="grid-content bg-purple">
                    <img src="../assets/2f55400.svg" alt="提出问题icon" />
                    <div class="action-text">提出问题</div>
                  </div>
                </a-col>
              </a-row>
            </a-card>

            <a-card title="热门标签" style="margin-top: 24px">
              <template #extra>
                <a href="#">更多 ></a>
              </template>
              <a-tag class="tags-item" color="pink">标签一</a-tag>
              <a-tag class="tags-item" color="red">标签二</a-tag>
            </a-card>
          </a-col>
        </a-row>

        <a-row :gutter="20" style="margin-top: 24px">
          <a-col :offset="2" :span="12">
            <a-card title="优选文章">
              <template #extra>
                <router-link to="BlogPostList">文章首页</router-link>
              </template>

              <b-blogpost
                v-for="item in blogPostList"
                :key="item.id"
                :content="item.content"
                :userName="item.userName"
                :cover="item.cover"
                :title="item.title"
                :createTime="item.createTime"
                @click="gotoBlogPostDetails(item.id)"
              >
              </b-blogpost>
            </a-card>
          </a-col>

          <a-col :span="6">
            <a-card title="推荐作者">
              <b-author
                v-for="item in userInfoList"
                :key="item.id"
                :userName="item.userName"
                :blogPostsCount="item.blogPostsCount"
                :questionsCount="item.questionsCount"
                :headPortrait="item.headPortrait"
              >
              </b-author>
            </a-card>
          </a-col>
        </a-row>
      </main>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, reactive } from "vue";
import BlogPost from "@/components/BlogPost.vue";
import Author from "@/components/Author.vue";
import Question from "@/components/Question.vue";
import request from "@/api/http";
import router from "@/router";
import store from "@/store";

export default defineComponent({
  name: "Home",
  components: {
    "b-blogpost": BlogPost,
    "b-author": Author,
    "b-question": Question,
  },
  setup() {
    let blogPostList = ref([]);
    let questionList = ref([]);
    let userInfoList = ref([]);

    function getBlogPost() {
        blogPostList.value.push({"id": 1, "title": "C# WPF 普通登录界面", "content": "上面的操作还是太简单，相当于只是对文件在操作系统层面进行了复制，如果要完成类似百度网盘的拖拽下载功能（如下图）：", "createTime": "2022-02-13 13:8:32", "userName": "沙漠尽头的狼", "cover": "https://img1.dotnet9.com/2022/01/cover_01.jpeg"});
        blogPostList.value.push({"id": 2, "title": "WPF文件拖拽", "content": "上面的操作还是太简单，相当于只是对文件在操作系统层面进行了复制，如果要完成类似百度网盘的拖拽下载功能（如下图）：", "createTime": "2022-02-13 13:8:32", "userName": "沙漠尽头的狼", "cover": "https://img1.dotnet9.com/2022/01/cover_01.jpeg"});
        blogPostList.value.push({"id": 3, "title": "WPF开发的实用小工具 - 快捷悬浮菜单", "content": "上面的操作还是太简单，相当于只是对文件在操作系统层面进行了复制，如果要完成类似百度网盘的拖拽下载功能（如下图）：", "createTime": "2022-02-13 13:8:32", "userName": "沙漠尽头的狼", "cover": "https://img1.dotnet9.com/2022/01/cover_01.jpeg"});
        blogPostList.value.push({"id": 4, "title": "C# WPF：这次把文件拖出去！", "content": "上面的操作还是太简单，相当于只是对文件在操作系统层面进行了复制，如果要完成类似百度网盘的拖拽下载功能（如下图）：", "createTime": "2022-02-13 13:8:32", "userName": "沙漠尽头的狼", "cover": "https://img1.dotnet9.com/2022/01/cover_01.jpeg"});
      request({
        url: "/Home/GetBlogPost",
      }).then((res: any) => {
        blogPostList.value = res.data.response;
      });
    }

    function getQuestion() {
        questionList.value.push({"title": "WPF开源控件库：MahApps.Metro", "comments": 32, "tag": "wpf,winform,blazor"});
        questionList.value.push({"title": "WPF Prism框架Region失效了？", "comments": 12, "tag": "wpf,prism"});
        questionList.value.push({"title": "怎么做一个专业的软件安装包？", "comments": 65, "tag": "安装包,桌面"});
      request({
        url: "/Home/GetQuestion",
      }).then((res: any) => {
        questionList.value = res.data.response;
      });
    }

    function getUserInfo() {
        userInfoList.value.push({"id": 1, "userName": "天边的云", "blogPostsCount": "23", "questionsCount": "2"});
        userInfoList.value.push({"id": 2, "userName": "南边的大伟", "blogPostsCount": "1", "questionsCount": "2"});
        userInfoList.value.push({"id": 3, "userName": "路路飞", "blogPostsCount": "23", "questionsCount": "2"});
        userInfoList.value.push({"id": 4, "userName": "好飞", "blogPostsCount": "23", "questionsCount": "2"});
      request({
        url: "/Home/GetUserInfo",
      }).then((res: any) => {
        userInfoList.value = res.data.response;
      });
    }

    getBlogPost();
    getQuestion();
    getUserInfo();

    function gotoBlogPostDetails(id: number) {
      router.push("/BlogPostDetails/" + id);
    }

    let tempStore = store;
    let userInfo: any = null;

    if (tempStore.state.token) {
      userInfo = JSON.parse(tempStore.state.userInfo!);
    }

    return {
      blogPostList,
      questionList,
      userInfoList,
      gotoBlogPostDetails,
      userInfo,
    };
  },
});
</script>

<style lang="scss" scoped>
.app-content {
    -webkit-box-flex: 1;
    flex-grow: 1;
}

.tags-item {
    margin: 11.5px 7px;
}
</style>