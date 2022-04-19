<script>
</script>
<script setup lang="ts">
import {del, get} from "shared/http/HttpClient"
import {nextTick, onMounted, ref, watch} from "vue";
import {useTableData} from "shared/useTableData";
import {PageResponse} from "shared/base";
import {FriendLinkDto} from "./FriendLinkDto";
import EditFriendLink from "./EditFriendLink.vue";
import {ElMessageBox, ElNotification} from "element-plus";

const page = {
  index: 1, size: 10
}
const index = ref(1)

const {loading, pageLayout} = useTableData()

const data = ref<{ list: FriendLinkDto[], total: number }>({list: [], total: 0})

const getData = () => {
  loading.value = true
  get<PageResponse<FriendLinkDto>>("/admin/friendlink/getlist", page).then(res => {
    console.log(res)
    data.value = {
      list: res.data,
      total: res.total
    }
  }).finally(() => loading.value = false)
}
onMounted(() => {
  getData()
})

const currentChange = (index: number) => {
  page.index = index;
  getData()
}

const showEditValue = ref(false)
const id = ref(0)


const showEdit = () => {
  showEditValue.value = true;
}

const editItem = (item: FriendLinkDto) => {
  showEdit()
  id.value = item.id
  console.log(item.id)
}

const delItem = (item: FriendLinkDto) => {
  console.log(item)
  ElMessageBox.confirm(`是否确定删除【${item.siteName}】?`).then(() => {
    loading.value = true;
    del('/admin/friendlink/delete', {id: item.id}).then(() => {
      ElNotification({
        type: 'success',
        message: '删除成功'
      })
      nextTick(() => getData())
    }).finally(() => loading.value = false)
  })
}

</script>
<template>
  <el-card>
    <template #header>
      友情链接
    </template>
    <el-row>
      <el-button type="primary" @click="showEdit()">
        添加链接
      </el-button>
      <EditFriendLink v-model="showEditValue" :id="id" @success="getData()"></EditFriendLink>
    </el-row>
    <el-table v-loading="loading" :data="data.list" border>
      <el-table-column prop="siteName" label="名称"></el-table-column>
      <el-table-column prop="siteUrl" label="域名"></el-table-column>
      <el-table-column prop="auditStatus" label="审核状态">
        <template #default="scope">
          <el-tag v-if="scope.row.auditStatus==0">未审核</el-tag>
          <el-tag v-if="scope.row.auditStatus==1" type="success">通过</el-tag>
          <el-tag v-if="scope.row.auditStatus==2" type="danger">拒绝</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="addTime" label="添加时间"></el-table-column>
      <el-table-column label="操作">
        <template #default="scope">
          <el-button type="default" size="mini" @click="editItem(scope.row)">编辑</el-button>
          <el-button type="danger" size="mini" @click="delItem(scope.row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination
        :layout="pageLayout"
        :total="data.total"
        v-model:current-page="index"
        @current-change="currentChange"
    ></el-pagination>
  </el-card>
</template>
