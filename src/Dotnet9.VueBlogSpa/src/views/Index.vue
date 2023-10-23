<template>
  <!-- banner -->
  <div class="home-banner" :style="cover">
    <div class="banner-container">
      <!-- 联系方式 -->
      <h1 class="blog-title animated zoomIn">{{ blogSetting.siteName }}</h1>
      <!-- 一言 -->
      <div class="blog-intro">
        {{ state.print.output }} <span class="typed-cursor">|</span>
      </div>
      <!-- 联系方式 -->
      <div class="blog-contact">
        <a
          class="mr-5 iconfont iconqq"
          target="_blank"
          :href="`http://wpa.qq.com/msgrd?v=3&uin=${info.qq}&site=qq&menu=yes`"
        />
        <a
          target="_blank"
          :href="info.github ?? ''"
          class="mr-5 iconfont icongithub"
        />
        <a
          target="_blank"
          :href="info.gitee ?? ''"
          class="iconfont icongitee-fill-round"
        />
      </div>
    </div>
    <!-- 向下滚动 -->
    <div class="scroll-down" @click="scrollDown">
      <v-icon color="#fff" class="scroll-down-effects">
        mdi-chevron-down
      </v-icon>
    </div>
  </div>
  <!-- 主页文章 -->
  <v-row class="home-container">
    <v-col md="9" cols="12">
      <!-- 说说轮播 -->
      <v-card class="animated zoomIn" v-if="state.talks.length > 0">
        <Swiper :list="state.talks" />
      </v-card>
      <v-card
        class="animated zoomIn article-card"
        :style="{ 'border-radius': '12px 8px 8px 12px' }"
        v-for="(item, index) of state.article.items"
        :key="item.id"
      >
        <!-- 文章封面图 -->
        <div :class="isRight(index)">
          <router-link :to="'/articles/' + item.id">
            <v-img
              class="on-hover"
              width="100%"
              height="100%"
              :src="item.cover!"
              :cover="true"
            />
          </router-link>
        </div>
        <!-- 文章信息 -->
        <div class="article-wrapper">
          <div style="line-height: 1.4">
            <router-link :to="'/articles/' + item.id">
              {{ item.title }}
            </router-link>
          </div>
          <div class="article-info">
            <!-- 是否置顶 -->
            <span v-if="item.isTop">
              <span style="color: #ff7242">
                <i class="iconfont iconzhiding" /> 置顶
              </span>
              <span class="separator">|</span>
            </span>
            <!-- 发表时间 -->
            <v-icon size="14">mdi-calendar-month-outline</v-icon>
            {{ $formatDate(item.publishTime!, "YYYY-MM-DD") }}
            <span class="separator">|</span>
            <!-- 文章分类 -->
            <router-link :to="'/categories/' + item.categoryId">
              <v-icon size="14">mdi-inbox-full</v-icon>
              {{ item.categoryName }}
            </router-link>
            <span class="separator">|</span>
            <!-- 文章标签 -->
            <router-link
              :style="{ display: 'inline-block' }"
              :to="'/tags/' + tag.id"
              class="mr-1"
              v-for="tag of item.tags"
              :key="tag.id"
            >
              <v-icon size="14">mdi-tag-multiple</v-icon>{{ tag.name }}
            </router-link>
          </div>
          <!-- 文章内容 -->
          <div class="article-content">
            {{ item.summary }}
          </div>
        </div>
      </v-card>
      <v-pagination
        v-if="state.article.totalPage > 1"
        v-model="state.article.page"
        style="margin: 20px 0"
        size="x-small"
        :length="state.article.totalPage"
        active-color="#00C4B6"
        :total-visible="3"
        variant="elevated"
      ></v-pagination>
    </v-col>
    <!-- 博主信息 -->
    <v-col md="3" cols="12" class="d-md-block d-none">
      <div class="blog-wrapper">
        <v-card class="animated zoomIn blog-card">
          <div class="author-wrapper">
            <!-- 博主头像 -->
            <v-avatar size="110" class="author-avatar" :image="info.avatar!" />
            <div style="font-size: 1.375rem; margin-top: 0.625rem">
              {{ info.nikeName }}
            </div>
            <div style="font-size: 0.875rem">{{ info.motto }}</div>
          </div>
          <!-- 博客信息 -->
          <div class="blog-info-wrapper">
            <div class="blog-info-data">
              <router-link to="/archives">
                <div style="font-size: 0.875rem">文章</div>
                <div style="font-size: 1.25rem">
                  {{ report.articleCount }}
                </div>
              </router-link>
            </div>
            <div class="blog-info-data">
              <router-link to="/category">
                <div style="font-size: 0.875rem">分类</div>
                <div style="font-size: 1.25rem">
                  {{ report.categoryCount }}
                </div>
              </router-link>
            </div>
            <div class="blog-info-data">
              <router-link to="/tags">
                <div style="font-size: 0.875rem">标签</div>
                <div style="font-size: 1.25rem">
                  {{ report.tagCount }}
                </div>
              </router-link>
            </div>
          </div>
          <!-- 收藏按钮 -->
          <a
            class="collection-btn"
            @click="useToast().info('按CTRL+D 键将本页加入书签')"
          >
            <v-icon color="#fff" size="18" class="mr-1">mdi-bookmark</v-icon>
            加入书签
          </a>
          <!-- 社交信息 -->
          <div class="card-info-social">
            <a
              class="mr-5 iconfont iconqq"
              target="_blank"
              :href="`http://wpa.qq.com/msgrd?v=3&uin=111514&ste=${info.qq}&menu=yes`"
            />
            <a
              target="_blank"
              :href="info.github ?? ''"
              class="mr-5 iconfont icongithub"
            />
            <a
              target="_blank"
              :href="info.gitee ?? ''"
              class="iconfont icongitee-fill-round"
            />
          </div>
        </v-card>
        <!-- 网站信息 -->
        <v-card
          class="blog-card animated zoomIn mt-5 big"
          v-if="blogSetting.announcement"
        >
          <div class="web-info-title">
            <v-icon size="18">mdi-bell</v-icon>
            公告
          </div>
          <div style="font-size: 0.875rem">
            {{ blogSetting.announcement }}
          </div>
        </v-card>
        <!-- 网站信息 -->
        <v-card class="blog-card animated zoomIn mt-5">
          <div class="web-info-title">
            <v-icon size="18">mdi-chart-line</v-icon>
            网站资讯
          </div>
          <div class="web-info">
            <div style="padding: 4px 0 0">
              运行时间:<span class="float-right">{{ state.runTime }}</span>
            </div>
            <div style="padding: 4px 0 0">
              用户数量:<span class="float-right">
                {{ report.userCount }}
              </span>
            </div>
            <div style="padding: 4px 0 0">
              友链数量:<span class="float-right">
                {{ report.linkCount }}
              </span>
            </div>
          </div>
        </v-card>
      </div>
    </v-col>
  </v-row>
