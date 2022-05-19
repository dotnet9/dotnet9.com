<template>
  <el-form
    v-loading="loading"
    :model="formData"
    label-width="70px"
    :rules="rules"
    ref="form"
    label-position="top"
    @keydown.enter="submitHandler()"
  >
    <el-form-item prop="account">
      <span>
        <img src="../assets/account.png" alt="" srcset="" />
      </span>
      <el-input placeholder="输入账号" v-model="formData.account"></el-input>
    </el-form-item>
    <el-form-item prop="email">
      <span>
        <img src="../assets/Email.png" alt="" srcset="" />
      </span>
      <el-input placeholder="输入邮箱" v-model="formData.email"></el-input>
    </el-form-item>
    <el-form-item prop="password">
      <span>
        <img src="../assets/pwd.png" alt="" srcset="" />
      </span>
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
</template>

<script lang="ts" setup>
import { reactive, ref, onMounted, defineEmits } from "vue";
import { useRouter } from "vue-router";
import { ElForm, ElMessage } from "element-plus";
import { post, get } from "shared/http/HttpClient";
import LoginSUB from "../Login/index.vue";

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
      trigger: "blur",
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
      trigger: "blur",
    },
  ],
};
const form = ref<InstanceType<typeof ElForm>>();

const router = useRouter();
const emit = defineEmits(["ChangeSub"]);

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

          emit("ChangeSub", LoginSUB);
        })
        .finally(() => (loading.value = false));
    }
  });
};

onMounted(() => {});
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
</style>


