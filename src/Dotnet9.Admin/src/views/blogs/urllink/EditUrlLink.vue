<template>
  <el-dialog
    @opened="open()"
    v-model="showDialog"
    @closed="close()"
    :title="formModel.id == 0 ? '添加链接' : '编辑链接'"
  >
    <el-form
      :model="formModel"
      :rules="formRule"
      ref="instance"
      v-loading="loading"
      label-width="100px"
      @keyup.enter="save()"
    >
      <el-form-item label="名称" prop="name">
        <el-input v-model="formModel.name"></el-input>
      </el-form-item>
      <el-form-item label="地址" prop="url">
        <el-input v-model="formModel.url"></el-input>
      </el-form-item>
      <el-form-item label="描述" prop="description">
        <el-input v-model="formModel.description"></el-input>
      </el-form-item>
      <el-form-item label="顺序" prop="index">
        <el-input-number v-model="formModel.index"></el-input-number>
      </el-form-item>
      <el-form-item label="类型" prop="kind">
        <el-select v-model.number="formModel.kind">
          <el-option :value="0" label="私密"></el-option>
          <el-option :value="1" label="网站相关"></el-option>
          <el-option :value="2" label="友情链接"></el-option>
          <el-option :value="3" label="课程链接"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="save()">保存</el-button>
        <el-button type="default" @click="clearForm()">清空</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script setup lang="ts">
import { ref, watch } from "vue";
import { useForm } from "shared/useForm";
import { ElForm, ElMessage } from "element-plus";
import { get, post } from "shared/http/HttpClient";

const props = withDefaults(defineProps<{ modelValue: boolean; id: number }>(), {
  modelValue: false,
  id: 0,
});

const emit = defineEmits(["update:modelValue", "success"]);

const showDialog = ref(false);

const formModel = ref({
  name: "",
  url: "",
  description: "",
  index: 0,
  kind: 0,
  id: 0,
  createDate: "",
});

const formRule = {
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
  url: [
    {
      required: true,
      message: "链接不能为空",
      trigger: "blur",
    },
    {
      min: 3,
      max: 128,
      message: "链接的长度在 3 ~ 128个字符之间",
      trigger: "blur",
    },
  ],
};

watch(
  () => props.modelValue,
  () => {
    showDialog.value = props.modelValue;
    if (props.id > 0) {
      loadItem();
    } else {
      clearForm();
    }
  }
);

const loadItem = () => {
  loading.value = true;
  get("/api/urllink/get", { id: props.id })
    .then((res: any) => {
      formModel.value = {
        id: res.id,
        name: res.name,
        url: res.url,
        description: res.description,
        index: res.index,
        kind: res.kind,
        createDate: res.createDate,
      };
    })
    .finally(() => (loading.value = false));
};

const { instance, loading, clearForm } = useForm();

const open = () => {};

const close = () => {
  emit("update:modelValue", false);
};

const save = () => {
  instance.value.validate((isvalid) => {
    if (isvalid) {
      loading.value = true;
      post("/api/urllink/addOrUpdate", formModel.value)
        .then(() => {
          close();
          emit("success");
        })
        .finally(() => (loading.value = false));
    }
  });
};
</script>
