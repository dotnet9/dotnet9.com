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
            v-model="state.commentContent"
            placeholder="留下点什么吧..."
            auto-grow
            dense
          />
        </div>
        <!-- 操作按钮 -->
        <div class="emoji-container">
          <span
            :class="state.chooseEmoji ? 'emoji-btn-active' : 'emoji-btn'"
            @click="state.chooseEmoji = !state.chooseEmoji"
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
        <emoji @addEmoji="addEmoji" :chooseEmoji="state.chooseEmoji" />
      </div>
    </div>
  </div>
  <!-- 评论详情 -->
  <div v-if="state.count > 0">
    <!-- 评论数量 -->
    <div class="count">{{ state.count }} 评论</div>
    <!-- 评论列表 -->
    <div
      style="display: flex"
      class="pt-5"
      v-for="(item, index) of state.commentList"
      :key="item.id"
    >
      <!-- 头像 -->
      <v-avatar size="40" class="comment-avatar" :image="item.avatar!" />
      <div class="comment-meta">
        <!-- 用户名 -->
        <div class="comment-user">
          <span>{{ item.nickName }}</span>
          <!-- <span v-if="!item.webSite">{{ item.nickname }}</span>
          <a v-else :href="item.webSite" target="_blank">
            {{ item.nickname }}
          </a> -->
          <span class="blogger-tag" v-if="item.isBlogger">博主</span>
        </div>
        <!-- 信息 -->
        <div class="comment-info">
          <!-- 楼层 -->
          <span style="margin-right: 10px">{{ state.count - index }}楼</span>
          <!-- 发表时间 -->
          <span style="margin-right: 10px">{{ item.createdTime }}</span>
          <!-- 点赞 -->
          <span
            :class="
              (item.isPraise ? 'like-active' : 'like') /**模拟点赞 */ +
              ' iconfont icondianzan'
            "
            @click="onPraise(item)"
          />
          <span v-show="(item.praiseTotal ?? 0) > 0">
            {{ item.praiseTotal }}</span
          >
          <!-- 回复 -->
          <span class="reply-btn" @click="replyComment(index, item, item.id)">
            回复
          </span>
        </div>
        <!-- 评论内容 -->
        <p
          v-html="formatContent(item.content ?? '', true)"
          class="comment-content"
        ></p>
        <!-- 回复人 -->
        <div
          style="display: flex"
          v-for="reply of item.replyList?.rows ?? []"
          :key="reply.id"
        >
          <!-- 头像 -->
          <v-avatar size="36" class="comment-avatar">
            <img :src="reply.avatar!" />
          </v-avatar>
          <div class="reply-meta">
            <!-- 用户名 -->
            <div class="comment-user">
              <span>{{ reply.nickName }}</span>
              <!-- <span v-if="!reply.webSite">{{ reply.nickname }}</span>
              <a v-else :href="reply.webSite" target="_blank">
                {{ reply.nickname }}
              </a> -->
              <span class="blogger-tag" v-if="reply.isBlogger">博主</span>
            </div>
            <!-- 信息 -->
            <div class="comment-info">
              <!-- 发表时间 -->
              <span style="margin-right: 10px">
                {{ reply.createdTime }}
              </span>
              <!-- 点赞 -->
              <span
                :class="
                  (reply.isPraise ? 'like-active' : 'like') +
                  ' iconfont icondianzan'
                "
                @click="onPraise(reply)"
              />
              <span v-show="(reply.praiseTotal ?? 0) > 0">
                {{ reply.praiseTotal }}</span
              >
              <!-- 回复 -->
              <span
                class="reply-btn"
                @click="replyComment(index, reply, item.id)"
              >
                回复
              </span>
            </div>
            <!-- 回复内容 -->
            <p class="comment-content">
              <!-- 回复用户名 -->
              <template v-if="reply.replyAccountId === item.accountId">
                <span>{{ reply.relyNickName }}</span>
                <!-- <span v-if="!reply.replyWebSite" class="ml-1">
                  @{{ reply.replyNickname }}
                </span>
                <a
                  v-else
                  :href="reply.replyWebSite"
                  target="_blank"
                  class="comment-nickname ml-1"
                >
                  @{{ reply.replyNickname }}
                </a> -->
                ，
              </template>
              <span v-html="formatContent(reply.content ?? '', true)" />
            </p>
          </div>
        </div>
        <!-- 回复数量 -->
        <div
          class="mb-3"
          style="font-size: 0.75rem; color: #6d757a"
          v-show="(item.replyCount ?? 0) > 0 && item.replyList?.pageNo === 0"
          ref="check"
        >
          共
          <b>{{ item.replyCount }}</b>
          条回复，
          <span
            style="color: #00a1d6; cursor: pointer"
            @click="changeReplyCurrent(1, index, item.id!)"
          >
            点击查看
          </span>
        </div>
        <!-- 回复分页 -->
        <div
          class="mb-3"
          style="font-size: 0.75rem; color: #222"
          ref="paging"
          v-if="(item.replyList?.pages ?? 0) > 1"
        >
          <span style="padding-right: 10px">
            共{{ item.replyList?.pages }}页
          </span>
          <paging
            ref="page"
            :totalPage="item.replyList?.pages ?? 0"
            :index="index"
            :commentId="item.id!"
            @changeReplyCurrent="changeReplyCurrent"
            style="display: inline-block"
          />
        </div>
        <!-- 回复框 -->
        <Reply ref="reply" @submit="reloadReply" />
      </div>
    </div>
    <!-- 加载按钮 -->
    <div class="load-wrapper">
      <v-btn outlined v-if="state.pages > state.current" @click="onMore">
        加载更多...
      </v-btn>
    </div>
  </div>
  <!-- 没有评论提示 -->
  <div v-else style="padding: 1.25rem; text-align: center">来发评论吧~</div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted, watch } from "vue";