</template>

<script setup lang="ts">
import { reactive, onMounted, computed, watch } from "vue";
import { storeToRefs } from "pinia";
import EasyTyper from "easy-typer-js/src/ts";
import Swiper from "../components/Swiper.vue";
import { useApp } from "@/stores/app";
import { useAuth } from "@/stores/auth";
import ArticleApi from "@/api/ArticleApi";
import dayjs from "dayjs";
import type { ArticleOutput, TalksOutput } from "@/api/models";
import TalksApi from "@/api/TalksApi";
import { useRoute, useRouter } from "vue-router";
import { useToast } from "@/stores/toast";
const router = useRouter();
const appStore = useApp();
const route = useRoute();
const { blogSetting, info, report } = storeToRefs(appStore);
const authStore = useAuth();
// 打字机配置
const state = reactive({
  talks: [] as TalksOutput[], // 说说
  print: {
    output: "",
    isEnd: false,
    speed: 300,
    singleBack: false,
    sleep: 0,
    type: "rollback",
    backSpeed: 40,
    sentencePause: true,
  },
  runTime: "", // 运行时长
  article: {
    page: 1,
    pageSize: 10,
    total: 0,
    totalPage: 0,
    items: [] as ArticleOutput[], //文章列表
  },
});

const articlePage = async () => {
  const { data } = await ArticleApi.list({
    pageNo: state.article.page,
    pageSize: state.article.pageSize,
  });
  state.article.items = data?.rows ?? [];
  state.article.total = data?.total ?? 0;
  state.article.totalPage = data?.pages ?? 0;
  return;
};

