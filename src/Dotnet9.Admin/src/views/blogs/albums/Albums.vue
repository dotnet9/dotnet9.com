
<template>
  <el-card>
    <template #header>
      <h1>专辑管理</h1>
    </template>
    <el-table :data="tableData" style="width: 100%" border v-loading="loading">
      <el-table-column prop="name" label="名称" width="180" />
      <el-table-column label="封面" width="230">
        <template #default="scope">
          <img :src="scope.row.cover" class="album-img" />
        </template>
      </el-table-column>
      <el-table-column prop="slug" label="别名" width="200" />
      <el-table-column prop="isShow" label="显示" width="60">
        <template #default="scope">{{
          scope.row.isShow ? "是" : "否"
        }}</template>
      </el-table-column>

      <el-table-column prop="description" label="说明" />
      <el-table-column label="操作" fixed="right" width="150">
        <template #default="scope">
          <el-button type="primary" size="mini" @click="editAlbum(scope.row)"
            >编辑</el-button
          >
          <el-popconfirm title="确定删除吗？" @confirm="delAlbum(scope.row.id)">
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
import { defineComponent, onMounted, reactive, ref, toRefs } from "vue";
import EditAlbum from "./EditAlbum.vue";
import { get, http } from "shared/http/HttpClient";
import { deepCopy } from "shared/utils/ObjectUtils";
import { AlbumItem } from "./AlbumModel";
import LeftMenuLayout from "../../../components/LeftMenuLayout.vue";
import LeftMenu from "../posts/LeftMenu.vue";
import { ElForm, ElMessage } from "element-plus";
import { useRouter } from "vue-router";
export default defineComponent({
  components: {
    EditAlbum,
    LeftMenuLayout,
    LeftMenu,
  },
  setup() {
    const tableData = ref<AlbumItem[]>([]);
    const show = ref(false);
    const showDialog = () => {
      albumItem.value = {
        isShow: true,
      };
      show.value = !show.value;
    };

    const loading = ref(false);

    const albumItem = ref<AlbumItem>();

    const close = () => {};

    const getData = async () => {
      try {
        loading.value = true;
        var res = await get<AlbumItem[]>("/api/album/list", {});
        tableData.value = res;
      } finally {
        loading.value = false;
      }
    };
    onMounted(() => {
      getData();
    });

    const delAlbum = async (id: number) => {
      try {
        loading.value = true;
        await http.delete("/api/album/delete", {
          params: { id: id },
        });
        getData();
      } finally {
        loading.value = false;
      }
    };

    const router = useRouter();

    const editAlbum = (item: AlbumItem) => {
      let data = deepCopy(item);
      router.push({
        name: "editalbum",
        params: {
          obj: JSON.stringify(data),
        },
      });
    };

    const form = ref<InstanceType<typeof ElForm>>();

    const formRule = reactive({
      name: {
        required: true,
        message: "名称必填",
        trigger: "blur",
      },
    });

    const fromLoading = ref(false);
    const formModel = ref<AlbumItem>({
      id: 0,
      name: "",
      parentId: 0,
      isShow: true,
    });

    const save = async () => {
      try {
        await form.value?.validate();
        loading.value = true;
        await http.post("/admin/category/addorupdate", formModel.value);
        ElMessage({
          message: "保存成功",
          type: "success",
        });
        cancel();
        getData();
      } finally {
        loading.value = false;
      }
    };

    const cancel = () => {
      form.value?.clearValidate();
      form.value?.resetFields();
      console.log(formModel.value);
      formModel.value.id = 0;
    };

    return {
      tableData,
      show,
      showDialog,
      close,
      getData,
      loading,
      delAlbum,
      editAlbum,
      albumItem,
      formRule,
      form,
      fromLoading,
      formModel,
      save,
      cancel,
    };
  },
});
</script>

<style lang="less">
.album-img {
  width: 200px;
}
</style>