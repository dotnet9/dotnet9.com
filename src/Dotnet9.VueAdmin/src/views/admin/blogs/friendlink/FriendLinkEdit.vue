<template>
    <ElCard>
        <template #header>
            <ElSpace>
                <ElButton :icon="Back" @click="() => router.back()" text></ElButton> {{
                    form.id ? "编辑" : "添加"
                }}友情链接
            </ElSpace>
        </template>
        <ElForm label-width="60" :model="form" @submit.native.prevent :rules="rules" ref="formRef">
            <ElFormItem label="名称" prop="name">
                <ElInput v-model="form.name"></ElInput>
            </ElFormItem>
            <ElFormItem label="域名" prop="url">
                <ElInput v-model="form.url"></ElInput>
            </ElFormItem>
            <ElFormItem label="公开" prop="url">
                <ElSwitch v-model="form.isPublish"></ElSwitch>
            </ElFormItem>
            <ElFormItem label="权重" prop="order">
                <ElInputNumber v-model="form.order"></ElInputNumber>
            </ElFormItem>
            <ElFormItem>
                <ElSpace>
                    <ElButton type="primary" native-type="submit" @click="save()">保存</ElButton>
                    <ElButton @click="() => router.back()">返回</ElButton>
                </ElSpace>
            </ElFormItem>

        </ElForm>
    </ElCard>
</template>

<script lang="ts" setup>
import { FriendLinkModel, FriendLinkService } from '@/shared/service';
import { onMounted, reactive, ref } from 'vue';

import { ElMessage, FormInstance, FormRules } from 'element-plus';
import { useRoute, useRouter } from 'vue-router';
import { Back } from '@element-plus/icons-vue';

const loading = ref(false)

const form = ref<FriendLinkModel>({
    order: 0,
    id: 0,
    isPublish: false
})

const rules = reactive<FormRules>({
    name: [
        {
            required: true, message: '名称不能为空'
        }
    ],
    url: [
        {
            required: true, message: '域名不能为空'
        }, {
            type: 'url', message: "url地址错误"
        }
    ]
}
)
const formRef = ref<FormInstance>();

const router = useRouter();

const save = () => {
    formRef.value?.validate((valid) => {
        if (valid) {
            loading.value = true
            FriendLinkService.edit({
                body: form.value
            }).then(() => {
                ElMessage.success('保存成功')
                router.back()
            }).finally(() => loading.value = false)
        }
    })
}

const route = useRoute();

onMounted(() => {
    const params = history.state.item;
    // console.log('params：', params)
    if (params) {
        var item = params as FriendLinkModel;
        form.value = {
            ...item
        }
    }
})

</script>