<template>
  <div v-show="chooseEmoji" class="emoji-wrapper">
    <span
      class="emoji-item"
      v-for="(value, key, index) of emojiList"
      :key="index"
      @click="addEmoji(key as string)"
    >
      <v-img
        :lazy-src="value"
        :src="value"
        :title="key"
        class="emoji"
        width="24"
        height="24"
      />
    </span>
  </div>
</template>

<script setup lang="ts">
import emojiList from "../assets/emoji";
//组件参数
defineProps<{
  chooseEmoji: boolean;
}>();

//自定义事件
const emit = defineEmits<{
  (e: "addEmoji", key: string): void;
}>();

const addEmoji = (key: string): void => {
  emit("addEmoji", key); //触发自定义事件
};
</script>

<style scoped>
.emoji {
  user-select: none;
  margin: 0.25rem;
  display: inline-block;
  vertical-align: middle;
}
.emoji-item {
  cursor: pointer;
  display: inline-block;
}
.emoji-item:hover {
  transition: all 0.2s;
  border-radius: 0.25rem;
  background: #dddddd;
}
.emoji-wrapper {
  max-height: 150px;
  overflow-y: auto;
}
</style>
