<template>
  <div class="x-link auth-item" style="padding: 20px 0">
    <div class="auth-avatar">
      <img src="../assets/608144857.jpg" />
    </div>
    <div class="auth-info">
      <h3 class="auth-info-name">{{ userName }}</h3>
      <div class="auth-info-statics">
        <span>{{ blogPostsCount }}篇文章</span>
        <a-divider type="vertical" />
        <span>{{ questionsCount }}个问答</span>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, toRefs } from "vue";

export default defineComponent({
  name: "Author",
  props: {
    userName: {
      type: String,
      default: "",
    },
    blogPostsCount: {
      type: String,
      default: "",
    },
    questionsCount: {
      type: String,
      default: "",
    },
    headPortrait: {
      type: String,
      default: "",
    },
  },
  setup(props: any) {
    toRefs(props);
  },
});
</script>

<style lang="scss" scoped>
.auth-item {
  display: -webkit-box;
  display: flex;
  -webkit-box-pack: justify;
  justify-content: space-between;
}

.auth-avatar {
  position: relative;
  overflow: hidden;
  width: 40px;
  height: 40px;
  background-color: #f7f9fb;
  border-radius: 50%;
}

.auth-avatar > img {
  width: 100%;
  position: absolute;
  left: 0;
  top: 50%;
  -webkit-transform: translateY(-50%);
  transform: translateY(-50%);
}

.auth-info {
  width: 200px;
  margin-left: 16px;
}

-auth-info-name {
    margin: 0;
    font-family: PingFangSC-Medium, PingFangSC, helvetica neue, hiragino sans gb, arial, microsoft yahei ui, microsoft yahei, simsun, sans-serif;
    font-size: 16px;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
}

.auth-info-statics {
    margin-top: 4px;
    font-size: 12px;
    color: #575e6e;
}
</style>