/**
 * 博客允许时间
 */
const runTime = (): void => {
  const timespan: number =
    new Date().getTime() -
    dayjs(blogSetting.value.runTime ?? "2023/06/01")
      .toDate()
      .getTime();
  const msPerDay: number = 24 * 60 * 60 * 1000;
  const daysold: number = Math.floor(timespan / msPerDay);
  let str: string = "";
  const day: Date = new Date();
  str += daysold + "天";
  str += day.getHours() + "时";
  str += day.getMinutes() + "分";
  str += day.getSeconds() + "秒";
  state.runTime = str;
};

/**
 * 滚动条
 */
const scrollDown = (): void => {
  window.scrollTo({
    behavior: "smooth",
    top: document.documentElement.clientHeight,
  });
};
const isRight = (index: number): string => {
  return index % 2 === 0
    ? "article-cover left-radius"
    : "article-cover right-radius";
};
// 封面图
const cover = computed(() => {
  return `background: url(${appStore.homeCover()}) center center / cover no-repeat`;
});

// 监听页码发生改变
watch(
  () => state.article.page,
  async () => {
    scrollDown();
    await articlePage();
  }
);

onMounted(async () => {
  new EasyTyper(
    state.print,
    blogSetting.value.motto ??
      "虽然人生在世有种种不如意，但你仍可以在幸福与不幸中做选择。-王小波",
    () => {},
    () => {}
  );
  const code = route.query.code || route.params.code;
  if (code) {
    const { data, succeeded } = await authStore.login(code as string);
    if (succeeded && data) {
      router.push(data);
    }
  }
  const [talks] = await Promise.all([
    TalksApi.list({ pageNo: 1, pageSize: 10 }),
    articlePage(),
  ]);
  state.talks = talks.data!.rows!;
  setInterval(() => {
    runTime();
  }, 1000);
});
</script>
<style scoped lang="scss">
.typed-cursor {
  opacity: 1;
  animation: blink 0.7s infinite;
  @keyframes blink {
    0% {
      opacity: 1;
    }
    50% {
      opacity: 0;
    }
    100% {
      opacity: 1;
    }
  }
}

