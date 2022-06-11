<script>
</script>
<script setup lang="ts">
import { del, get } from "shared/http/HttpClient";
import { nextTick, onMounted, ref, watch } from "vue";
import { useTableData } from "shared/useTableData";
import { PageResponse } from "shared/base";
import { UrlLinkDto } from "./UrlLinkDto";
import EditUrlLink from "./EditUrlLink.vue";
import { ElMessageBox, ElNotification } from "element-plus";

const page = {
  index: 1,
  size: 10,
};
const index = ref(1);

const { loading, pageLayout } = useTableData();

const data = ref<{ list: UrlLinkDto[]; total: number }>({ list: [], total: 0 });

const getData = () => {
  loading.value = true;
  get<PageResponse<UrlLinkDto>>("/api/urllink/list", page)
    .then((res) => {
      data.value = {
        list: res.data,
        total: res.total,
      };
    })
    .finally(() => (loading.value = false));
};
onMounted(() => {
  getData();
});

const currentChange = (index: number) => {
  page.index = index;
  getData();
};

const showEditValue = ref(false);
const id = ref(0);

const showEdit = () => {
  showEditValue.value = true;
};

const editItem = (item: UrlLinkDto) => {
  showEdit();
  id.value = item.id;
  console.log(item.id);
};

const delItem = (item: UrlLinkDto) => {
  console.log(item);
  ElMessageBox.confirm(`是否确定删除【${item.name}】?`).then(() => {
    loading.value = true;
    del("/api/urllink/delete", { id: item.id })
      .then(() => {
        ElNotification({
          type: "success",
          message: "删除成功",
        });
        nextTick(() => getData());
      })
      .finally(() => (loading.value = false));
  });
};
</script>
<template>
  <el-card>
    <template #header> 链接 </template>
    <el-row>
      <el-button type="primary" @click="showEdit()"> 添加链接 </el-button>
      <EditUrlLink
        v-model="showEditValue"
        :id="id"
        @success="getData()"
      ></EditUrlLink>
    </el-row>
    <el-table v-loading="loading" :data="data.list" border>
      <el-table-column prop="name" label="名称"></el-table-column>
      <el-table-column prop="url" label="链接"></el-table-column>
      <el-table-column prop="description" label="描述"></el-table-column>
      <el-table-column prop="index" label="顺序"></el-table-column>
      <el-table-column prop="kind" label="类型">
        <template #default="scope">
          <el-tag v-if="scope.row.kind == 0">私密</el-tag>
          <el-tag v-if="scope.row.kind == 1" type="success">网站</el-tag>
          <el-tag v-if="scope.row.kind == 2" type="danger">友情链接</el-tag>
          <el-tag v-if="scope.row.kind == 3" type="danger">课程链接</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="createTime" label="添加时间"></el-table-column>
      <el-table-column label="操作">
        <template #default="scope">
          <el-button type="default" size="small" @click="editItem(scope.row)"
            >编辑</el-button
          >
          <el-button type="danger" size="small" @click="delItem(scope.row)"
            >删除</el-button
          >
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
