
<template>
    <el-card class="anim1">
        <template #header>所有页面</template>
        <el-table :data="data.data" v-loading="loading">
            <el-table-column label="Url" prop="url"></el-table-column>
            <el-table-column label="标题" prop="title"></el-table-column>
            <el-table-column label="最后修改时间" width="150" prop="updateTime"></el-table-column>
            <el-table-column label="操作" fixed="right" width="200">
                <template #default="scope">
                    <el-button type="default" @click="editHandler(scope.row.id)">编辑</el-button>
                    <el-button type="danger" @click="delHandler(scope.row.id)">删除</el-button>
                </template>
            </el-table-column>
        </el-table>
        <el-pagination
            layout="total , prev, pager, next"
            :total="data.total"
            @current-change="currentChange"
        ></el-pagination>
    </el-card>
</template>

<script lang="ts">
import { http, get } from 'shared/http/HttpClient'
import { onMounted, ref } from 'vue'
import { PageResponse } from 'shared/base'
interface PageItem {
    url: string
    title: string
    updateTime: string
    id: number
    addTime: string
}

export default {
    setup() {

        const data = ref<PageResponse<PageItem>>({
            total: 0,
            data: []
        })

        const loading = ref(false)

        const delHandler = (id: number) => {

        }
        const editHandler = (id: number) => {

        }

        const currentChange = () => {

        }

        const loadData = () => {
            get<PageResponse<PageItem>>('/admin/pages/getlist', {
                params: {

                }
            }).then(res => {
                console.log('res:', res)
                data.value.data = res.data
                data.value.total = res.total
            })
        }
        onMounted(() => {
            loadData();
        })
        return {
            delHandler,
            editHandler,
            currentChange,
            data,
            loading
        }
    }
}
</script>

<style>
</style>