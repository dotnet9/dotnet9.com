<template>
	<div class="blog-friendLink-container layout-padding">
		<ProTable ref="tableRef" :request-api="FriendLinkApi.page" :columns="columns" :tool-button="false">
			<template #tools> <el-button type="primary" v-auth="'friendLink:add'" icon="ele-Plus" @click="onOpen(null)"> 新增 </el-button></template>
			<template #siteName="{ row }">
				<el-link target="_blank" type="primary" :underline="false" :href="row.link">{{ row.siteName }}</el-link>
			</template>
			<template #isIgnoreCheck="{ row }">
				{{ row.isIgnoreCheck ? '否' : '是' }}
			</template>
			<!-- <template #logo="{ row }">
				<el-avatar :src="row.logo" />
			</template> -->
			<template #logo="{ row }">
				<el-image
					shape="square"
					:size="100"
					fit="cover"
					:src="row.logo"
				/>
			</template>
			<template #status="scope">
				<el-tag :type="scope.row.status === 0 ? 'success' : 'danger'"> {{ scope.row.status === 0 ? '启用' : '禁用' }}</el-tag>
			</template>
			<template #action="scope">
				<el-button icon="ele-Edit" size="small" v-auth="'friendLink:edit'" text type="primary" @click="onOpen(scope.row)"> 编辑 </el-button>
				<el-popconfirm title="确认删除吗？" @confirm="onDeleteRole(scope.row.id)">
					<template #reference>
						<el-button icon="ele-Delete" size="small" v-auth="'friendLink:delete'" text type="danger"> 删除 </el-button>
					</template>
				</el-popconfirm>
			</template>
		</ProTable>
		<LinkDialog ref="linkDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="ts" name="friendLink">
import { defineAsyncComponent, reactive, ref } from 'vue';
import { ElMessage } from 'element-plus';
import FriendLinkApi from '/@/api/FriendLinkApi';
import type { UpdateFriendLinkInput } from '/@/api/models';
import { auths } from '/@/utils/authFunction';

// 引入组件
const LinkDialog = defineAsyncComponent(() => import('./dialog.vue'));
import ProTable from '/@/components/ProTable/index.vue';
import { ColumnProps } from '/@/components/ProTable/interface';

//  table实例
const tableRef = ref<InstanceType<typeof ProTable>>();
// 弹窗实例
const linkDialogRef = ref<InstanceType<typeof LinkDialog>>();
const columns = reactive<ColumnProps[]>([
	{
		type: 'index',
		label: '序号',
		width: 60,
	},
	{
		prop: 'siteName',
		label: '站点名称',
		search: { el: 'input' },
		width: 200,
	},
	{
		prop: 'logo',
		label: 'Logo',
		width: 180,
	},
	{
		prop: 'isIgnoreCheck',
		label: '互链校验',
		width: 180,
	},
	{
		prop: 'sort',
		label: '排序',
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
		isShow: auths(['friendLink:edit', 'friendLink:delete']),
	},
]);
// 打开新增标签弹窗
const onOpen = (row: UpdateFriendLinkInput | null) => {
	linkDialogRef.value?.openDialog(row);
};

// 删除角色
const onDeleteRole = async (id: number) => {
	const { succeeded } = await FriendLinkApi.delete({ id });
	if (succeeded) {
		ElMessage.success('删除成功');
		tableRef.value?.reset();
	}
};
</script>
<style scoped></style>
