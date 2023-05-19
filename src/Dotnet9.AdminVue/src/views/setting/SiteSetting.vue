<template>
    <ElForm label-width="100" v-loading="loading" :model="form" @submit.native.prevent>
        <ElFormItem label="网站名称" prop="siteName">
            <ElInput v-model="form.siteName"></ElInput>
        </ElFormItem>
        <ElFormItem label="ICP备案号" prop="icp">
            <ElInput v-model="form.icp"></ElInput>
        </ElFormItem>
        <ElFormItem label="网站描述">
            <ElInput></ElInput>
        </ElFormItem>
        <ElFormItem>
            <ElButton type="primary" @click="save()" native-type="submit">保存</ElButton>
        </ElFormItem>
    </ElForm>
</template>
<script setup lang="ts">
import { ConfigService, SiteConfig } from '@/shared/service';
import { ElMessage } from 'element-plus';
import { onMounted, ref } from 'vue';


const loading = ref(false)

const form = ref<SiteConfig>({
    siteName: '',
    age: 0,
    icp: ''
});

const save = () => {
    ConfigService.setSite({
        body: form.value
    })
        .then(() => {
            ElMessage.success('保存成功')
        })
}

const load = ()=>{
    ConfigService.getSiteConfig().then(res=>{
        form.value = res;
    })
}

onMounted(()=>{
    load();
})

</script>