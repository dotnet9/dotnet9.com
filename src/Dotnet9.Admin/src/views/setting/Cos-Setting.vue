
<template>
    <el-form
        label-width="100px"
        label-position="top"
        v-loading="loading"
        :model="formModel"
        :rules="rules"
        ref="form"
    >
        <el-form-item label="地域简称" prop="cos_region">
            <el-input placeholder="COS 地域的简称" v-model="formModel.cos_region"></el-input>
        </el-form-item>
        <el-form-item label="SecretId" prop="secret_id">
            <el-input placeholder="云 API 密钥 SecretId" v-model="formModel.secret_id"></el-input>
        </el-form-item>
        <el-form-item label="SecretKey" prop="secret_key">
            <el-input placeholder="云 API 密钥 SecretKey" v-model="formModel.secret_key"></el-input>
        </el-form-item>
        <el-form-item label="存储桶名称" prop="bucket">
            <el-input placeholder="存储桶名称，此处填入格式必须为 bucketname-APPID" v-model="formModel.bucket"></el-input>
        </el-form-item>
        <el-form-item label="访问默认域名" prop="host">
            <el-input placeholder="访问默认域名" v-model="formModel.host"></el-input>
        </el-form-item>
        <el-form-item>
            <el-button type="primary" @click="submit()">保存</el-button>
        </el-form-item>
    </el-form>
</template>

<script lang="ts" setup>
import { onMounted, ref } from 'vue';
import { ElForm } from 'element-plus';
import { post, get } from 'shared/http/HttpClient';

const loading = ref(false);

const formModel = ref({
    cos_region: '', secret_id: '', secret_key: '', bucket: '', host: ''
})

const rules = {
    host: [
        {
            required: true, message: '域名不能为空'
        }
    ],
    cos_region: [{ required: true, message: '地域简称不能为空' }],
    secret_id: [
        {
            required: true, message: 'SecretId不能为空'
        }
    ],
    secret_key: [
        {
            required: true, message: 'SecretKey不能为空'
        }
    ],
    bucket: [
        { required: true, message: '存储桶名称不能为空' }
    ]
}

const form = ref<InstanceType<typeof ElForm>>()

const submit = () => {
    console.log(form.value)
    loading.value = true
    form.value?.validate(async (valid) => {
        console.log('valid:' + valid)
        if (valid) {
            console.log(formModel.value)
            try {
                await post('/admin/dicdata/update', {
                    groupKey: 'tencent_cos',
                    list: formModel.value
                })
            } finally {
                loading.value = false
            }
        }
        loading.value = false;
    });
}

const load = async () => {
    loading.value = true
    var res = await get<any>('/admin/dicdata/get', { groupName: 'tencent_cos' })
    console.log(res)
    formModel.value = {
        cos_region: res.cos_region,
        secret_id: res.secret_id,
        secret_key: res.secret_key,
        bucket: res.bucket,
        host: res.host
    }
    loading.value = false
}

onMounted(() => {
    load();
})


</script>

<style>
</style>