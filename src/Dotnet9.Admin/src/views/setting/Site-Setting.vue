<template>
  <el-form
      label-width="100px"
      label-position="top"
      ref="instance"
      :model="formModel"
      :rules="rules"
      v-loading="loading"
  >
    <el-form-item label="网站名称" prop="site_name">
      <el-input placeholder="网站名称" v-model="formModel.site_name"></el-input>
    </el-form-item>
    <el-form-item label="网站域名" prop="host">
      <el-input placeholder="网站主域名 https://xxxx.com" v-model="formModel.host"></el-input>
    </el-form-item>
    <el-form-item label="网站描述" prop="site_description">
      <el-input placeholder="网站描述，meta元素的description，用于整站的seo描述" v-model="formModel.site_description"></el-input>
    </el-form-item>
    <el-form-item label="图片懒加载">
      <el-switch v-model="formModel.img_lazy" active-value="true" inactive-value="false"></el-switch>
    </el-form-item>
    <el-form-item label="ICP备案号" prop="icp">
      <el-input rows="5" v-model="formModel.icp"></el-input>
    </el-form-item>
    <el-form-item label="统计代码" prop="statistics">
      <el-input type="textarea" rows="8" v-model="formModel.statistics"></el-input>
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="submit()">保存</el-button>
    </el-form-item>
  </el-form>
</template>

<script lang="ts">
import {defineComponent, onMounted, ref} from 'vue'
import {useForm} from 'shared/useForm'
import {post, get} from 'shared/http/HttpClient'

export default defineComponent({
  setup() {

    const formModel = ref({
      site_name: '',
      icp: '',
      site_description: '',
      statistics: '',
      img_lazy: 'false',
      host: ''
    })

    const {instance, loading} = useForm();


    const rules = {
      site_name: [
        {
          required: true, message: '站点名称不能为空'
        }
      ],
      host: [
        {
          required: true, message: '域名不能为空'
        }
      ],
      site_description: [
        {required: true, message: '网站描述不能为空'}
      ]
    }

    const load = async () => {
      let res = await get<any>('/admin/dicdata/get', {groupName: 'site'})
      formModel.value = {
        site_name: res.site_name,
        icp: res.icp,
        statistics: res.statistics,
        img_lazy: res.img_lazy,
        host: res.host,
        site_description: res.site_description
      }
    }

    onMounted(() => {
      load();
    })

    const submit = () => {
      loading.value = true
      console.log(instance.value)
      instance.value?.validate(async (valid) => {
        console.log(valid)
        if (valid) {
          post('/admin/dicdata/update', {
            groupkey: 'site',
            list: formModel.value
          }).finally(() => loading.value = false)
        } else {
          loading.value = false
        }
      })
    }

    onMounted(() => {

      console.log(loading.value)
    })

    return {
      formModel,
      rules,
      instance, loading,
      submit
    }
  }
})
</script>

<style>
</style>