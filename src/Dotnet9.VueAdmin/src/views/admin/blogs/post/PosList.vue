<template>
    <ElCard>
        <template #header>
            <div class="card-header">
                文章
            </div>
        </template>
        <ElRow>
            <ElCol>
                <ElSpace>
                    <el-button :icon="Plus" @click="toEdit(null)" type="primary">写一篇</el-button>
                    <ElButton :icon="Refresh" @click="getList()">刷新</ElButton>
                </ElSpace>
            </ElCol>
        </ElRow>
        <ElTable :data="tableData" v-loading="loading" v-bind="table">
            <ElTableColumn label="标题" prop="title">
                <template #default="scope">
                    <ElButton type="primary" link @click="toDetail(scope.row.id)">{{ scope.row.title }}</ElButton>
                </template>
            </ElTableColumn>
            <ElTableColumn label="更新时间" prop="lastUpdateTime" width="180"></ElTableColumn>
            <ElTableColumn label="分类" width="120" prop="cates">
                <template #default="scope">
                    <ElSpace wrap>
                        <ElTag v-for="item in scope.row.cateItems" :key="item.id">
                            {{ item.cateName }}</ElTag>
                    </ElSpace>
                </template>
            </ElTableColumn>
            <ElTableColumn label="标签" width="120">
                <template #default="scope">
                    <ElSpace wrap>
                        <ElTag v-for="item in scope.row.tagItems" :key="item.id">
                            {{ item.tagName }}</ElTag>
                    </ElSpace>
                </template>
            </ElTableColumn>
            <ElTableColumn label="阅读人数" width="80" prop="readCount"></ElTableColumn>
            <ElTableColumn label="评论" width="80" prop="commentCount"></ElTableColumn>
            <ElTableColumn label="置顶" width="80" prop="commentCount">
                <template #default="scope">
                    <ElSwitch v-model="scope.row.isTop" @change="topHandler(scope.row)"></ElSwitch>
                </template>
            </ElTableColumn>
            <ElTableColumn label="发布" width="80" prop="commentCount">
                <template #default="scope">
                    <ElSwitch v-model="scope.row.isPublish" @change="publishHandler(scope.row)"></ElSwitch>
                </template>
            </ElTableColumn>
            <ElTableColumn label="操作" width="150">
                <template #default="scope">
                    <ElSpace>
                        <ElButton size="small" @click="toEdit(scope.row.id)">编辑</ElButton>

                    </ElSpace>
                </template>
            </ElTableColumn>
        </ElTable>
        <ElPagination v-bind="pagination" :total="pageModel.total" v-model="pageModel.index" />
    </ElCard>
</template>

<script setup lang="ts">

import { useRouter } from 'vue-router'

import { PostItemModel, PostService } from '@/shared/service'
import { onMounted, ref, reactive, watch } from 'vue';

import { Plus, Refresh } from '@element-plus/icons-vue';

import { table, pagination } from '@/shared/ElConfig'
import { HttpService } from '@/shared/Axios.Config';


const router = useRouter();

const tableData = ref<PostItemModel[]>();

const toDetail = (id: number) => {
    window.open(`/post/${id}.html`)
}

const topHandler = (e) => {
    console.log(e)
    HttpService.post('/admin/post/top', {
        id: e.id
    }).catch(_ => {
        e.isTop = !e.isTop;
    })
}

const publishHandler = (e)=>{
    HttpService.post('/admin/post/publish', {
        id: e.id
    }).catch(_ => {
        e.isPublish = !e.isPublish;
    })
}


const toEdit = (id: number | null) => {
    console.log(id)
    if (!id) {
        router.push({
            name: '编辑文章'
        })
    } else {
        router.push({
            name: '编辑文章',
            query: {
                id: id
            }
        })
    }
}

const pageModel = reactive({
    index: 1,
    size: 15,
    total: 0
})


const loading = ref(false)

watch(() => pageModel.index, () => getList())

const getList = () => {
    loading.value = true;
    PostService.list({
        pageSize: pageModel.size, index: pageModel.index
    }).then(res => {
        tableData.value = []
        pageModel.total = 0;
        if (res.data) {
            tableData.value.push(...res.data);
            pageModel.total = res.total!;
        }
    }).finally(() => {
        loading.value = false;
    })
}

onMounted(() => {
    getList();
})

</script>

<style lang="scss">
.card-header {
    display: flex;
    flex-direction: row;
    justify-content: space-between;

    .title {
        font-size: 24px;
    }
}
</style>