<template>
	<div class="system-user-container layout-padding main-box">
		<TreeFilter ref="orgTreeRef" :request-api="SysOrganizationApi.getTreeSelect" id="value" :default-value="initParam.orgId" @change="onChangeTree" />
		<div class="table-box">
			<ProTable ref="proTableRef" :init-param="initParam" :columns="columns" :request-api="SysUserApi.page" :tool-button="false">
				<template #tools>
					<el-button v-auth="'sysuser:add'" type="primary" icon="ele-Plus" @click="onOpenUser(0)">新增</el-button>
				</template>
				<template #status="scope">
					<el-tag :type="scope.row.status === 0 ? 'success' : 'danger'"> {{ scope.row.status === 0 ? '启用' : '禁用' }}</el-tag>
				</template>
				<template #gender="scope">
					<el-tag :type="scope.row.gender === 0 ? '' : scope.row.gender === 1 ? 'success' : 'danger'">
						{{ scope.row.gender === 0 ? '男' : scope.row.gender === 1 ? '女' : '保密' }}</el-tag
					>
				</template>
				<template #action="scope">
					<el-button icon="ele-Edit" v-auth="'sysuser:edit'" size="small" text type="primary" @click="onOpenUser(scope.row.id)"> 编辑 </el-button>
					<el-dropdown v-auths="['sysuser:delete', 'sysuser:reset']">
						<el-button icon="ele-MoreFilled" size="small" text type="primary" style="padding-left: 12px" />
						<template #dropdown>
							<el-dropdown-menu>
								<el-dropdown-item
									v-if="auth('sysuser:reset')"
									icon="ele-RefreshLeft"
									@click="
										() => {
											resetDialogRef?.openDialog(scope.row.id);
										}
									"
								>
									重置密码
								</el-dropdown-item>
								<el-dropdown-item v-if="auth('sysuser:delete')" icon="ele-Delete" :divided="auth('sysuser:reset')" @click="onDeleteUser(scope.row)">
									删除账号
								</el-dropdown-item>
							</el-dropdown-menu>
						</template>
					</el-dropdown>
				</template>
			</ProTable>
		</div>
		<!-- 用新增编辑 -->
		<UserDialog ref="userDialogRef" @refresh="tableRef?.reset" />
		<!-- 密码重置 -->
		<ResetDialog ref="resetDialogRef" />
	</div>
</template>

<script setup lang="ts" name="sysUser">
import { defineAsyncComponent, reactive, ref, computed } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import ProTable from '/@/components/ProTable/index.vue';
import TreeFilter from '/@/components/TreeFilter/index.vue';
import SysUserApi from '/@/api/SysUserApi';
import SysOrganizationApi from '/@/api/SysOrganizationApi';
import { auths, auth } from '/@/utils/authFunction';
import type { ColumnProps } from '/@/components/ProTable/interface';
import type { TreeSelectOutput } from '/@/api/models';

// 引入组件
const UserDialog = defineAsyncComponent(() => import('/@/views/system/user/dialog.vue'));
const ResetDialog = defineAsyncComponent(() => import('/@/views/system/user/reset.vue'));

// 表单实例
const userDialogRef = ref<InstanceType<typeof UserDialog>>();
const resetDialogRef = ref<InstanceType<typeof ResetDialog>>();
//table实例
const tableRef = ref<InstanceType<typeof ProTable>>();
const orgTreeRef = ref<InstanceType<typeof TreeFilter>>();
//机构数据
const orgs = computed(() => orgTreeRef.value?.treeData ?? ([] as TreeSelectOutput[])); //orgTreeRef.value?.treeData ?? ([] as TreeSelectOutput[]);
const initParam = reactive<{ orgId?: number | string }>({ orgId: '' });

// 机构熟选项发生改变事件
const onChangeTree = (val?: number | string) => {
	initParam.orgId = val;
};
// 表列设置
const columns: ColumnProps[] = [
	{ type: 'index', label: '序号', width: 60 },
	{
		prop: 'account',
		label: '用户名',
		align: 'center',
		search: { el: 'input' },
	},
	{
		prop: 'name',
		label: '姓名',
		align: 'center',
		search: { el: 'input' },
	},
	{
		prop: 'nickName',
		label: '昵称',
		align: 'center',
	},
	{
		prop: 'gender',
		label: '性别',
		align: 'center',
	},
	{
		prop: 'birthday',
		label: '出生日期',
		align: 'center',
	},
	{
		prop: 'mobile',
		label: '手机号码',
		align: 'center',
		search: { el: 'input' },
	},
	{
		prop: 'status',
		label: '状态',
		align: 'center',
	},
	{
		prop: 'createdTime',
		label: '创建时间',
		align: 'center',
	},
	{
		prop: 'action',
		label: '操作',
		align: 'center',
		width: 120,
		fixed: 'right',
		isShow: auths(['sysuser:edit', 'sysuser:delete']),
	},
];

// 打开新增用户弹窗
const onOpenUser = async (id: number) => {
	await userDialogRef.value?.openDialog(id, orgs.value);
};
// 删除用户
const onDeleteUser = async (row: any) => {
	ElMessageBox.confirm(`确定删除账号：【${row.account}】?`, '提示', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			const { succeeded } = await SysUserApi.delete({ id: row.id });
			if (succeeded) {
				ElMessage.success('删除成功');
				tableRef.value?.reset();
			}
		})
		.catch(() => {});
};
</script>

<style scoped lang="scss"></style>
