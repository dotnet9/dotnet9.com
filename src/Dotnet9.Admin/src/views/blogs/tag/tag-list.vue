<template>
  <el-table border :data="data?.data">
    <el-table-column label="标签名称" prop="tagName"></el-table-column>
    <el-table-column label="文章数" prop="count"></el-table-column>
    <el-table-column label="操作">
      <template #default="scope">
        <el-button type="default" size="mini" @click="edit(scope.row)">编辑</el-button>
        <el-button type="danger" size="mini" @click="del(scope.row)">删除</el-button>
      </template>
    </el-table-column>
  </el-table>
  <el-pagination
    layout="total , prev, pager, next"
    :total="data?.total"
    @current-change="currentChange"
  ></el-pagination>
</template>
<script lang="ts" setup>
import { get } from 'shared/http/HttpClient';
import { onMounted, ref } from 'vue';
import { PageResponse } from 'shared/base'

interface TagItem {
  tagName: ''
  id: 0
  count: 0
}

const data = ref<PageResponse<TagItem[]>>()
const page = {
  index: 1,
  size: 10
}
const load = async () => {
  get('/admin/tag/getlist', page).then((res: any) => {
    console.log(res)
    data.value = res
  })
}
onMounted(() => {
  load();
})

const edit = (item: any) => {

}
const del = (item: any) => {

}

const currentChange = (index: number) => {
  page.index = index;
  load();
}

</script>

