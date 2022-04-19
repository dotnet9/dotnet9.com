<template>
    <el-card class="anim1">
        <template #header>新建页面</template>
        <el-form
            v-loading="loading"
            label-width="80px"
            :model="formModel"
            :rules="formRules"
            ref="form"
        >
            <el-form-item label="Url" prop="url">
                <el-input placeholder="输入Url" v-model="formModel.url"></el-input>
            </el-form-item>
            <el-form-item label="标题" prop="title">
                <el-input placeholder="输入标题" v-model="formModel.title"></el-input>
            </el-form-item>
            <el-form-item label="内容" prop="content">
                <WangEditor v-model="formModel.content"></WangEditor>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" @click="submitForm()">保存</el-button>
            </el-form-item>
        </el-form>
    </el-card>
</template>

<script lang="ts">
import { ElForm } from "element-plus";
import { http } from "shared/http/HttpClient";
import { defineComponent, ref } from "vue";
import WangEditor from "../../../components/WangEditor.vue";
export default defineComponent({
    components: {
        WangEditor
    },
    setup() {
        const formModel = ref({
            url: '',
            title: '',
            content: ''
        })
        const formRules = {
            url: [
                {
                    required: true, message: 'url不能为空'
                }
            ],
            title: [
                {
                    required: true, message: 'title不能为空'
                }
            ],
            content: [
                {
                    required: true, message: '内容不能为空'
                }
            ]
        }

        const form = ref<InstanceType<typeof ElForm>>();

        const submitForm = async () => {
            form.value?.validate(isValid => {
                if (isValid) {
                    console.log(formModel.value)
                    loading.value = true
                    http.post('/admin/pages/addorupdate', formModel.value).then(res => {

                    }).finally(() => {
                        loading.value = false
                    })
                }
            });
        }

        const loading = ref(false)

        return {
            formModel,
            formRules,
            submitForm,
            form,
            loading
        }

    }
})
</script>

<style>
</style>