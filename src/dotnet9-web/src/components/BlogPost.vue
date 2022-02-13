<template>
  <div class="columns-item columns-item-img">
    <div class="columns-config">
      <h3 class="columns-config-title">
        <a style="display: block; word-break: break-all">{{ title }}</a>
      </h3>

      <p class="columns-config-desc">
        <a style="display: block; word-break: break-all">{{ content }}</a>
      </p>

      <div class="columns-config-footer">
        <span style="margin-right: 26px">{{ userName }}</span>
        <span class="columns-auth-time" title="2022-02-12 17:47:89">{{
          createTime
        }}</span>
      </div>
    </div>

    <a class="columns-img" style="position: relative; overflow: hidden">
      <img
        :src="cover"
        style="
          position: absolute;
          top: 50%;
          left: 0px;
          width: 100%;
          transform: translateY(-50%);
        "
      />
    </a>
  </div>
</template>

<script lang="ts">
import { defineComponent, toRefs } from "vue";

export default defineComponent({
  name: "BlogPost",
  props: {
    content: {
      type: String,
      default: "",
    },
    title: {
      type: String,
      default: "",
    },
    cover: {
      type: String,
      default: "",
    },
    userName: {
      type: String,
      default: "",
    },
    createTime: {
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
.columns-item {
  display: flex;
  padding: 24px 0;
}

.columns-item-img .columns-config {
  width: calc(100% - 234px);
  flex-shrink: 3;
}

.columns-config {
  width: 100%;
  position: relative;
  padding-bottom: 46px;
}

.columns-config-title a{
  margin: 6px 0;
  font-size: 20px;
  word-break: break-all;
  word-wrap: break-word;
  color: #00223b;
}

.columns-config-desc a{
  margin: 8px 0 0;
  word-break: break-all;
  word-wrap: break-word;
  color: #575e6e;
  font-size: 14px;
}

.columns-config-footer {
  display: -webkit-box;
  display: flex;
  position: absolute;
  bottom: 0;
  left: 0;
  width: 100%;
  color: #575e6e;
}

.columns-auth-time {
  display: inline-block;
  white-space: nowrap;
  line-height: 24px;
  margin-right: 26px;
}

.columns-config-tags {
  line-height: 24px;
  flex-shrink: 0;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.columns-img {
  width: 210px;
  height: 140px;
  margin-left: 24px;
  overflow: hidden;
  flex-shrink: 0;
}
</style>