<template>
  <el-row>
    <el-col :xs="24" :sm="24" :md="12" :xl="8" :lg="6">
      <el-form label-position="top" v-loading="loading" ref="instance" :model="formModel">
        <el-form-item label="标签名称">
          <el-input placeholder="标签名称"></el-input>
        </el-form-item>
        <el-form-item label>
          <el-button type="primary" @click="save()">保存</el-button>
          <el-button type="default" @click="back()">返回</el-button>
        </el-form-item>
      </el-form>
    </el-col>
  </el-row>
</template>

<script lang="ts" setup>
import { post } from 'shared/http/HttpClient';
import { useForm } from 'shared/useForm'
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const back = () => {
  router.back()
}

const { loading, instance } = useForm();

const formModel = ref({
  id: 0,
  tag: ''
})
const save = () => {
  loading.value = true;

  console.log(instance.value?.validate)
  instance.value?.validate()?.then((valid: any) => {

    console.log('instance.value' + valid)
    if (valid) {
      post('/admin/tag/update', formModel.value).then(res => {

      }).finally(() => {
        loading.value = false
      })
    } else {
      loading.value = false;
    }
  })
}
</script>

<style>
</style>