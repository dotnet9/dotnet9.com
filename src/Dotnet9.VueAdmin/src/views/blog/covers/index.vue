<template>
	<div class="blog-cover-container layout-padding">
		<ProTable ref="tableRef" :request-api="CoversApi.page" :columns="columns" :tool-button="false">
			<template #tools> <el-button type="primary" v-auth="'covers:add'" icon="ele-Plus" @click="onOpen(null)"> 新增 </el-button></template>
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
				<el-button icon="ele-Edit" size="small" v-auth="'covers:edit'" text type="primary" @click="onOpen(row)"> 编辑 </el-button>
				<el-dropdown v-auths="['pictures:page', 'covers:delete']">
					<el-button icon="ele-MoreFilled" size="small" text type="primary" style="padding-left: 12px" />
					<template #dropdown>
						<el-dropdown-menu>
							<el-dropdown-item
								v-if="auth('pictures:page')"
								icon="ele-PictureFilled"
								@click="
									() => {
										router.push({
											path: '/blog/covers/pictures',
											query: {
												id: row.id,
											},
										});
									}
								"
							>
								图片列表
							</el-dropdown-item>
							<el-dropdown-item icon="ele-Delete" v-if="auth('covers:delete')" :divided="auth('pictures:page')" @click="onDeleteRole(row)">
								删除模块
							</el-dropdown-item>
						</el-dropdown-menu>
					</template>
				</el-dropdown>
			</template>
		</ProTable>
		<CoverDialog ref="coverDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="ts" name="blogCovers">
import { defineAsyncComponent, reactive, ref } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import CoversApi from '/@/api/CoversApi';
import type { UpdateCoversInput } from '/@/api/models';
import { auth, auths } from '/@/utils/authFunction';

// 引入组件
const CoverDialog = defineAsyncComponent(() => import('./dialog.vue'));
import ProTable from '/@/components/ProTable/index.vue';
import { ColumnProps } from '/@/components/ProTable/interface';
import { useRouter } from 'vue-router';
const coverType = [
	'首页封面图',
	'归档封面图',
	'分类封面图',
	'标签封面图',
	'模块封面图',
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
const coverDialogRef = ref<InstanceType<typeof CoverDialog>>();
const columns = reactive<ColumnProps[]>([
	{
		type: 'index',
		label: '序号',
		width: 60,
	},
	{
		prop: 'name',
		label: '模块名称',
		search: { el: 'input' },
		width: 200,
	},
	{
		prop: 'type',
		label: '模块类型',
		search: { el: 'select' },
		enum: coverType.map((item, index) => {
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
		isShow: auths(['covers:edit', 'covers:delete']),
	},
]);
// 打开新增标签弹窗
const onOpen = (row: UpdateCoversInput | null) => {
	coverDialogRef.value?.openDialog(row, coverType);
};

// 删除角色
const onDeleteRole = async (row: any) => {
	ElMessageBox.confirm(`确定删除模块：【${row.name}】吗?`, '提示', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			const { succeeded } = await CoversApi.delete({ id: row.id });
			if (succeeded) {
				ElMessage.success('删除成功');
				tableRef.value?.reset();
			}
		})
		.catch(() => {});
};
</script>
<style scoped lang="scss"></style>
