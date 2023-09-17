<template>
    <ElDialog v-model="visibleDialog" title="修改密码">
        <ElForm ref="form" label-width="100px" :model="formModel" :rules="rules" @submit.native.prevent>
            <ElFormItem label="旧密码" prop="oldPwd">
                <ElInput v-model="formModel.oldPwd"></ElInput>
            </ElFormItem>
            <ElFormItem label="新密码" prop="newPwd">
                <ElInput v-model="formModel.newPwd"></ElInput>
            </ElFormItem>
            <ElFormItem label="重复密码" prop="confirmNewPwd">
                <ElInput v-model="formModel.confirmNewPwd"></ElInput>
            </ElFormItem>
            <ElFormItem>
                <ElButton type="primary" native-type="submit" @click="save()" :loading="loading">确定</ElButton>
            </ElFormItem>
        </ElForm>
    </ElDialog>
</template>

<script lang="ts" setup>
import { AuthService, ChangeCurrPwd } from '@/shared/service';
import { ElMessage, FormInstance, FormRules } from 'element-plus';
import { onMounted, reactive, ref, watch } from 'vue';
import { useRouter } from 'vue-router';



const props = defineProps({
    modelValue: {
        type: Boolean,
        default: false
    }
})

const emit = defineEmits(['update:modelValue'])

const visibleDialog = ref(false)

watch(visibleDialog, () => {
    emit('update:modelValue', visibleDialog.value)
})

watch(() => props.modelValue, () => {
    visibleDialog.value = props.modelValue;
    form.value?.resetFields();
})

const formModel = reactive({
    oldPwd: '',
    newPwd: '',
    confirmNewPwd: ''
})

const rules: FormRules = {
    oldPwd: [
        {
            required: true, message: '旧密码不能为空'
        }
    ],
    newPwd: [
        {
            required: true, message: '新密码不能为空'
        }
    ],
    confirmNewPwd: [
        {
            required: true, message: '重复密码不能为空'
        }, {
            validator: () => formModel.newPwd == formModel.confirmNewPwd,
            message: '两次输入新密码不一样'
        }
    ]
}

const loading = ref(false)

const router = useRouter();

const form = ref<FormInstance>();

const save = () => {

    form.value?.validate((valid) => {
        if (valid) {
            loading.value = true;
            AuthService.changeCurrPwd({
                body: {
                    oldPwd: formModel.oldPwd,
                    newPwd: formModel.newPwd
                }
            })
                .then(() => {
                    ElMessage.success('修改密码成功，请重新登录')
                    router.replace({
                        name: '登录'
                    })
                }).finally(() => {
                    loading.value = false
                })
        }
    })
}

onMounted(() => {
    form.value?.resetFields();
})

</script>