import img from "../assets/images/1.jpg";
import Emoji from "./Emoji.vue";
import Reply from "./Replay.vue";
import Paging from "./Paging.vue";
import EmojiList from "../assets/emoji";
import CommentApi from "@/api/CommentApi";
import type { CommentOutput, ReplyOutput } from "@/api/models";
import { useToast } from "@/stores/toast";
const props = defineProps<{
  type?: number | string;
}>();
const emit = defineEmits<{
  (e: "getCommentCount", count: number): void;
}>();
const toast = useToast();
const state = reactive({
  reFresh: true,
  commentContent: "",
  chooseEmoji: false,
  current: 1,
  commentList: [] as Array<CommentOutput>,
  count: 0,
  pages: 0,
});

const reply = ref<Array<InstanceType<typeof Reply>>>([]);
const loadData = async () => {
  const { data } = await CommentApi.list({
    pageNo: state.current,
    id: props.type as number,
  });
  if (state.current === 1) {
    state.commentList = data?.rows ?? [];
  } else {
    state.commentList.push(...(data?.rows ?? []));
  }
  state.count = data?.total ?? 0;
  state.pages = data?.pages ?? 0;
  emit("getCommentCount", state.count);
};
const onMore = async () => {
  state.current++;
  await loadData();
};

const changeReplyCurrent = async (
  current: number,
  index: number,
  commentId: number
) => {
  const item = state.commentList.find((item) => item.id == commentId);
  item!.replyList!.pageNo = current;
  const { data, succeeded } = await CommentApi.replyList({
    id: commentId,
    pageNo: current,
    pageSize: 5,
  });
  if (succeeded && data) {
    item!.replyList = data;
  }
};

const addEmoji = (key: string): void => {
  state.commentContent += key;
};

//提交评论
const insertComment = async () => {
  //删除html标签
  const content = formatContent(state.commentContent);
  if (content.length === 0) {
    toast.error("请输入评论内容");
    return;
  }
  const { succeeded } = await CommentApi.add({
    moduleId: props.type as number,
    content,
  });
  if (succeeded) {
    state.commentContent = "";
    state.current = 1;
    loadData();
  }
};

// 点赞或取消点赞
const onPraise = async (item: ReplyOutput) => {
  const { succeeded, data } = await CommentApi.praise(item.id!);
  if (succeeded) {
    item.isPraise = data;
    item.praiseTotal = data ? item.praiseTotal! + 1 : item.praiseTotal! - 1;
  }
};

const replyComment = (
  index: number,
  item: CommentOutput,
  rootId?: number
): void => {
  reply.value[index].replay.commentContent = "";
  reply.value[index].replay.nickname = item.nickName!;
  reply.value[index].replay.parentId = item.id!;
  reply.value[index].replay.rootId = rootId;
  reply.value[index].replay.replyAccountId = item.accountId;
  reply.value[index].replay.chooseEmoji = false;
  reply.value[index].replay.index = index;
  reply.value[index].replay.visible = true;
};

const reloadReply = async (index: number) => {
  const item = reply.value[index].replay;
  const content = formatContent(item.commentContent ?? "");
  if (content.length === 0) {
    toast.success("请输入评论内容");
    return;
  }

  const { succeeded } = await CommentApi.add({
    content: content ?? "",
    parentId: item.parentId,
    moduleId: props.type as number,
    rootId: item.rootId,
    replyAccountId: item.replyAccountId,
  });
  if (succeeded) {
    item.commentContent = "";
    reply.value[index].replay.visible = false;
    await changeReplyCurrent(1, index, item.rootId!);
  }
};
watch(
  () => props.type,
  async () => {
    await loadData();
  }
);
onMounted(async () => {
  await loadData();
});

// 格式化评论内容
const formatContent = (content: string, isHandleEmoji: boolean = false) => {
  content = content.replace(/<[^<>]*>/g, "");
  if (!isHandleEmoji) {
    return content;
  }
  const reg: RegExp = /\[.+?\]/g;
  return content.replace(reg, function (str: string) {
    return (
      "<img src= '" +
      EmojiList[str] +
      "' width='24'height='24' style='margin: 0 1px;vertical-align: text-bottom'/>"
    );
  });
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
