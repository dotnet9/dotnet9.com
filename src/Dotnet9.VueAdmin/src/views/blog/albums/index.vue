<template>
	<div class="blog-album-container layout-padding">
		<ProTable ref="tableRef" :request-api="AlbumsApi.page" :columns="columns" :tool-button="false">
			<template #tools> <el-button type="primary" v-auth="'albums:add'" icon="ele-Plus" @click="onOpen(null)"> 新增 </el-button></template>
			<template #status="scope">
				<el-tag :type="scope.row.status === 0 ? 'success' : 'danger'"> {{ scope.row.status === 0 ? '启用' : '禁用' }}</el-tag>
			</template>
			<template #cover="{ row }">
				<el-image shape="square" :size="100" fit="cover" :src="row.cover" />
			</template>
			<template #isVisible="{ row }">
				<el-tag :type="row.isVisible ? 'success' : 'danger'"> {{ row.isVisible ? '显示' : '隐藏' }}</el-tag>
			</template>
			<template #action="{ row }">
				<el-button icon="ele-Edit" size="small" v-auth="'albums:edit'" text type="primary" @click="onOpen(row)"> 编辑 </el-button>
				<el-dropdown v-auths="['pictures:page', 'albums:delete']">
					<el-button icon="ele-MoreFilled" size="small" text type="primary" style="padding-left: 12px" />
					<template #dropdown>
						<el-dropdown-menu>
							<el-dropdown-item
								v-if="auth('pictures:page')"
								icon="ele-PictureFilled"
								@click="
									() => {
										router.push({
											path: '/blog/albums/pictures',
											query: {
												id: row.id,
											},
										});
									}
								"
							>
								图片列表
							</el-dropdown-item>
							<el-dropdown-item icon="ele-Delete" v-if="auth('albums:delete')" :divided="auth('pictures:page')" @click="onDeleteRole(row)">
								删除相册
							</el-dropdown-item>
						</el-dropdown-menu>
					</template>
				</el-dropdown>
			</template>
		</ProTable>
		<AlbumDialog ref="albumDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="ts" name="blogAlbums">
import { defineAsyncComponent, reactive, ref } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import AlbumsApi from '/@/api/AlbumsApi';
import type { UpdateAlbumsInput } from '/@/api/models';
import { auth, auths } from '/@/utils/authFunction';

// 引入组件
const AlbumDialog = defineAsyncComponent(() => import('./dialog.vue'));
import ProTable from '/@/components/ProTable/index.vue';
import { ColumnProps } from '/@/components/ProTable/interface';
import { useRouter } from 'vue-router';
const albumType = [
	'首页封面图',
	'归档封面图',
	'分类封面图',
	'标签封面图',
	'相册封面图',
	'说说封面图',
	'关于封面图',
	'留言封面图',
	'个人中心封面图',
	'友情链接封面图',
	'标签列表封面图',
	'分类列表封面图',
];
const router = useRouter();
//  table实例
const tableRef = ref<InstanceType<typeof ProTable>>();
// 弹窗实例
const albumDialogRef = ref<InstanceType<typeof AlbumDialog>>();
const columns = reactive<ColumnProps[]>([
	{
		type: 'index',
		label: '序号',
		width: 60,
	},
	{
		prop: 'name',
		label: '相册名称',
		search: { el: 'input' },
		width: 200,
	},
	{
		prop: 'type',
		label: '相册类型',
		search: { el: 'select' },
		enum: albumType.map((item, index) => {
			return {
				value: index,
				label: item,
			};
		}),
		width: 150,
	},
	{
		prop: 'cover',
		label: '封面',
		width: 180,
	},
	{
		prop: 'sort',
		label: '排序',
	},
	{
		prop: 'isVisible',
		label: '可见',
	},
	{
		prop: 'status',
		label: '状态',
	},
	{
		prop: 'createdTime',
		label: '创建时间',
	},
	{
		prop: 'action',
		label: '操作',
		fixed: 'right',
		width: 150,
		isShow: auths(['albums:edit', 'albums:delete']),
	},
]);
// 打开新增标签弹窗
const onOpen = (row: UpdateAlbumsInput | null) => {
	albumDialogRef.value?.openDialog(row, albumType);
};

// 删除角色
const onDeleteRole = async (row: any) => {
	ElMessageBox.confirm(`确定删除相册：【${row.name}】吗?`, '提示', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			const { succeeded } = await AlbumsApi.delete({ id: row.id });
			if (succeeded) {
				ElMessage.success('删除成功');
				tableRef.value?.reset();
			}
		})
		.catch(() => {});
};
</script>
<style scoped lang="scss"></style>
