
<template>
  <router-view></router-view>
</template>
<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({});
</script>
<style lang="scss">
body {
  background: #f1f1f1;
}

p {
  padding: 0;
  margin: 0;
}

html,
body,
#app {
  padding: 0;
  margin: 0;
  width: 100%;
  height: 100%;
  box-sizing: border-box;
}

#app {
  font-family: "Helvetica Neue", Helvetica, "PingFang SC", "Hiragino Sans GB",
    "Microsoft YaHei", "微软雅黑", Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

.tool-bar {
  margin-bottom: 1rem;
  margin-top: 1rem;
  background: #f7f8fa;
  padding: 1rem;
  box-sizing: border-box;
}
.pointer {
  cursor: pointer;
}
a {
  text-decoration: none;
}

.el-card__header {
  h1 {
    color: var(--el-text-color-regular);
  }
}

h1,
h2,
h3 {
  padding: 0;
  margin: 0;
}

@keyframes test1 {
  0% {
    opacity: 0.1;
    /* transform: translateY(50px); */
  }
  100% {
    /* transform: translateY(0); */
    opacity: 1;
  }
}

/* .anim1 {
  animation: test1 0.3s ease-in-out;
} */



input::-webkit-input-placeholder {
  color: #ccc !important;
}

.w_100 {
  width: 100%;
}
// .m_t_1{
//   margin-top: 10px;
// }

.el-input {
  border-bottom: 1px solid #fff;
  // border-radius: 25px;
  height: 55px;
  line-height: 55px;
  padding-left: 50px;
  padding-bottom: 2px;
  margin-right: 20px;
}
.el-input__wrapper {
  background: transparent;
  box-shadow: none;
}
.el-input__inner {
  font-size: 30px;
  color: #fff;
}

.el-button {
  height: 80px;
  width: 80%;
  line-height: 80px;
  font-size: 20px;
  margin-top: 20px;
}

* {
  font-family: Arial, Helvetica, sans-serif;
}
</style>
