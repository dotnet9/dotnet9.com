<template>
	<div class="blog-talks-container layout-padding">
		<ProTable ref="tableRef" :request-api="TalksApi.page" :columns="columns" :tool-button="false">
			<template #tools> <el-button type="primary" v-auth="'talks:add'" icon="ele-Plus" @click="onOpen(null)"> 新增 </el-button></template>
			<template #content="{ row }">
				<div v-html="row.content" class="content"></div>
			</template>
			<template #status="scope">
				<el-tag :type="scope.row.status === 0 ? 'success' : 'danger'"> {{ scope.row.status === 0 ? '启用' : '禁用' }}</el-tag>
			</template>
			<template #isTop="{ row }">
				{{ row.isTop ? '是' : '否' }}
			</template>
			<template #isAllowComments="{ row }">
				{{ row.isAllowComments ? '是' : '否' }}
			</template>
			<template #action="scope">
				<el-button icon="ele-Edit" size="small" v-auth="'talks:edit'" text type="primary" @click="onOpen(scope.row)"> 编辑 </el-button>
				<el-popconfirm title="确认删除吗？" @confirm="onDeleteRole(scope.row.id)">
					<template #reference>
						<el-button icon="ele-Delete" size="small" v-auth="'talks:delete'" text type="danger"> 删除 </el-button>
					</template>
				</el-popconfirm>
			</template>
		</ProTable>
		<TalksDialog ref="talksDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="ts" name="blogTags">
import { defineAsyncComponent, reactive, ref } from 'vue';
import { ElMessage } from 'element-plus';
import TalksApi from '/@/api/TalksApi';
import type { UpdateTalksInput } from '/@/api/models';
import { auths } from '/@/utils/authFunction';

// 引入组件
const TalksDialog = defineAsyncComponent(() => import('./dialog.vue'));
import ProTable from '/@/components/ProTable/index.vue';
import { ColumnProps } from '/@/components/ProTable/interface';

//  table实例
const tableRef = ref<InstanceType<typeof ProTable>>();
// 弹窗实例
const talksDialogRef = ref<InstanceType<typeof TalksDialog>>();
const columns = reactive<ColumnProps[]>([
	{
		type: 'index',
		label: '序号',
		width: 60,
	},
	{
		prop: 'content',
		label: '内容',
		search: { el: 'input', key: 'keyword' },
	},
	{
		prop: 'isTop',
		label: '置顶',
		width: 150,
	},
	{
		prop: 'isAllowComments',
		label: '允许评论',
		width: 150,
	},
	{
		prop: 'status',
		label: '状态',
		width: 150,
	},
	{
		prop: 'createdTime',
		label: '发表时间',
		width: 180,
	},
	{
		prop: 'action',
		label: '操作',
		fixed: 'right',
		width: 150,
		isShow: auths(['talks:edit', 'talks:delete']),
	},
]);
// 打开新增标签弹窗
const onOpen = (row: UpdateTalksInput | null) => {
	talksDialogRef.value?.openDialog(row);
};

// 删除角色
const onDeleteRole = async (id: number) => {
	const { succeeded } = await TalksApi.delete({ id });
	if (succeeded) {
		ElMessage.success('删除成功');
		tableRef.value?.reset();
	}
};
</script>
<style lang="scss" scoped>
:deep(.content) {
	img {
		width: 100px;
		height: 100px;
		object-fit: cover;
	}
}
</style>
