<template>
  <div class="d-login">
    <div class="d-Mantle">
      <div class="d-login-box" v-loading="loading">
        <div class="d-login-header">
          <div class="Login_Logo">
            <img src="./assets/favicon.ico" alt="" srcset="" />
          </div>
          <h1>Dotnet9博客系统后台</h1>
        </div>
        <el-form
          :model="loginForm"
          label-width="70px"
          :rules="rules"
          ref="form"
          label-position="top"
          @keydown.enter="loginHandler()"
        >
          <el-form-item prop="account">
            <span>
              <img src="./assets/account.png" alt="" srcset="" />
            </span>
            <el-input
              placeholder="输入用户名"
              v-model="loginForm.account"
            ></el-input>
          </el-form-item>
          <el-form-item prop="password">
            <span>
              <img src="./assets/pwd.png" alt="" srcset="" />
            </span>
            <el-input
              placeholder="输入密码"
              v-model="loginForm.password"
              show-password
            ></el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="loginHandler()" class="w_100"
              >登录</el-button
            >
          </el-form-item>
        </el-form>
      </div>

      <div class="Login_Tips">Copyright ©2019-2022 Dotnet9博客系统后台</div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { reactive, ref } from "@vue/reactivity";
import { ElForm } from "element-plus";
import { useRouter } from "vue-router";
import { ElMessage } from "element-plus";
import { onMounted } from "vue";
import { get, post } from "shared/http/HttpClient";

const loginForm = reactive({
  account: "",
  password: "",
});
const loading = ref<boolean>(false);
const rules = {
  account: [
    {
      required: true,
      message: "账号不能为空",
      trigger: "blur",
    },
  ],
  password: [
    {
      required: true,
      message: "密码不能为空",
      trigger: "blur",
    },
  ],
};
const form = ref<InstanceType<typeof ElForm>>();
const router = useRouter();
const loginHandler = async () => {
  loading.value = true;
  await form.value?.validate(async (valid) => {
    if (valid) {
      try {
        await post("/api/account/login", {
          account: loginForm.account,
          password: loginForm.password,
        });
        setTimeout(() => {
          loading.value = false;
          router.replace("/admin/dash");
          ElMessage({
            message: "登录成功！",
            showClose: false,
            type: "success",
          });
        }, 100);
      } finally {
        loading.value = false;
      }
    } else {
      loading.value = false;
    }
  });
};

const checkLogin = async () => {
  loading.value = true;
  try {
    let { isLogin, isInit } = await get<{ isLogin: boolean; isInit: boolean }>(
      "/api/account/checkLogin",
      {}
    );
    console.log("islogin ", isLogin);
    if (isLogin) {
      router.replace("/admin/dash");
    }
    if (!isInit) {
      router.replace("/initAccount");
    }
  } finally {
    loading.value = false;
  }
};
onMounted(() => {
  console.log("Mounted");
  checkLogin();
});
</script>
<style lang="scss" scoped>
// #496CD8
.d-login {
  background-image: url("./assets/loginBackground2.png");
  background-repeat: no-repeat;
  background-position: 340px 184px;
  background-size: 26%;
  .d-Mantle {
    background: url("./assets/background.jpg") no-repeat;
    width: 100%;
    height: 100%;
    background-size: 100% 100%;
    overflow: hidden;
  }
}

@keyframes rotation {
  from {
    transform: rotate(0deg);
  }

  to {
    transform: rotate(360deg);
  }
}
.Login_Logo {
  font-size: 22px;
  font-weight: bold;
  top: 22px;
  color: #fff;
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
  position: absolute;
  bottom: 22px;
  color: #b9cdff;
  text-align: center;
  font-size: 18px;
  width: 100%;
  letter-spacing: 1.5px;
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
