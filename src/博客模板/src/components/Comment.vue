<template>
  <div class="comment-title"><i class="iconfont iconpinglunzu" />评论</div>
  <!-- 评论框 -->
  <div class="comment-input-wrapper">
    <div style="display: flex">
      <v-avatar size="40" :image="img" />
      <div style="width: 100%" class="ml-3">
        <div class="comment-input">
          <textarea
            class="comment-textarea"
            v-model="data.commentContent"
            placeholder="留下点什么吧..."
            auto-grow
            dense
          />
        </div>
        <!-- 操作按钮 -->
        <div class="emoji-container">
          <span
            :class="data.chooseEmoji ? 'emoji-btn-active' : 'emoji-btn'"
            @click="data.chooseEmoji = !data.chooseEmoji"
          >
            <i class="iconfont iconbiaoqing" />
          </span>
          <button
            @click="insertComment"
            class="upload-btn v-comment-btn"
            style="margin-left: auto"
          >
            提交
          </button>
        </div>
        <!-- 表情框 -->
        <emoji @addEmoji="addEmoji" :chooseEmoji="data.chooseEmoji" />
      </div>
    </div>
  </div>
  <!-- 评论详情 -->
  <div v-if="data.count > 0 && data.reFresh">
    <!-- 评论数量 -->
    <div class="count">{{ data.count }} 评论</div>
    <!-- 评论列表 -->
    <div
      style="display: flex"
      class="pt-5"
      v-for="(item, index) of data.commentList"
      :key="item.id"
    >
      <!-- 头像 -->
      <v-avatar size="40" class="comment-avatar" :image="item.avatar" />
      <div class="comment-meta">
        <!-- 用户名 -->
        <div class="comment-user">
          <span v-if="!item.webSite">{{ item.nickname }}</span>
          <a v-else :href="item.webSite" target="_blank">
            {{ item.nickname }}
          </a>
          <span class="blogger-tag" v-if="item.userId == 1">博主</span>
        </div>
        <!-- 信息 -->
        <div class="comment-info">
          <!-- 楼层 -->
          <span style="margin-right: 10px">{{ data.count - index }}楼</span>
          <!-- 发表时间 -->
          <span style="margin-right: 10px">{{ item.createTime }}</span>
          <!-- 点赞 -->
          <span
            :class="
              (index % 2 === 0 ? 'like-active' : 'like') /**模拟点赞 */ +
              ' iconfont icondianzan'
            "
          />
          <span v-show="item.likeCount > 0"> {{ item.likeCount }}</span>
          <!-- 回复 -->
          <span class="reply-btn" @click="replyComment(index, item)">
            回复
          </span>
        </div>
        <!-- 评论内容 -->
        <p v-html="item.commentContent" class="comment-content"></p>
        <!-- 回复人 -->
        <div
          style="display: flex"
          v-for="reply of item.replyDTOList"
          :key="reply.id"
        >
          <!-- 头像 -->
          <v-avatar size="36" class="comment-avatar">
            <img :src="reply.avatar" />
          </v-avatar>
          <div class="reply-meta">
            <!-- 用户名 -->
            <div class="comment-user">
              <span v-if="!reply.webSite">{{ reply.nickname }}</span>
              <a v-else :href="reply.webSite" target="_blank">
                {{ reply.nickname }}
              </a>
              <span class="blogger-tag" v-if="reply.userId == 1">博主</span>
            </div>
            <!-- 信息 -->
            <div class="comment-info">
              <!-- 发表时间 -->
              <span style="margin-right: 10px">
                {{ $formatDate(reply.createTime) }}
              </span>
              <!-- 点赞 -->
              <span
                :class="
                  (index % 2 === 0 ? 'like-active' : 'like') /**模拟点赞 */ +
                  ' iconfont icondianzan'
                "
              />
              <span v-show="reply.likeCount > 0"> {{ reply.likeCount }}</span>
              <!-- 回复 -->
              <span class="reply-btn" @click="replyComment(index, reply)">
                回复
              </span>
            </div>
            <!-- 回复内容 -->
            <p class="comment-content">
              <!-- 回复用户名 -->
              <template v-if="reply.replyUserId != item.userId">
                <span v-if="!reply.replyWebSite" class="ml-1">
                  @{{ reply.replyNickname }}
                </span>
                <a
                  v-else
                  :href="reply.replyWebSite"
                  target="_blank"
                  class="comment-nickname ml-1"
                >
                  @{{ reply.replyNickname }}
                </a>
                ，
              </template>
              <span v-html="reply.commentContent" />
            </p>
          </div>
        </div>
        <!-- 回复数量 -->
        <div
          class="mb-3"
          style="font-size: 0.75rem; color: #6d757a"
          v-show="item.replyCount > 3"
          ref="check"
        >
          共
          <b>{{ item.replyCount }}</b>
          条回复，
          <span style="color: #00a1d6; cursor: pointer"> 点击查看 </span>
        </div>
        <!-- 回复分页 -->
        <div
          class="mb-3"
          style="font-size: 0.75rem; color: #222; display: none"
          ref="paging"
        >
          <span style="padding-right: 10px">
            共{{ Math.ceil(item.replyCount / 5) }}页
          </span>
          <paging
            ref="page"
            :totalPage="Math.ceil(item.replyCount / 5)"
            :index="index"
            :commentId="item.id"
            @changeReplyCurrent="changeReplyCurrent"
          />
        </div>
        <!-- 回复框 -->
        <Reply :type="type" ref="reply" @reloadReply="reloadReply" />
      </div>
    </div>
    <!-- 加载按钮 -->
    <div class="load-wrapper">
      <v-btn
        outlined
        v-if="data.count > data.commentList.length"
        @click="listComments"
      >
        加载更多...
      </v-btn>
    </div>
  </div>
  <!-- 没有评论提示 -->
  <div v-else style="padding: 1.25rem; text-align: center">来发评论吧~</div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted, watch, nextTick } from "vue";
