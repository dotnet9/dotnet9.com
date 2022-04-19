<template>
    <el-dialog v-model="show" @close="close()" title="修改密码" @open="clearForm()">
        <el-form
            label-width="100px"
            :model="formModel"
            :rules="rules"
            ref="instance"
            v-loading="loading"
        >
            <el-form-item label="老密码" prop="oldpwd">
                <el-input v-model="formModel.oldpwd" type="password"></el-input>
            </el-form-item>
            <el-form-item label="新密码" prop="newpwd">
                <el-input v-model="formModel.newpwd" type="password"></el-input>
            </el-form-item>
            <el-form-item label="重复新密码" prop="repwd">
                <el-input v-model="formModel.repwd" type="password"></el-input>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" @click="save()">保存</el-button>
                <el-button type="default" @click="clearForm()">清空</el-button>
            </el-form-item>
        </el-form>
    </el-dialog>
</template>

<script lang="ts">
import { ElForm, ElMessage } from "element-plus";
import { defineComponent, nextTick, ref, toRefs, watch } from "vue";
import { useForm } from 'shared/useForm'
import { post } from 'shared/http/HttpClient'

export default defineComponent({
    props: {
        modelValue: {
            type: Boolean,
            default: true
        }
    },
    setup(props, context) {

        console.log(props.modelValue)

        const { modelValue } = toRefs(props)

        const show = ref(modelValue.value)

        const { loading, instance, clearForm } = useForm();


        const formModel = ref({
            oldpwd: '',
            newpwd: '',
            repwd: ''
        })

        const rules = {
            oldpwd: [
                {
                    required: true, message: '旧的密码不能为空'
                }
            ],
            newpwd: [
                {
                    required: true, message: '新密码不能为空'
                }
            ],
            repwd: [
                {
                    required: true, message: '新密码重复字段不能为空'
                },
                {
                    type: 'string',
                    required: true,
                    validator: () => {
                        return formModel.value.newpwd == formModel.value.repwd
                    },
                    message: '两次输入密码不一致'
                }
            ]
        }

        watch(() => modelValue.value, () => {
            console.log('change')
            show.value = modelValue.value
        })

        const close = () => {
            context.emit('update:modelValue', false)
        }

        const form = ref<InstanceType<typeof ElForm>>();

        const clear = () => {
            form.value?.resetFields();

            nextTick(() => {
                form.value?.clearValidate()
            })
        }

        const save = () => {
            loading.value = true;
            instance.value?.validate(valid => {
                if (valid) {
                    post('/admin/account/changepwd', {
                        oldpwd: formModel.value.oldpwd, newpwd: formModel.value.newpwd
                    }).then(() => {
                        instance.value?.resetFields();
                        nextTick(() => {
                            instance.value?.clearValidate();
                        })
                        ElMessage({
                            message: '修改成功', type: 'success'
                        })
                        close();
                    }).finally(() => {
                        loading.value = false
                    })
                } else {
                    loading.value = false
                }
            })
        }



        return {
            close,
            formModel,
            show,
            rules,
            clear,
            form,
            loading, instance, save,
            clearForm
        }
    }
})
</script>

<style>
</style>