<template>
    <el-form
      :model="loginForm"
      label-width="70px"
      :rules="rules"
      ref="form"
      label-position="top"
      @keydown.enter="loginHandler()"
      v-loading="loading"
    >
      <el-form-item prop="account">
        <span>
          <img src="../assets/account.png" alt="" srcset="" />
        </span>
        <el-input
          placeholder="输入用户名"
          v-model="loginForm.account"
        ></el-input>
      </el-form-item>
      <el-form-item prop="password">
        <span>
          <img src="../assets/pwd.png" alt="" srcset="" />
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

</script>
<style lang="scss" scoped>
</style>
