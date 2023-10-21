<template>
	<div class="blog-account-container layout-padding">
		<ProTable ref="tableRef" :request-api="AuthAccountApi.page" :columns="columns" :tool-button="false">
			<template #gender="{ row }">
				{{ row.gender === 0 ? '男' : row.gender === 1 ? '女' : '未知' }}
			</template>
			<template #isBlogger="{ row }">
				<template v-if="auth('authaccount:setblogger')">
					<el-switch v-model="row.isBlogger" inline-prompt active-text="是" inactive-text="否" @change="onChange(row.id)" />
				</template>
				<template v-else>
					<el-tag :type="row.isBlogger ? 'success' : 'danger'">{{ row.isBlogger ? '是' : '否' }}</el-tag>
				</template>
			</template>
			<template #action="scope">
				<el-popconfirm title="确认删除吗？" @confirm="onDeleteRole(scope.row.id)">
					<template #reference>
						<el-button icon="ele-Delete" size="small" v-auth="'authaccount:delete'" text type="danger"> 删除 </el-button>
					</template>
				</el-popconfirm>
			</template>
		</ProTable>
	</div>
</template>

<script setup lang="ts" name="friendLink">
import { reactive, ref } from 'vue';
import { ElMessage } from 'element-plus';
import AuthAccountApi from '/@/api/AuthAccountApi';
import { auth } from '/@/utils/authFunction';

import ProTable from '/@/components/ProTable/index.vue';
import { ColumnProps } from '/@/components/ProTable/interface';
const loading = ref(false);
//  table实例
const tableRef = ref<InstanceType<typeof ProTable>>();
const columns = reactive<ColumnProps[]>([
	{
		type: 'index',
		label: '序号',
		width: 60,
	},
	{
		prop: 'name',
		label: '昵称',
		search: { el: 'input' },
	},
	{
		prop: 'gender',
		label: '性别',
	},
	{
		prop: 'type',
		label: '用户类型',
	},
	{
		prop: 'isBlogger',
		label: '博主',
	},
	{
		prop: 'createdTime',
		label: '注册时间',
		width: 180,
	},
	{
		prop: 'action',
		label: '操作',
		fixed: 'right',
		width: 150,
		isShow: auth('authaccount:delete'),
	},
]);

const onChange = async (id: number) => {
	loading.value = true;
	const { succeeded } = await AuthAccountApi.setBlogger(id);
	loading.value = false;
	if (succeeded) {
		tableRef.value?.reset();
	}
};

// 删除角色
const onDeleteRole = async (id: number) => {
	const { succeeded } = await AuthAccountApi.delete(id);
	if (succeeded) {
		ElMessage.success('删除成功');
		tableRef.value?.reset();
	}
};
</script>
<style scoped></style>
