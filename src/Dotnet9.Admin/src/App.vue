
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

.d-login {
  width: 100%;
  height: 100%;
  background-color: #2259d8;
  box-sizing: border-box;
  // background: url("./assets/login_bg.svg");
  position: relative;
  .d-login-box {
    top: 200px;
    width: 500px;
    padding: 2rem;
    border-radius: 5px;
    background-color: #2a50cd;
    color: #fff;
    box-shadow: 1px 3px 8px rgba(0, 0, 0, 0.1);
    position: absolute;
    right: 100px;
    .d-login-header {
      padding-bottom: 2rem;
      h1 {
        font-size: 40px;
        text-align: center;
        color: #fff;
      }
    }
  }

  .el-form-item__label {
    color: #fff;
  }
}
input::-webkit-input-placeholder {
  color: #fff !important;
}

.w_100 {
  width: 100%;
}
// .m_t_1{
//   margin-top: 10px;
// }


:deep() {
  .el-input__wrapper {
    background: transparent;
    box-shadow: none;
  }
  .el-input {
    border-bottom: 1px solid #496cd8;
    height: 50px;
    line-height: 50px;
    padding-left: 30px;
  }
  .el-input__inner {
    font-size: 20px;
    color: #fff;
  }
  .el-button {
    height: 60px;
    line-height: 60px;
    font-size: 20px;
    margin-top: 20px;
  }

  .el-form-item__content {
    position: relative;
    > span {
      position: absolute;
      top: 12px;
      left: 11px;
      img {
        width: 20px;
        height: 20px;
      }
    }
  }
}


*{
  font-family:Arial, Helvetica, sans-serif;
}
</style>
