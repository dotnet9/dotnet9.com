<template>
  <div class="BlogPostCreate">
    <a-row :gutter="20" style="margin-top: 24px">
      <a-col :offset="3" :span="18">
        <a-form
          name="custom-validation"
          ref="formRef"
          :model="formState"
          :rules="rules"
          v-bind="layout"
          @finish="handleFinish"
        >
          <a-card title="写文章">
            <template #extra>
              <a-form-item>
                <a-button
                  html-type="submit"
                  style="
                    background-color: rgb(24, 173, 145);
                    border-color: rgb(24, 173, 145);
                    color: aliceblue;
                  "
                  >发布文章</a-button
                >
              </a-form-item>
            </template>

            <a-form-item label="标题" name="title">
              <a-input v-model:value="formState.title" />
            </a-form-item>

            <a-form-item has-feedback label="标签" name="tag">
              <a-select
                v-model:value="formState.tag"
                mode="tags"
                style="width: 100%"
              >
              </a-select>
            </a-form-item>

            <a-form-item has-feedback label="内容" name="content">
              <a-textarea v-model:value="formState.content" :rows="30" />
            </a-form-item>
          </a-card>
        </a-form>
      </a-col>
    </a-row>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, reactive, toRefs } from "vue";
import { Modal, message } from "ant-design-vue";
import request from "@/api/http";
import router from "@/router";
import store from "@/store";

export default defineComponent({
  name: "BlogPostCreate",
  setup() {
    const formRef = ref();
    const formState = reactive({
      title: "",
      content: "",
      tag: "",
    });

    let checkName = async (rule: any, value: number) => {
      if (!value) {
        return Promise.reject("账号不能为空");
      }
    };

    let checkPassword = async (rule: any, value: string) => {
      if (value === "") {
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
      pass: [
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
        url: "/BlogPost/Create",
        method: "post",
        data: {
          title: values.title,
          cover: "",
          content: values.content,
          tag: values.tag.toString(),
        },
      }).then((res: any) => {
        if (res.data.success) {
          Modal.success({
            title: "提示",
            content: "文章创建成功",
          });

          router.replace("/");
        } else {
          message.error(res.data.msg);
        }

        console.log("创建文章", res);
      });
    };

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
