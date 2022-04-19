<template>
  <el-dialog @opened="open()" v-model="showDialog" @closed="close()" :title="formModel.id==0?'添加友情链接':'编辑友情链接'">
    <el-form :model="formModel" :rules="formRule" ref="instance" v-loading="loading" label-width="100px"
             @keyup.enter.native="save()">
      <el-form-item label="名称" prop="siteName">
        <el-input v-model="formModel.siteName"></el-input>
      </el-form-item>
      <el-form-item label="地址" prop="siteUrl">
        <el-input v-model="formModel.siteUrl"></el-input>
      </el-form-item>
      <el-form-item label="权重" prop="weight">
        <el-input-number v-model="formModel.weight"></el-input-number>
      </el-form-item>
      <el-form-item label="状态" prop="auditStatus">
        <el-select v-model.number="formModel.auditStatus">
          <el-option :value="0" label="未审核"></el-option>
          <el-option :value="1" label="通过"></el-option>
          <el-option :value="2" label="拒绝"></el-option>
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
import {ref, watch} from 'vue'
import {useForm} from "shared/useForm";
import {get, post} from "shared/http/HttpClient";

const props = withDefaults(defineProps<{ modelValue: boolean, id: number }>(), {
  modelValue: false, id: 0
})

const emit = defineEmits(['update:modelValue','success']);

const showDialog = ref(false)

const formModel = ref({
  siteName: '',
  siteUrl: '',
  weight: 0,
  auditStatus: 0,
  id: 0
})

const formRule = {
  siteName: {
    required: true, message: '站点名称不能为空'
  },
  siteUrl: {
    required: true, message: '站点地址不能为空'
  }
}

watch(() => props.modelValue, () => {
  showDialog.value = props.modelValue;
  if (props.id > 0) {
    loadItem()
  }else{
    clearForm()
  }
})


const loadItem = () => {
  loading.value = true;
  get('/admin/friendlink/get',{id:props.id}).then((res:any)=>{
    console.log(res)
    formModel.value = {
      id:res.id,
      siteUrl: res.siteUrl,
      siteName: res.siteName,
      auditStatus: res.auditStatus,
      weight: res.weight
    }
  }).finally(()=>loading.value=false)
}

const {instance, loading, clearForm} = useForm()

const open = () => {

}

const close = () => {
  emit('update:modelValue', false)
}

const save = () => {
  loading.value = true;
  post('/admin/friendlink/addorupdate', formModel.value).then(() => {
    close()
    emit('success')
  }).finally(() => loading.value = false)
}

</script>
