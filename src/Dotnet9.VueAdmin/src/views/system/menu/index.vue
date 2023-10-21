<template>
	<div class="system-menu-container layout-padding">
		<ProTable ref="tableRef" :request-api="SysMenuApi.page" :columns="columns" :pagination="false" :tool-button="false">
			<template #tools>
				<el-button v-auth="'sysmenu:add'" type="primary" icon="ele-Plus" @click="onOpenMenu"> 新增 </el-button>
			</template>
			<template #name="scope">
				<SvgIcon :name="scope.row.icon" />
				<span class="ml10">{{ $t(scope.row.name) }}</span>
			</template>
			<template #type="scope">
				<el-tag :type="scope.row.type === 0 ? '' : scope.row.type === 1 ? 'success' : 'danger'">
					{{ scope.row.type === 0 ? '目录' : scope.row.type === 1 ? '菜单' : '按钮' }}</el-tag
				>
			</template>
			<template #status="scope">
				<el-tag :type="scope.row.status === 0 ? 'success' : 'danger'"> {{ scope.row.status === 0 ? '启用' : '禁用' }}</el-tag>
			</template>
			<template #action="scope">
				<el-button v-auth="'sysmenu:edit'" icon="ele-Edit" size="small" text type="primary" @click="onOpenMenu(scope.row.id)"> 编辑 </el-button>
				<el-popconfirm title="确认删除吗？" @confirm="onDeleteMenu(scope.row.id)">
					<template #reference>
						<el-button v-auth="'sysmenu:delete'" icon="ele-Delete" size="small" text type="danger"> 删除 </el-button>
					</template>
				</el-popconfirm>
			</template>
		</ProTable>
		<MenuDialog ref="menuDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="ts" name="sysMenu">
import { defineAsyncComponent, ref, reactive } from 'vue';
import { ElMessage } from 'element-plus';
import SysMenuApi from '/@/api/SysMenuApi';
import { auths } from '/@/utils/authFunction';
import ProTable from '/@/components/ProTable/index.vue';
import { ColumnProps } from '/@/components/ProTable/interface';

// 引入组件
const MenuDialog = defineAsyncComponent(() => import('/@/views/system/menu/dialog.vue'));
//table组件实例
const tableRef = ref<InstanceType<typeof ProTable>>();
const columns = reactive<ColumnProps[]>([
	{
		prop: 'name',
		label: '菜单名称',
		align: 'left',
		search: {
			el: 'input',
		},
	},
	{ prop: 'type', label: '类型' },
	{ prop: 'path', label: '路由地址' },
	{ prop: 'component', label: '组件路径' },
	{ prop: 'code', label: '权限标识' },
	{ prop: 'status', label: '状态' },
	{ prop: 'sort', label: '排序' },
	{ prop: 'createdTime', label: '创建时间' },
	{ prop: 'action', label: '操作', align: 'center', width: 150, fixed: 'right', isShow: auths(['sysmenu:edit', 'sysmenu:delete']) },
]);

// 定义变量内容
const menuDialogRef = ref<InstanceType<typeof MenuDialog>>();

// 打开添加编辑菜单弹窗
const onOpenMenu = async (id: number = 0) => {
	await menuDialogRef.value!.openDialog(id);
};

//删除菜单
const onDeleteMenu = async (id: number) => {
	const { succeeded, errors } = await SysMenuApi.delete({ id });
	if (succeeded) {
		ElMessage.success('删除成功');
		tableRef.value?.reset();
	} else {
		ElMessage.error(errors);
	}
};
</script>
<style scoped lang="scss"></style>
