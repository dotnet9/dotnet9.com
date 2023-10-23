<template>
  <div
    class="reply-input-wrapper"
    :style="{ display: replay.visible ? 'block' : 'none' }"
    ref="reply"
  >
    <textarea
      class="comment-textarea"
      :placeholder="'回复 @' + replay.nickname + '：'"
      auto-grow
      dense
      v-model="replay.commentContent"
    />
    <div class="emoji-container">
      <span
        :class="replay.chooseEmoji ? 'emoji-btn-active' : 'emoji-btn'"
        @click="replay.chooseEmoji = !replay.chooseEmoji"
      >
        <i class="iconfont iconbiaoqing" />
      </span>
      <div style="margin-left: auto">
        <button @click="cancleReply" class="cancle-btn v-comment-btn">
          取消
        </button>
        <button @click="insertReply" class="upload-btn v-comment-btn">
          提交
        </button>
      </div>
    </div>
    <!-- 表情框 -->
    <emoji @addEmoji="addEmoji" :chooseEmoji="replay.chooseEmoji" />
  </div>
</template>

<script setup lang="ts">
import { reactive } from "vue";
import Emoji from "./Emoji.vue";
interface ReplyInfo {
  index: number;
  chooseEmoji: boolean;
  parentId?: number;
  rootId?: number;
  replyAccountId?: number;
  nickname?: string;
  commentContent?: string;
  visible: boolean;
}
const emit = defineEmits<{
  (e: "submit", index: number): void;
}>();
const replay = reactive<ReplyInfo>({
  index: 0,
  chooseEmoji: false,
  visible: false,
});

const cancleReply = () => {
  replay.visible = false;
};

const insertReply = () => {
  replay.commentContent = replay.commentContent?.replace(/<[^<>]*>/g, "");
  //解析表情
  // var reg = /\[.+?\]/g;
  // replay.commentContent = replay.commentContent?.replace(
  //   reg,
  //   function (str: string) {
  //     return (
  //       "<img src= '" +
  //       EmojiList[str] +
  //       "' width='24'height='24' style='margin: 0 1px;vertical-align: text-bottom'/>"
  //     );
  //   }
  // );
  emit("submit", replay.index);
};

const addEmoji = (text: string) => {
  replay.commentContent = replay.commentContent + text;
};

defineExpose({
  replay,
});
</script>

<style scoped>
.reply-input-wrapper {
  border: 1px solid #90939950;
  border-radius: 4px;
  padding: 10px;
  margin: 0 0 10px;
}
</style>