.home-banner {
  position: absolute;
  top: 0px;
  left: 0;
  right: 0;
  // height: 106.5vh;
  height: 100vh;
  background-attachment: fixed;
  text-align: center;
  color: #fff !important;
  animation: header-effect 1s;
}
.banner-container {
  margin-top: 43vh;
  line-height: 1.5;
  color: #eee;
}
.blog-contact a {
  color: #fff !important;
}
.card-info-social {
  line-height: 40px;
  text-align: center;
  margin: 6px 0 -6px;
}
.card-info-social a {
  font-size: 1.5rem;
}
.left-radius {
  border-radius: 8px 0 0 8px !important;
  order: 0;
}
.right-radius {
  border-radius: 0 8px 8px 0 !important;
  order: 1;
}
.article-wrapper {
  font-size: 14px;
}
@media (min-width: 760px) {
  .blog-title {
    font-size: 2.5rem;
  }
  .blog-intro {
    font-size: 1.5rem;
  }
  .blog-contact {
    display: none;
  }
  .home-container {
    max-width: 1200px;
    margin: calc(100vh - 48px) auto 28px auto;
    padding: 0 5px;
  }
  .article-card {
    display: flex;
    align-items: center;
    height: 280px;
    width: 100%;
    margin-top: 20px;
  }
  .article-cover {
    overflow: hidden;
    height: 100%;
    width: 45%;
  }
  .on-hover {
    transition: all 0.6s;
  }
  .article-card:hover .on-hover {
    transform: scale(1.1);
  }
  .article-wrapper {
    padding: 0 2.5rem;
    width: 55%;
  }
  .article-wrapper a {
    font-size: 1.5rem;
    transition: all 0.3s;
  }
}
@media (max-width: 759px) {
  .blog-title {
    font-size: 26px;
  }
  .blog-contact {
    font-size: 1.25rem;
    line-height: 2;
  }
  .home-container {
    width: 100%;
    margin: calc(100vh - 66px) auto 0 auto;
  }
  .article-card {
    margin-top: 1rem;
  }
  .article-cover {
    border-radius: 8px 8px 0 0 !important;
    height: 230px !important;
    width: 100%;
  }
  .article-cover div {
    border-radius: 8px 8px 0 0 !important;
  }
  .article-wrapper {
    padding: 1.25rem 1.25rem 1.875rem;
  }
  .article-wrapper a {
    font-size: 1.25rem;
    transition: all 0.3s;
  }
}
.scroll-down {
  cursor: pointer;
  position: absolute;
  bottom: 15px;
  width: 100%;
}
.scroll-down i {
  font-size: 2rem;
}
.article-wrapper a:hover {
  color: #8e8cd8;
}
.article-info {
  font-size: 95%;
  color: #858585;
  line-height: 2;
  margin: 0.375rem 0;
}
.article-info a {
  font-size: 95%;
  color: #858585 !important;
}
.article-content {
  line-height: 2;
  overflow: hidden;
  text-overflow: ellipsis;
  display: -webkit-box;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
}
.blog-wrapper {
  position: sticky;
  top: 10px;
}
.blog-card {
  line-height: 2;
  padding: 1.25rem 1.5rem;
}
.author-wrapper {
  text-align: center;
}
.blog-info-wrapper {
  display: flex;
  justify-self: center;
  padding: 0.875rem 0;
}
.blog-info-data {
  flex: 1;
  text-align: center;
}
.blog-info-data a {
  text-decoration: none;
}
.collection-btn {
  text-align: center;
  z-index: 1;
  font-size: 14px;
  position: relative;
  display: block;
  background-color: #49b1f5;
  color: #fff !important;
  height: 32px;
  line-height: 32px;
  transition-duration: 1s;
  transition-property: color;
}
.collection-btn:before {
  position: absolute;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  z-index: -1;
  background: #ff7242;
  content: "";
  transition-timing-function: ease-out;
  transition-duration: 0.5s;
  transition-property: transform;
  transform: scaleX(0);
  transform-origin: 0 50%;
}
.collection-btn:hover:before {
  transition-timing-function: cubic-bezier(0.45, 1.64, 0.47, 0.66);
  transform: scaleX(1);
}
.author-avatar {
  transition: all 0.5s;
}
.author-avatar:hover {
  transform: rotate(360deg);
}
.web-info {
  padding: 0.25rem;
  font-size: 0.875rem;
}
.scroll-down-effects {
  color: #eee !important;
  text-align: center;
  text-shadow: 0.1rem 0.1rem 0.2rem rgba(0, 0, 0, 0.15);
  line-height: 1.5;
  display: inline-block;
  text-rendering: auto;
  -webkit-font-smoothing: antialiased;
  animation: scroll-down-effect 1.5s infinite;
}
@keyframes scroll-down-effect {
  0% {
    top: 0;
    opacity: 0.4;
    filter: alpha(opacity=40);
  }
  50% {
    top: -16px;
    opacity: 1;
    filter: none;
  }
  100% {
    top: 0;
    opacity: 0.4;
    filter: alpha(opacity=40);
  }
}
.big i {
  color: #f00;
  animation: big 0.8s linear infinite;
}
@keyframes big {
  0%,
  100% {
    transform: scale(1);
  }
  50% {
    transform: scale(1.2);
  }
}
</style>
