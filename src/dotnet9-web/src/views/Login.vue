<template>
  <div class="Login">
    <div style="margin: auto; margin-top: 12%">
      <h1 style="text-align: center">
        <span style="color: rbg(24, 173, 145)">社区Logo</span>
      </h1>
    </div>

    <a-card title="登录" style="width: 431px; margin: auto">
      <template #extra>
        <a href="#">注册账号</a>
      </template>

      <a-form
        name="custom-validation"
        ref="formRef"
        :model="formState"
        :rules="rules"
        v-bind="layout"
        @finish="handleFinish"
      >
        <a-form-item label="账号" name="name"> 
            <a-input v-model:value="formState.name" />
        </a-form-item>

        <a-form-item has-feedback label="密码" name="pass">
            <a-input
                v-model:value="formState.password"
                type="password"
                autocomplete="off"
            />
        </a-form-item>

        <a-form-item :wrapper-col="{ span: 14, offset: 4 }">
            <a-button type="primary" htmp-type="submit">登录</a-button>
        </a-form-item>
      </a-form>
    </a-card>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, ref } from "vue";
import { Modal, message } from "ant-design-vue";
import router from "@/router";
import store from "@/store";
import request from "@/api/http";

export default defineComponent({
  name: "Login",
  components: {},
  setup() {
    const formRef = ref();
    const formState = reactive({
      name: "",
      password: "",
    });

    let checkName = async (rule: any, value: number) => {
      if (!value) {
        return Promise.reject("账号不能为空");
      }
    };

    let checkPassword = async (rule: any, value: string) => {
        if(value===""){
            return Promise.reject("请输入密码");
        } else {
            return Promise.resolve();
        }
    };

    const rules = {
        name: [
            {
                required: true,
                validator: checkName,
                trigger: "change",
            },
        ],
        password: [
            {
                required: true,
                validator: checkPassword,
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
            url: "/Auth/Login",
            params: {
                loginName: values.name,
                loginPassword: values.password,
            },
        }).then((res: any) => {
            if(!res.data.success){
                Modal.error({
                    title: "提示",
                    content: res.data.msg,
                });
            } else {
                store.commit("saveToken", res.data.response);
                getMyUserInfo();
            }
        });
    };

    function getMyUserInfo() {
        request({
            url: '/UserInfo/Get',
        }).then((res: any) => {
            store.commit("saveUserInfo", JSON.stringify(res.data.response));
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
    }
  },
});
</script>
