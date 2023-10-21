<template>
	<div class="system-dept-container layout-padding">
		<ProTable ref="tableRef" :request-api="SysOrganizationApi.page" :pagination="false" :columns="columns" :tool-button="false">
			<template #tools>
				<el-button v-auth="'sysorganization:add'" type="primary" icon="ele-Plus" @click="onOpenDept(null)"> 新增 </el-button>
			</template>
			<template #status="scope">
				<el-tag :type="scope.row.status === 0 ? 'success' : 'danger'"> {{ scope.row.status === 0 ? '启用' : '禁用' }}</el-tag>
			</template>
			<template #action="scope">
				<el-button v-auth="'sysorganization:edit'" icon="ele-Edit" size="small" text type="primary" @click="onOpenDept(scope.row)"> 编辑 </el-button>
				<el-popconfirm title="确认删除吗？" @confirm="onDeleteOrg(scope.row.id)">
					<template #reference>
						<el-button v-auth="'sysorganization:delete'" icon="ele-Delete" size="small" text type="danger"> 删除 </el-button>
					</template>
				</el-popconfirm>
			</template>
		</ProTable>
		<DeptDialog ref="deptDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="ts" name="sysOrganization">
import { defineAsyncComponent, ref, reactive } from 'vue';
import { ElMessage } from 'element-plus';

// 引入组件
const DeptDialog = defineAsyncComponent(() => import('/@/views/system/dept/dialog.vue'));
import ProTable from '/@/components/ProTable/index.vue';
import SysOrganizationApi from '/@/api/SysOrganizationApi';
import type { UpdateOrgInput } from '/@/api/models';
import { auths } from '/@/utils/authFunction';
import type { ColumnProps } from '/@/components/ProTable/interface';

// 定义变量内容
const deptDialogRef = ref<InstanceType<typeof DeptDialog>>();
const tableRef = ref<InstanceType<typeof ProTable>>();

const columns = reactive<ColumnProps[]>([
	{
		prop: 'name',
		label: '机构名称',
		search: { el: 'input' },
		align: 'left',
	},
	{
		prop: 'code',
		label: '机构编码',
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
		isShow: auths(['sysorganization:edit', 'sysorganization:delete']),
	},
]);

// 打开新增菜单弹窗
const onOpenDept = async (row: UpdateOrgInput | null = null) => {
	await deptDialogRef.value?.openDialog(row);
};

//删除机构
const onDeleteOrg = async (id: number) => {
	const { succeeded } = await SysOrganizationApi.delete({ id });
	if (succeeded) {
		ElMessage.success('删除成功');
		tableRef.value?.reset();
	}
};
</script>
<style scoped lang="scss"></style>
