<template>
  <div class="d-login">
    <div class="d-Mantle">
      <div class="d-login-box">
        <div class="d-login-header">
          <div class="Login_Logo">
            <img src="./assets/favicon.ico" alt="" srcset="" />
          </div>
          <h1>
            {{ CurrentSUB == LoginSUB ? "Dotnet9博客系统后台" : "创建管理员" }}
          </h1>
        </div>
        <component :is="CurrentSUB" @ChangeSub="ChangeSub"> </component>
        <div class="Login_Tips">Copyright ©2019-2022 Dotnet9博客系统后台</div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { reactive, ref, shallowRef } from "@vue/reactivity";
import { useRouter } from "vue-router";
import { onMounted, defineEmits } from "vue";
import { get, post } from "shared/http/HttpClient";
import LoginSUB from "./Login/index.vue";
import RegisterSUB from "./Register/index.vue";

let CurrentSUB: any = shallowRef(LoginSUB);
const router = useRouter();
const checkLogin = async () => {
  try {
    let { isLogin, isInit } = await get<{ isLogin: boolean; isInit: boolean }>(
      "/api/account/checkLogin",
      {}
    );
    if (isLogin) {
      router.replace("/admin/dash");
    }
    if (!isInit) {
      CurrentSUB.value = RegisterSUB;
    }
  } finally {
    console.error("Login question");
  }
};

const ChangeSub = (data: any) => {
  debugger;
  CurrentSUB.value = data;
};
onMounted(() => {
  checkLogin();
});
</script>
<style lang="scss" scoped>
.d-login {
  background-image: url("./assets/loginBackground2.png");
  background-repeat: no-repeat;
  background-position: 340px 184px;
  background-size: 26%;
  height: 100%;
  .d-login-box {
    display: block;
    width: 450px;
    padding: 2rem;
    border-radius: 15px;
    background-color: #184376;
    color: #fff;
    box-shadow: 1px 3px 8px rgb(0 0 0 / 10%);
    margin-right: 200px;
    margin: 0 auto;
    margin-right: 200px;
    margin-top: 200px;
    position: relative;
    .d-login-header {
      padding-bottom: 2rem;
      margin-top: 35px;
      h1 {
        font-size: 25px;
        margin-top: 10px;
        text-align: center;
        color: #fff;
      }
    }

    .el-form-item {
      height: 70px;
    }
  }

  .el-form-item__label {
    color: #fff;
  }
  .d-Mantle {
    background: url("./assets/background.jpg") no-repeat;
    width: 100%;
    height: 100%;
    background-size: 100% 100%;
    overflow: hidden;
  }
}

.Login_Logo {
  font-size: 22px;
  font-weight: bold;
  top: 22px;
  color: #fff;
  text-align: center;
  text-align: center;
  img {
    width: 60px;
    vertical-align: middle;
    background: #e9edf7;
    padding: 10px;
    border-radius: 50%;
    position: relative;
    left: -5px;
  }
}

.Login_Tips {
  position: fixed;
  bottom: 12px;
  color: #b9cdff;
  text-align: center;
  font-size: 18px;
  width: 100%;
  left: 0;
  letter-spacing: 1.5px;
  height: 40px;
  line-height: 40px;
}

:deep() {
  .el-input__wrapper {
    background: transparent;
    box-shadow: none;
  }
  .el-input {
    // border-bottom: 1px solid #496cd8;
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
    width: 100%;
    background: #143560;
    border: 1px solid #29548d;
  }
  .el-form-item__error {
    top: 111%;
    left: 28px;
  }
  .el-form-item__content {
    position: relative;
    > span {
      position: absolute;
      top: 12px;
      left: 0px;
      img {
        width: 25px;
        height: 26px;
      }
    }
  }

  .el-form-item.is-error .el-input__wrapper {
    box-shadow: none;
  }
}
// :deep() {

// }
</style>
