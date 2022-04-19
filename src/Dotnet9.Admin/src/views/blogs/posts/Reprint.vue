<template>
  <el-button type="primary" @click="showDialog()">转载博客园</el-button>
  <el-dialog v-model="show" :show-close="false">
    <el-form label-width="100px" v-loading="loading" ref="instance" :model="formModel" :rules="rules">
      <el-form-item label="地址" prop="url">
        <el-input v-model="formModel.url" placeholder="输入博客园文章地址"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="reprint()">转载</el-button>
      </el-form-item>
    </el-form>
  </el-dialog>
</template>

<script setup lang="ts">
import {ref} from "vue";
import {get} from "shared/http/HttpClient";
import {useForm} from "shared/useForm";

const show = ref(false)

const formModel = ref({
  url: ''
})

const rules = {
  url: {
    required: true, message: '地址不能为空'
  }
}

const showDialog = () => {
  show.value = true;
}

const emit = defineEmits(['complate'])

const {loading, instance} = useForm()

const reprint = () => {
  instance.value?.validate()?.then(valid => {
    if (valid) {
      loading.value = true
      get("/admin/spider/GetCnBlog", {url: formModel.value.url}).then((res: any) => {
        console.log(res)
        show.value = false
        emit('complate', res.html)
      }).finally(() => loading.value = false)
    }
  })
}
</script>
