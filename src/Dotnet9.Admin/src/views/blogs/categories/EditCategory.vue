<template>
  <el-card class="anim1">
    <template #header>
      <h1>{{ formModel.id ? "编辑" : "添加" }}</h1>
    </template>
    <div v-loading="loading">
      <el-form
        label-width="80px"
        :rules="formRule"
        :model="formModel"
        ref="form"
      >
        <el-form-item label="名称" prop="name">
          <el-input placeholder="输入名称" v-model="formModel.name"></el-input>
        </el-form-item>
        <el-form-item label="别名" prop="slug">
          <el-input placeholder="输入别名" v-model="formModel.slug"></el-input>
        </el-form-item>
        <el-form-item label="封面" prop="cover">
          <el-input placeholder="输入封面" v-model="formModel.cover"></el-input>
        </el-form-item>
        <el-form-item label="描述" prop="description">
          <el-input
            placeholder="描述"
            type="textarea"
            v-model="formModel.description"
          ></el-input>
        </el-form-item>
        <el-form-item label="是否显示">
          <el-switch v-model="formModel.isShow"></el-switch>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="save()">{{
            formModel.id ? "保存修改" : "添加"
          }}</el-button>
          <el-button @click="restore()">重置</el-button>
        </el-form-item>
      </el-form>
    </div>
  </el-card>
</template>

<script lang="ts" setup>
import { reactive } from "vue";
import { defineComponent, onMounted, PropType, ref, toRefs, watch } from "vue";
import { http } from "shared/http/HttpClient";
import { ElForm, ElMessage } from "element-plus";
import { CategoryItem } from "./CategoryModel";
import { useRoute, useRouter } from "vue-router";

const loading = ref(false);

const defaultValue = {
  name: "",
  slug: "",
  cover: "",
  description: "",
  parentId: 0,
  index: 0,
  isShow: true,
};

const formModel = ref<CategoryItem>(defaultValue);
const form = ref<InstanceType<typeof ElForm>>();

const formRule = reactive({
  name: [
    {
      required: true,
      message: "名称不能为空",
      trigger: "blur",
    },
    {
      min: 3,
      max: 32,
      message: "名称的长度在 3 ~ 32个字符之间",
      trigger: "blur",
    },
  ],
  slug: [
    {
      required: true,
      message: "别名不能为空",
      trigger: "blur",
    },
    {
      min: 3,
      max: 256,
      message: "别名的长度在 3 ~ 256个字符之间",
      trigger: "blur",
    },
  ],
  cover: [
    {
      required: true,
      message: "封面不能为空",
      trigger: "blur",
    },
    {
      min: 3,
      max: 128,
      message: "封面的长度在 3 ~ 128个字符之间",
      trigger: "blur",
    },
  ],
  description: [
    {
      min: 0,
      max: 128,
      message: "描述的长度在 0 ~ 128个字符之间",
      trigger: "blur",
    },
  ],
});

const router = useRouter();

const route = useRoute();

watch(
  () => route.params,
  (params) => {
    console.log("监听:", params);
  }
);

if (route.params.obj) {
  formModel.value = JSON.parse(route.params.obj.toString()) as CategoryItem;
}

onMounted(() => {});

const restore = () => {
  router.replace("/admin/editcate");
  formModel.value = {
    name: "",
    slug: "",
    cover: "",
    description: "",
    parentId: 0,
    index: 0,
    isShow: true,
  };
};

const save = async () => {
  try {
    await form.value?.validate();
    loading.value = true;
    await http.post("/api/category/addOrUpdate", formModel.value);
    ElMessage({
      message: "保存成功",
      type: "success",
    });
    form.value?.resetFields();
    form.value?.clearValidate();
    router.replace("/admin/categories");
  } finally {
    loading.value = false;
  }
};
</script>

<style lang="less">
</style>