<template>
    <ElCard>
        <template #header>
            <div class="text-title">
                分类
            </div>
        </template>
        <ElRow>
            <ElCol>
                <ElSpace>
                    <ElButton :icon="Plus" @click="EditCate(null)"></ElButton>
                    <ElButton :icon="Refresh" @click="getList()"></ElButton>
                </ElSpace>
            </ElCol>
        </ElRow>
        <ElTable :data="list" v-loading="loading" v-bind="table">
            <ElTableColumn label="Id" width="80" prop="id"></ElTableColumn>
            <ElTableColumn label="名称" prop="cateName"></ElTableColumn>
            <ElTableColumn label="关联数量" width="120" prop="postCount"></ElTableColumn>
            <ElTableColumn label="创建时间" width="180" prop="createTime"></ElTableColumn>
            <ElTableColumn label="最后修改时间" width="180" prop="lastUpdateTime"></ElTableColumn>
            <ElTableColumn label="操作" width="120">
                <template #default="scope">
                    <ElSpace>
                        <ElLink type="primary" @click="EditCate(scope.row)">编辑</ElLink>
                        <ElLink type="danger" @click="del(scope.row.id)">删除</ElLink>
                    </ElSpace>
                </template>
            </ElTableColumn>
        </ElTable>
        <ElPagination v-bind="pagination" :total="pageModel.total" v-model="pageModel.index" />
    </ElCard>
</template>

<script lang="ts" setup>
import { useRouter } from 'vue-router';

import { CateService, CateDtoModel } from '@/shared/service'
import { onMounted, reactive, ref, watch } from 'vue';
import { ElCard, ElButton, ElTable, ElTableColumn, ElMessageBox, ElMessage } from 'element-plus';

import { Plus, Refresh } from '@element-plus/icons-vue';
import { table, pagination } from '@/shared/ElConfig'
import { HttpService } from '@/shared/Axios.Config';

const router = useRouter();


const EditCate = (item: CateDtoModel | null) => {
    if (item != null) {
        router.push({
            name: '编辑分类', query: {
                id: item.id,
                cateName: item.cateName,
            }
        })
    } else {
        router.push({
            name: '编辑分类'
        })
    }
}
const del = (id) => {
    ElMessageBox.confirm('确定删除吗?', '提示')
        .then(() => {
            HttpService.delete('/admin/cate/delete', {
                params: {
                    id
                }
            }).then(_ => {
                ElMessage.success('删除成功')
                getList();
            })
        })
}

var loading = ref(false)

const pageModel = reactive({
    index: 1,
    pageSize: 10,
    total: 0
})

watch(() => pageModel.index, () => {
    getList();
})

var list = ref<CateDtoModel[]>([])

const getList = () => {
    loading.value = true;
    CateService.getList({
        ...pageModel
    }).then(res => {
        list.value = []
        if (res.data) {
            list.value = res.data
        }
        console.log(list.value)
        pageModel.total = res.total!

    }).finally(() => loading.value = false)
}

onMounted(() => {
    getList();
})



</script>