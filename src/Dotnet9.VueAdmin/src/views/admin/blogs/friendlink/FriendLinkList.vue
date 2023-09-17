<template>
    <ElCard>
        <template #header>
            友情链接
        </template>
        <ElRow>
            <ElCol>
                <ElSpace>
                    <ElButton :icon="Refresh" @click="getList()"></ElButton>
                    <ElButton :icon="Plus" @click="edit(null)" type="primary"></ElButton>
                </ElSpace>
            </ElCol>
        </ElRow>
        <ElTable v-loading="loading" :data="table?.data">
            <ElTableColumn label="名称" prop="name"></ElTableColumn>
            <ElTableColumn label="地址" prop="url"></ElTableColumn>
            <ElTableColumn label="权重" prop="order"></ElTableColumn>
            <ElTableColumn label="状态" prop="isPublish">
                <template #default="scope">
                    <template v-if="scope.row.isPublish">
                        <ElTag type="success">公开</ElTag>
                    </template>
                    <template v-if="!scope.row.isPublish">
                        <ElTag type="danger">隐藏</ElTag>
                    </template>
                </template>
            </ElTableColumn>
            <ElTableColumn label="操作">
                <template #default="scope">
                    <ElSpace>
                        <ElButton :icon="Edit" @click="edit(scope.row)"></ElButton>
                        <ElButton :icon="Delete" type="danger"></ElButton>
                    </ElSpace>
                </template>
            </ElTableColumn>
        </ElTable>
        <ElPagination background small layout="prev, pager, next" v-model:current-page="page.index"
            :total="table?.total" v-model:page-size="page.pageSize"></ElPagination>
    </ElCard>
</template>
<script lang="ts" setup>
import { Refresh, Plus } from '@element-plus/icons-vue';
import { onMounted, reactive, ref, watch } from 'vue';
import { useRouter } from 'vue-router';

import { FriendLinkModel, FriendLinkModelPageDto, FriendLinkService, PagedResultDto } from '@/shared/service'

import { Edit, Delete } from '@element-plus/icons-vue';

const loading = ref(false)

const router = useRouter();

const edit = (item: any) => {
    console.log(item)
    router.push({
        name: '友情链接编辑',
        state: { item: { ...item } }
    })
}

const page = reactive({
    index: 1, pageSize: 10
})

watch(() => page.index, () => getList())
watch(() => page.pageSize, () => getList())

const table = ref<FriendLinkModelPageDto>({
    total: 0
})

const getList = () => {
    loading.value = true;
    FriendLinkService.getList({ index: 1, pageSize: 20 })
        .then(res => {
            table.value = res;
        }).finally(() => {
            loading.value = false;
        })
}

onMounted(() => {
    getList();
})

</script>