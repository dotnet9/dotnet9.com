<template>
	<div class="blog-category-container layout-padding">
		<ProTable ref="tableRef" :request-api="CategoryApi.page" :pagination="false" :columns="columns" :tool-button="false">
			<template #tools>
				<el-button v-auth="'category:add'" type="primary" icon="ele-Plus" @click="onOpenDialog(null)"> 新增 </el-button>
			</template>
			<template #status="scope">
				<el-tag :type="scope.row.status === 0 ? 'success' : 'danger'"> {{ scope.row.status === 0 ? '启用' : '禁用' }}</el-tag>
			</template>
			<template #action="scope">
				<el-button v-auth="'category:edit'" icon="ele-Edit" size="small" text type="primary" @click="onOpenDialog(scope.row)"> 编辑 </el-button>
				<el-popconfirm title="确认删除吗？" @confirm="onDelete(scope.row.id)">
					<template #reference>
						<el-button v-auth="'category:delete'" icon="ele-Delete" size="small" text type="danger"> 删除 </el-button>
					</template>
				</el-popconfirm>
			</template>
		</ProTable>
		<CategoryDialog ref="categoryDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="ts" name="blogCategory">
import { defineAsyncComponent, ref, reactive } from 'vue';
import { ElMessage } from 'element-plus';

// 引入组件
const CategoryDialog = defineAsyncComponent(() => import('./dialog.vue'));
import ProTable from '/@/components/ProTable/index.vue';
import CategoryApi from '/@/api/CategoryApi';
import type { UpdateCategoryInput } from '/@/api/models';
import { auths } from '/@/utils/authFunction';
import type { ColumnProps } from '/@/components/ProTable/interface';

// 定义变量内容
const categoryDialogRef = ref<InstanceType<typeof CategoryDialog>>();
const tableRef = ref<InstanceType<typeof ProTable>>();

const columns = reactive<ColumnProps[]>([
	{
		prop: 'name',
		label: '栏目名称',
		search: { el: 'input' },
		align: 'left',
	},
	{
		prop: 'status',
		label: '状态',
	},
	{
		prop: 'sort',
		label: '排序',
	},
	{
		prop: 'createdTime',
		label: '创建时间',
	},
	{
		prop: 'action',
		label: '操作',
		width: 150,
		isShow: auths(['category:edit', 'category:delete']),
	},
]);

// 打开新增栏目弹窗
const onOpenDialog = async (row: UpdateCategoryInput | null = null) => {
	await categoryDialogRef.value?.openDialog(row);
};

//删除机构
const onDelete = async (id: number) => {
	const { succeeded } = await CategoryApi.delete({ id });
	if (succeeded) {
		ElMessage.success('删除成功');
		tableRef.value?.reset();
	}
};
</script>
