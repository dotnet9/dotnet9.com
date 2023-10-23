<template>
  <!-- banner -->
  <div class="banner" :style="cover">
    <h1 class="banner-title">归档</h1>
  </div>
  <!-- 归档列表 -->
  <v-card class="blog-container">
    <timeline>
      <timeline-title>
        目前共计{{ data.count }}篇文章，继续加油
      </timeline-title>
      <timeline-item v-for="item of data.recordList" :key="item.id">
        <v-card style="padding: 20px 20px;">
          <!-- 日期 -->
          <div class="time">{{ item.createTime }}</div>
          <!-- 文章标题 -->
          <router-link
            :to="'/articles/' + item.id"
            style="color: #666; text-decoration: none"
          >
            {{ item.articleTitle }}
          </router-link>
        </v-card>
      </timeline-item>
    </timeline>
    <!-- 分页按钮 -->
    <v-pagination
      size="x-small"
      :length="Math.ceil(data.count / 10)"
      active-color="#00C4B6"
      v-model="current"
      :total-visible="3"
      variant="elevated"
    ></v-pagination>
  </v-card>
</template>

<script setup lang="ts">
import { Timeline, TimelineTitle, TimelineItem } from "vue3-cute-component";
import { ref, reactive } from "vue";
let current = ref(1);
const cover = ref(
  `background: url(https://xiaogerblog.oss-cn-chengdu.aliyuncs.com/config/c796ff3ca8e6a736cae59f8eda6d9948.jpg) center center / cover no-repeat`
);
const data = reactive({
  recordList: [
    {
      id: 79,
      articleTitle: "Redis哨兵模式",
      createTime: "2023-02-16T15:02:56",
    },
    {
      id: 78,
      articleTitle: "Redis主从复制",
      createTime: "2023-02-15T21:02:42",
    },
    {
      id: 77,
      articleTitle: "Redis持久化机制",
      createTime: "2023-02-15T14:15:52",
    },
    {
      id: 76,
      articleTitle: "Redis6新数据类型",
      createTime: "2023-02-14T21:25:21",
    },
    {
      id: 75,
      articleTitle: "Redis配置文件",
      createTime: "2023-02-14T15:11:20",
    },
    {
      id: 74,
      articleTitle: "源码分析AbstractQueuedSynchronizer 加锁操作",
      createTime: "2022-09-06T15:11:12",
    },
    {
      id: 68,
      articleTitle: "HashMap底层机制和源码分析",
      createTime: "2022-07-18T21:43:06",
    },
    {
      id: 65,
      articleTitle: "HashMap的几种遍历方法",
      createTime: "2022-07-17T21:07:28",
    },
    {
      id: 64,
      articleTitle: "nginx的基本应用",
      createTime: "2022-05-29T21:19:18",
    },
    {
      id: 63,
      articleTitle: "全站https配置",
      createTime: "2022-05-16T22:39:46",
    },
  ],
  count: 14,
});
</script>

<style scoped>
.time {
  font-size: 0.75rem;
  color: #555;
  margin-right: 1rem;
}
</style>
