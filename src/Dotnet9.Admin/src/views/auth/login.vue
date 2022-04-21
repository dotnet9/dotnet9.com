<template>
  <div class="d-login">
    <div class="d-login-box" v-loading="loading">
      <div class="d-login-header">
        <h1>Dotnet9博客系统后台</h1>
      </div>
      <el-form
        :model="loginForm"
        label-width="70px"
        :rules="rules"
        ref="form"
        label-position="top"
        @keydown.native.enter="loginHandler()"
      >
        <el-form-item label="账号" prop="userName">
          <el-input placeholder="输入用户名" v-model="loginForm.userName"></el-input>
        </el-form-item>
        <el-form-item label="密码" prop="pass">
          <el-input placeholder="输入密码" v-model="loginForm.pass" show-password></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="loginHandler()" class="w_100">登录</el-button>
        </el-form-item>
        <el-form-item>
          <el-button type="default" class="w_100">重置密码</el-button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { reactive, ref } from "@vue/reactivity";
import { ElForm } from "element-plus"
import { useRouter } from "vue-router";
import { ElMessage } from 'element-plus'
import { onMounted } from 'vue';
import { get, post } from "shared/http/HttpClient";

const loginForm = reactive({
  userName: "",
  pass: "",
});
const loading = ref<boolean>(false);
const rules = {
  userName: [
    {
      required: true,
      message: "用户名不能为空",
      trigger: 'blur'
    },
  ],
  pass: [
    {
      required: true,
      message: "密码不能为空",
      trigger: 'blur'
    },
  ],
};
const form = ref<InstanceType<typeof ElForm>>();
const router = useRouter();
const loginHandler = async () => {
  loading.value = true
  await form.value?.validate(async (valid) => {
    if (valid) {
      try {
        await post("/admin/account/login", { userName: loginForm.userName, password: loginForm.pass })
        setTimeout(() => {
          loading.value = false
          router.replace('/admin/dash')
          ElMessage({ message: '登录成功！', showClose: false, type: 'success' })
        }, 100);
      } finally {
        loading.value = false
      }
    } else {
      loading.value = false
    }
  });
}

const checkLogin = async () => {
  loading.value = true
  try {
    let { isLogin, isInit } = await get<{ isLogin: boolean, isInit: boolean }>('/admin/account/checkLogin', {})
    console.log('islogin ', isLogin)
    if (isLogin) {
      router.replace('/admin/dash')
    }
    if (!isInit) {
      router.replace('/initAccount')
    }
  } finally {
    loading.value = false
  }
}
onMounted(() => {
  console.log('Mounted')
  checkLogin();
})
</script>

<style lang="less" scoped>
</style>
