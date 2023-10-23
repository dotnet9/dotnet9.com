<template>
  <!-- banner -->
  <div class="message-banner" :style="cover">
    <!-- 弹幕输入框 -->
    <div class="message-container">
      <h1 class="message-title">留言板</h1>
      <div class="animated fadeInUp message-input-wrapper">
        <input
          v-model="state.content"
          @click="state.show = true"
          @keyup.enter="addToList"
          placeholder="说点什么吧"
        />
        <button
          class="ml-3 animated bounceInLeft"
          @click="addToList"
          v-show="state.show"
        >
          发送
        </button>
      </div>
    </div>
    <!-- 弹幕列表 -->
    <div class="barrage-container">
      <vue-danmaku
        ref="danmaku"
        v-model:danmus="state.items"
        useSlot
        :loop="loop"
        randomChannel
        :speeds="150"
      >
        <template v-slot:dm="{ index, danmu }">
          <span class="barrage-items" :key="index">
            <img
              :src="danmu.avatar"
              width="30"
              height="30"
              style="border-radius: 50%"
            />
            <span class="ml-2">{{ danmu.nickName }} :</span>
            <span class="ml-2">{{ danmu.content }}</span>
          </span>
        </template>
      </vue-danmaku>
    </div>
  </div>
</template>
<script setup lang="ts">
//弹幕开源地址：https://github.com/hellodigua/vue-danmaku/tree/vue3
import vueDanmaku from "vue3-danmaku";
import { computed, ref, reactive, onMounted } from "vue";
import { useApp } from "@/stores/app";
import { useAuth } from "@/stores/auth";
import CommentApi from "@/api/CommentApi";
import { CommentOutput } from "@/api/models";
import { storeToRefs } from "pinia";
import { useToast } from "@/stores/toast";
const appStore = useApp();
const authStore = useAuth();
const toast = useToast();
const { info } = storeToRefs(authStore);
const state = reactive({
  content: "",
  items: [] as CommentOutput[],
  show: false,
});

// 发送弹幕
const addToList = async () => {
  if (!state.content) {
    toast.error("请输入内容");
    return;
  }
  if (!info.value) {
    toast.error("请登录后发表留言");
    return;
  }
  const { succeeded } = await CommentApi.add({
    content: state.content,
  });
  if (succeeded) {
    state.items.push({
      content: state.content,
      avatar: authStore.info?.avatar,
    });
    state.content = "";
  }
};
// 弹幕实例
const danmaku = ref<any>(null);

const cover = computed(() => {
  return (
    "background: url(" +
    appStore.messageCover() +
    ") center center / cover no-repeat"
  );
});

// 循环播放
const loop = computed(() => {
  return state.items.length > 100;
});

onMounted(async () => {
  const { data, succeeded } = await CommentApi.list({
    pageNo: 1,
    pageSize: 1000,
  });
  if (succeeded && (data?.rows?.length ?? 0) > 0) {
    state.items.push(...data!.rows!);
  }
});
</script>

<style scoped>
.message-banner {
  position: absolute;
  top: -60px;
  left: 0;
  right: 0;
  height: 100vh;
  /* background: url(https://www.static.talkxj.com/d5ojdj.jpg) center center /
    cover no-repeat; */
  background-color: #49b1f5;
  animation: header-effect 1s;
  margin-top: 60px;
}
.message-title {
  color: #eee;
  animation: title-scale 1s;
}
.message-container {
  position: absolute;
  width: 360px;
  top: 35%;
  left: 0;
  right: 0;
  text-align: center;
  z-index: 5;
  margin: 0 auto;
  color: #fff;
}
.message-input-wrapper {
  display: flex;
  justify-content: center;
  height: 2.5rem;
  margin-top: 2rem;
}
.message-input-wrapper input {
  outline: none;
  width: 70%;
  border-radius: 20px;
  height: 100%;
  padding: 0 1.25rem;
  color: #eee;
  border: #fff 1px solid;
}
.message-input-wrapper input::-webkit-input-placeholder {
  color: #eeee;
}
.message-input-wrapper button {
  outline: none;
  border-radius: 20px;
  height: 100%;
  padding: 0 1.25rem;
  border: #fff 1px solid;
}
.barrage-container {
  position: absolute;
  top: 50px;
  left: 0;
  right: 0;
  bottom: 0;
  height: calc(100% -50px);
  width: 100%;
}
.barrage-items {
  background: rgb(0, 0, 0, 0.7);
  border-radius: 100px;
  color: #fff;
  padding: 5px 10px 5px 5px;
  align-items: center;
  display: flex;
}
.barrage-container .vue-danmaku {
  height: 100%;
}
</style>
