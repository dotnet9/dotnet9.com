<template>
    <ElCard>
        <template #header>
            <span class="text-large">
                <ElSpace>
                    <ElButton text :icon="Back" @click="() => router.back()"></ElButton>
                    编辑分类
                </ElSpace>
            </span>
        </template>
        <ElForm label-position="top" :model="formModel" ref="form" @submit.navtive.prevent>
            <ElFormItem label="分类名称" prop="cateName">
                <ElInput placeholder="输入分类名称" v-model="formModel.cateName"></ElInput>
            </ElFormItem>
            <ElFormItem label="">
                <ElButton type="primary" native-type="submit" @click="save()">确定</ElButton>
                <ElButton @click="back()">返回</ElButton>
            </ElFormItem>
        </ElForm>

    </ElCard>
</template>
<script lang="ts" setup>

import { useRoute, useRouter } from 'vue-router';

import { CateRequest, CateService } from '@/shared/service'
import { onMounted, reactive, ref } from 'vue';
import { ElMessage, FormInstance } from 'element-plus';
import { Back } from '@element-plus/icons-vue';

const router = useRouter();

const route = useRoute();

onMounted(() => {
    var data = route.query;
    if (data) {
        console.log(data)
        if (data.id) {
            formModel.id = Number(data.id)
        }
        formModel.cateName = data.cateName?.toString();
    }
})

const formModel = reactive<CateRequest>({
    cateName: '',
    id: 0
})


const back = () => {
    router.go(-1)
}

const form = ref<FormInstance>()

const save = () => {
    form.value?.validate((valid) => {
        if (valid) {
            CateService.edit({
                body: formModel
            })
                .then(() => {
                    ElMessage({
                        message: '保存成功',
                        type: 'success'
                    })
                    router.replace({ name: '文章分类' })
                })
        }
    })

}

</script>