import img from "../assets/images/1.jpg";
import Emoji from "./Emoji.vue";
import Reply from "./Replay.vue";
import Paging from "./Paging.vue";
import EmojiList from "../assets/emoji";
import { comments } from "../api/data";
const props = defineProps<{
  type: number;
}>();

const emit = defineEmits<{
  (e: "getCommentCount", count: number): void;
}>();

const data = reactive({
  reFresh: true,
  commentContent: "",
  chooseEmoji: false,
  current: 1,
  commentList: [] as Array<any>,
  count: 0,
});

const reply = ref<Array<InstanceType<typeof Reply>>>([]);

onMounted(() => {
  //模拟加载数据
  listComments();
});

//监听数据变化
watch(
  () => data.commentList,
  () => {
    data.reFresh = false;
    nextTick(() => {
      data.reFresh = true;
    });
  }
);

const changeReplyCurrent = (
  current: number,
  index: number,
  commentId: number
): void => {
  console.log("查看下一条回复");
};

const addEmoji = (key: string): void => {
  data.commentContent += key;
};

//提交评论
const insertComment = (): void => {
  const reg: RegExp = /\[.+?\]/g;
  data.commentContent = data.commentContent.replace(
    reg,
    function (str: string) {
      return (
        "<img src= '" +
        EmojiList[str] +
        "' width='24'height='24' style='margin: 0 1px;vertical-align: text-bottom'/>"
      );
    }
  );
};

const replyComment = (index: number, item: any): void => {
  reply.value[index].replay.commentContent = "";
  reply.value[index].replay.nickname = item.nickname;
  reply.value[index].replay.replyUserId = item.replyUserId;
  reply.value[index].replay.parentId = data.commentList[index].id;
  reply.value[index].replay.chooseEmoji = false;
  reply.value[index].replay.index = index;
  reply.value[index].replay.visible = true;
};

const reloadReply = (): void => {
  console.log("加载回复");
};

const listComments = () => {
  switch (props.type) {
    case 1:
      data.commentList = comments[0].recordList;
      break;
    case 2:
      data.commentList = comments[1].recordList;
      break;
    case 3:
      data.commentList = comments[2].recordList;
      break;
  }
  data.count = comments[0].count as number;
  emit("getCommentCount", data.count);
};
</script>

<style scoped>
.blogger-tag {
  background: #ffa51e;
  font-size: 12px;
  display: inline-block;
  border-radius: 2px;
  color: #fff;
  padding: 0 5px;
  margin-left: 6px;
}
.comment-title {
  display: flex;
  align-items: center;
  font-size: 1.25rem;
  font-weight: bold;
  line-height: 40px;
  margin-bottom: 10px;
}
.comment-title i {
  font-size: 1.5rem;
  margin-right: 5px;
}
.comment-input-wrapper {
  border: 1px solid #90939950;
  border-radius: 4px;
  padding: 10px;
  margin: 0 0 10px;
}
.count {
  padding: 5px;
  line-height: 1.75;
  font-size: 1.25rem;
  font-weight: bold;
}
.comment-meta {
  margin-left: 0.8rem;
  width: 100%;
  border-bottom: 1px dashed #f5f5f5;
}
.reply-meta {
  margin-left: 0.8rem;
  width: 100%;
}
.comment-user {
  font-size: 14px;
  line-height: 1.75;
}
.comment-user a {
  color: #1abc9c !important;
  font-weight: 500;
  transition: 0.3s all;
}
.comment-nickname {
  text-decoration: none;
  color: #1abc9c !important;
  font-weight: 500;
}
.comment-info {
  line-height: 1.75;
  font-size: 0.75rem;
  color: #b3b3b3;
}
.reply-btn {
  cursor: pointer;
  float: right;
  color: #ef2f11;
}
.comment-content {
  font-size: 0.875rem;
  line-height: 1.75;
  padding-top: 0.625rem;
  white-space: pre-line;
  word-wrap: break-word;
  word-break: break-all;
}
.comment-avatar {
  transition: all 0.5s;
}
.comment-avatar:hover {
  transform: rotate(360deg);
}
.like {
  cursor: pointer;
  font-size: 0.875rem;
}
.like:hover {
  color: #eb5055;
}
.like-active {
  cursor: pointer;
  font-size: 0.875rem;
  color: #eb5055;
}
.load-wrapper {
  margin-top: 10px;
  display: flex;
  justify-content: center;
  align-items: center;
}
.load-wrapper button {
  background-color: #49b1f5;
  color: #fff;
}
</style>
