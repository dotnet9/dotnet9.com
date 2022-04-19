<template>
    <el-card class="anim1">
        <template #header>
            <h1>{{ formModel.id ? '编辑分类' : '添加分类' }}</h1>
        </template>
        <div v-loading="loading">
            <el-form label-width="80px" :rules="formRule" :model="formModel" ref="form">
                <el-form-item label="名称" prop="cateName">
                    <el-input placeholder="输入分类名称" v-model="formModel.categoryName"></el-input>
                </el-form-item>
                <el-form-item label="描述" prop="categoryDesc">
                    <el-input placeholder="描述" type="textarea" v-model="formModel.categoryDesc"></el-input>
                </el-form-item>
                <el-form-item label="是否显示">
                    <el-switch v-model="formModel.isShow"></el-switch>
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" @click="save()">{{ formModel.id ? '保存修改' : '添加' }}</el-button>
                    <el-button @click="restore()">重置</el-button>
                </el-form-item>
            </el-form>
        </div>
    </el-card>
</template>

<script lang="ts" setup>
import { reactive } from 'vue'
import { defineComponent, onMounted, PropType, ref, toRefs, watch } from 'vue'
import { http } from 'shared/http/HttpClient'
import { ElForm, ElMessage } from 'element-plus'
import { CategoryItem } from './CategoryModel'
import { useRoute, useRouter } from 'vue-router'

const loading = ref(false)

const defaultValue = {
    categoryName: '',
    categoryDesc: '',
    pid: 0,
    isShow: true
}

const formModel = ref<CategoryItem>(defaultValue)
const form = ref<InstanceType<typeof ElForm>>();

const formRule = reactive({
    categoryName: {
        required: true, message: '分类名称必填'
    }
})


const router = useRouter()

const route = useRoute()

watch(() => route.params, params => {
    console.log('监听:', params)
})

if (route.params.obj) {
    formModel.value = JSON.parse(route.params.obj.toString()) as CategoryItem
}

onMounted(() => {
})

const restore = () => {
    router.replace("/admin/editcate")
    formModel.value = {
        categoryName: '111',
        categoryDesc: '',
        pid: 0,
        isShow: false
    }
}

const save = async () => {
    try {
        await form.value?.validate();
        loading.value = true
        await http.post('/admin/category/addorupdate', formModel.value);
        ElMessage({
            message: '保存成功', type: 'success'
        })
        form.value?.resetFields();
        form.value?.clearValidate();
        router.replace("/admin/categories")
    } finally {
        loading.value = false
    }
}


</script>

<style lang="less">
</style>