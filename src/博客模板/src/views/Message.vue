<template>
  <!-- banner -->
  <div class="message-banner" :style="cover">
    <!-- 弹幕输入框 -->
    <div class="message-container">
      <h1 class="message-title">留言板</h1>
      <div class="animated fadeInUp message-input-wrapper">
        <input
          v-model="messageContent"
          @click="show = true"
          @keyup.enter="addToList"
          placeholder="说点什么吧"
        />
        <button
          class="ml-3 animated bounceInLeft"
          @click="addToList"
          v-show="show"
        >
          发送
        </button>
      </div>
    </div>
    <!-- 弹幕列表 -->
    <div class="barrage-container">
      <vue-danmaku
        ref="danmaku"
        v-model:danmus="danmus"
        useSlot
        loop
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
            <span class="ml-2">{{ danmu.nickname }} :</span>
            <span class="ml-2">{{ danmu.messageContent }}</span>
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
import { useRoute } from "vue-router";
import { images, messageList } from "../api/data";
const route = useRoute();
const messageContent = ref<string>("");
const show = ref<boolean>(false);

const addToList = (): void => {};
const barrageList = (): void => {};
const danmaku = ref(null);

const danmus = reactive(messageList);
const cover = computed(() => {
  let cover: string = images.find(
    (item) => item.pageLabel === route.name
  )?.pageCover;
  return "background: url(" + cover + ") center center / cover no-repeat";
});

onMounted(() => {
  (danmaku.value as any).play();
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
