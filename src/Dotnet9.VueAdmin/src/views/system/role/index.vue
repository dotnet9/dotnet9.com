<template>
	<div class="system-role-container layout-padding">
		<ProTable ref="tableRef" :request-api="SysRoleApi.page" :columns="columns" :tool-button="false">
			<template #tools> <el-button type="primary" v-auth="'sysrole:add'" icon="ele-Plus" @click="onOpenRole(null)"> 新增 </el-button></template>
			<template #status="scope">
				<el-tag :type="scope.row.status === 0 ? 'success' : 'danger'"> {{ scope.row.status === 0 ? '启用' : '禁用' }}</el-tag>
			</template>
			<template #action="scope">
				<el-button icon="ele-Edit" size="small" text v-auth="'sysrole:edit'" type="primary" @click="onOpenRole(scope.row)"> 编辑 </el-button>
				<el-popconfirm title="确认删除吗？" @confirm="onDeleteRole(scope.row.id)">
					<template #reference>
						<el-button icon="ele-Delete" size="small" text v-auth="'sysrole:delete'" type="danger"> 删除 </el-button>
					</template>
				</el-popconfirm>
			</template>
		</ProTable>
		<RoleDialog ref="roleDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="ts" name="sysRole">
import { defineAsyncComponent, reactive, ref } from 'vue';
import { ElMessage } from 'element-plus';
import SysRoleApi from '/@/api/SysRoleApi';
import type { UpdateSysRoleInput } from '/@/api/models';
import { auths } from '/@/utils/authFunction';

// 引入组件
const RoleDialog = defineAsyncComponent(() => import('/@/views/system/role/dialog.vue'));
import ProTable from '/@/components/ProTable/index.vue';
import { ColumnProps } from '/@/components/ProTable/interface';

//  table实例
const tableRef = ref<InstanceType<typeof ProTable>>();
// 表单实例
const roleDialogRef = ref<InstanceType<typeof RoleDialog>>();
const columns = reactive<ColumnProps[]>([
	{
		type: 'index',
		label: '序号',
		width: 60,
	},
	{
		prop: 'name',
		label: '角色名称',
		search: { el: 'input' },
		width: 200,
	},
	{
		prop: 'code',
		label: '角色标识',
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
		align: 'center',
		fixed: 'right',
		width: 150,
		isShow: auths(['sysrole:edit', 'sysrole:delete']),
	},
]);
// 打开新增角色弹窗
const onOpenRole = (row: UpdateSysRoleInput | null) => {
	roleDialogRef.value?.openDialog(row);
};

// 删除角色
const onDeleteRole = async (id: number) => {
	const { succeeded } = await SysRoleApi.delete({ id });
	if (succeeded) {
		ElMessage.success('删除成功');
		tableRef.value?.reset();
	}
};
</script>
<style scoped lang="scss"></style>
