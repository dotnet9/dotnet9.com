<template>
  <div class="d-login">
    <div class="d-login-box" v-loading="loading">
      <div class="d-login-header">
        <div class="Login_Logo">
          <img src="./assets/favicon.ico" alt="" srcset="" />
        </div>
        <h1>创建管理员</h1>
      </div>
      <el-form
        :model="formData"
        label-width="70px"
        :rules="rules"
        ref="form"
        label-position="top"
        @keydown.enter="submitHandler()"
      >
        <el-form-item label="账号" prop="account">
          <el-input
            placeholder="输入账号"
            v-model="formData.account"
          ></el-input>
        </el-form-item>
        <el-form-item label="邮箱" prop="email">
          <el-input placeholder="输入邮箱" v-model="formData.email"></el-input>
        </el-form-item>
        <el-form-item label="密码" prop="password">
          <el-input
            placeholder="输入密码"
            v-model="formData.password"
            show-password
          ></el-input>
        </el-form-item>
        <el-form-item label>
          <el-button type="primary" class="w_100" @click="submitHandler()"
            >创建</el-button
          >
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { reactive, ref, onMounted } from "vue";
import { useRouter } from "vue-router";
import { ElForm, ElMessage } from "element-plus";
import { post, get } from "shared/http/HttpClient";

const formData = reactive({
  account: "",
  email: "",
  password: "",
});
const loading = ref<boolean>(false);
var checkEmail = (rule, value, callback) => {
  const regEmail = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
  if (regEmail.test(value)) {
    return callback();
  }
  callback(new Error("请输入合法的邮箱"));
};
const rules = {
  account: [
    {
      required: true,
      message: "账号不能为空",
      trigger: "blur",
    },
    {
      min: 3,
      max: 10,
      message: "账号的长度在 3 ~ 10个字符之间",
      trigger: "blue",
    },
  ],
  email: [
    {
      required: true,
      message: "邮箱不能为空",
      trigger: "blur",
    },
    {
      validator: checkEmail,
      trigger: "blur",
    },
  ],
  password: [
    {
      required: true,
      message: "密码不能为空",
      trigger: "blur",
    },
    {
      min: 6,
      max: 15,
      message: "密码的长度在 6 ~ 15个字符之间",
      trigger: "blue",
    },
  ],
};
const form = ref<InstanceType<typeof ElForm>>();

const router = useRouter();

const submitHandler = () => {
  form.value.validate((isvalid) => {
    if (isvalid) {
      loading.value = true;
      post("/api/account/createAdminAccount", formData)
        .then((_) => {
          ElMessage({
            message: "初始化管理员账号成功",
            type: "success",
          });
          router.replace("/login");
        })
        .finally(() => (loading.value = false));
    }
  });
};

onMounted(() => {
  get("/api/account/checkLogin", {});
});
</script>


<style lang="scss" scoped>
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
  }
}
.el-button {
  height: 50px;
  line-height: 50px;
  font-size: 20px;
  text-align: center;
  margin-top: 40px;
}
:deep() {
  .el-form-item__content {
    position: relative;
    > span {
      position: absolute;
      top: 12px;
      left: 11px;
      img {
        width: 25px;
        height: 26px;
      }
    }
  }
}
</style>


