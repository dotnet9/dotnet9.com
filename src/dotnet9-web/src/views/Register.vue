<template>
  <div class="Login">
    <div style="margin: auto; margin-top: 8%">
      <h1 style="text-align: center">
        <span style="color: rgb(24, 173, 145)"> 社区Logo</span>
      </h1>
    </div>

    <a-card title="注册" style="width: 431px; margin: auto">
      <template #extra><a href="#">登录 ></a></template>

      <a-form
        name="custom-validation"
        ref="formRef"
        :model="formState"
        :rules="rules"
        v-bind="layout"
        @finish="handleFinish"
      >
        <a-form-item label="用户名" name="userName">
          <a-input v-model:value="formState.userName" />
        </a-form-item>

        <a-form-item label="账号" name="loginName">
          <a-input v-model:value="formState.loginName" />
        </a-form-item>

        <a-form-item has-feedback label="密码" name="loginPassWord">
          <a-input
            v-model:value="formState.loginPassWord"
            type="password"
            autocomplete="off"
          />
        </a-form-item>

        <a-form-item label="手机号" name="phone">
          <a-input v-model:value="formState.phone" />
        </a-form-item>

        <a-form-item label="邮箱" name="email">
          <a-input v-model:value="formState.email" />
        </a-form-item>

        <a-form-item label="个人介绍" name="introduction">
          <a-textarea
            v-model:value="formState.introduction"
            placeholder="个人介绍"
            :auto-size="{ minRows: 2, maxRows: 5 }"
          />
        </a-form-item>

        <a-form-item :wrapper-col="{ span: 14, offset: 4 }">
          <a-button type="primary" html-type="submit">注册</a-button>
        </a-form-item>
      </a-form>
    </a-card>
  </div>
</template>

<script lang="ts">
import { Modal, message } from "ant-design-vue";
import { defineComponent, reactive, ref } from "vue";
import request from "@/api/http";
import router from "@/router";
import store from "@/store";
export default defineComponent({
  name: "Login",
  components: {},
  setup() {
    const formRef = ref();
    const formState = reactive({
      userName: "",
      loginName: "",
      loginPassWord: "",
      phone: "",
      introduction: "",
      email: "",
      headPortrait: "",
    });

    let checkName = async (rule: any, value: number) => {
      if (!value) {
        return Promise.reject("用户名不能为空");
      }
    };

    let checkLoginName = async (rule: any, value: string) => {
      if (value === "") {
        return Promise.reject("账号不能为空");
      } else {
        return Promise.resolve();
      }
    };

    let checkPass = async (rule: any, value: string) => {
      if (value === "") {
        return Promise.reject("请输入密码");
      } else {
        return Promise.resolve();
      }
    };

    const rules = {
      userName: [
        {
          required: true,
          validator: checkName,
          trigger: "change",
        },
      ],
      loginName: [
        {
          required: true,
          validator: checkLoginName,
          trigger: "change",
        },
      ],
      loginPassWord: [
        {
          required: true,
          validator: checkPass,
          trigger: "change",
        },
      ],
    };
    const layout = {
      labelCol: {
        span: 4,
      },
      wrapperCol: {
        span: 14,
      },
    };

    const handleFinish = (values: any) => {
      request({
        url: "/Auth/Register",
        method: "POST",
        data: {
          userName: values.userName,
          loginName: values.loginName,
          loginPassWord: values.loginPassWord,
          phone: values.phone,
          introduction: values.introduction,
          email: values.email,
          headPortrait: values.headPortrait,
        },
      }).then((res: any) => {
        if (!res.data.success) {
          Modal.error({
            title: "提示",
            content: res.data.msg,
          });
        } else {
          store.commit("saveToken", res.data.response); //保存 token
          getMyUserInfo();
        }
      });
    };

    function getMyUserInfo() {
      request({
        url: "/UserInfo/Get",
      }).then((res: any) => {
        store.commit("saveUserInfo", JSON.stringify(res.data.response)); //保存 token
        message.success("登录成功");
        router.replace("/");
      });
    }

    return {
      formState,
      formRef,
      rules,
      layout,
      handleFinish,
    };
  },
});
</script>
