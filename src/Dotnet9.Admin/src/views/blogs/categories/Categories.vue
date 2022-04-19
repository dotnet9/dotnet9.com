
<template>
    <el-card>
        <template #header>
            <h1>分类管理</h1>
        </template>
        <el-table :data="tableData" style="width: 100%" border v-loading="loading">
            <el-table-column prop="categoryName" label="名称" width="180" />
            <el-table-column prop="count" label="数量" width="60" />
            <el-table-column prop="isShow" label="显示" width="60">
                <template #default="scope">{{ scope.row.isShow ? '是' : '否' }}</template>
            </el-table-column>

            <el-table-column prop="categoryDesc" label="说明" />
            <el-table-column label="操作" fixed="right" width="150">
                <template #default="scope">
                    <el-button type="primary" size="mini" @click="editCategory(scope.row)">编辑</el-button>
                    <el-popconfirm title="确定删除吗？" @confirm="delCategory(scope.row.id)">
                        <template #reference>
                            <el-button type="danger" size="mini">删除</el-button>
                        </template>
                    </el-popconfirm>
                </template>
            </el-table-column>
        </el-table>
    </el-card>
</template>

<script lang="ts">
import { defineComponent, onMounted, reactive, ref, toRefs } from 'vue';
import EditCategory from "./EditCategory.vue"
import { get, http } from "shared/http/HttpClient"
import { deepCopy } from 'shared/utils/ObjectUtils';
import { CategoryItem } from './CategoryModel';
import LeftMenuLayout from '../../../components/LeftMenuLayout.vue';
import LeftMenu from '../posts/LeftMenu.vue';
import { ElForm, ElMessage } from 'element-plus';
import { useRouter } from 'vue-router';
export default defineComponent({
    components: {
        EditCategory,
        LeftMenuLayout,
        LeftMenu
    },
    setup() {

        const tableData = ref<CategoryItem[]>([])
        const show = ref(false)
        const showDialog = () => {
            cateItem.value = {
                isShow: true
            }
            show.value = !show.value
        }

        const loading = ref(false)

        const cateItem = ref<CategoryItem>();

        const close = () => {

        }

        const getData = async () => {
            try {
                loading.value = true
                var res = await get<CategoryItem[]>('/admin/category/getlist', {});
                tableData.value = res
            } finally {
                loading.value = false
            }
        }
        onMounted(() => {
            getData()
        })

        const delCategory = async (id: number) => {
            try {
                loading.value = true
                await http.delete('/admin/category/delete', { params: { categoryId: id } })
                getData();
            } finally {
                loading.value = false
            }
        }

        const router = useRouter()

        const editCategory = (item: CategoryItem) => {
            let data = deepCopy(item);
            console.log(data)
            router.push({
                name: 'editcate',
                params: {
                    obj: JSON.stringify(data)
                }
            })
        }

        const form = ref<InstanceType<typeof ElForm>>();

        const formRule = reactive({
            categoryName: {
                required: true, message: '分类名称必填', trigger: 'blur'
            }
        })

        const fromLoading = ref(false)
        const formModel = ref<CategoryItem>({
            id: 0,
            categoryName: '',
            pid: 0,
            isShow: true
        })

        const save = async () => {
            try {
                await form.value?.validate();
                loading.value = true
                await http.post('/admin/category/addorupdate', formModel.value);
                ElMessage({
                    message: '保存成功', type: 'success'
                })
                cancel();
                getData()
            } finally {
                loading.value = false
            }
        }

        const cancel = () => {
            form.value?.clearValidate()
            form.value?.resetFields();
            console.log(formModel.value)
            formModel.value.id = 0
        }

        return {
            tableData,
            show,
            showDialog,
            close,
            getData,
            loading,
            delCategory,
            editCategory,
            cateItem,
            formRule,
            form,
            fromLoading,
            formModel,
            save,
            cancel
        }
    }
})
</script>

<style lang="less">
</style>