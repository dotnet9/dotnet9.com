<template>
	<div class="system-config-container layout-padding">
		<ProTable ref="tableRef" :request-api="CustomConfigApi.page" :columns="columns" :tool-button="false">
			<template #tools>
				<el-button v-auth="'customconfig:add'" type="primary" icon="ele-Plus" @click="onOpenConfig(null)"> 新增 </el-button></template
			>
			<template #status="scope">
				<el-tag :type="scope.row.status === 0 ? 'success' : 'danger'"> {{ scope.row.status === 0 ? '启用' : '禁用' }}</el-tag>
			</template>
			<template #isMultiple="{ row }">
				{{ row.isMultiple ? '多项' : '单项' }}
			</template>
			<template #action="{ row }">
				<el-button v-auth="'customconfig:edit'" icon="ele-Edit" size="small" text type="primary" @click="onOpenConfig(row)"> 编辑 </el-button>
				<el-dropdown>
					<el-button icon="ele-MoreFilled" size="small" text type="primary" style="padding-left: 12px" />
					<template #dropdown>
						<el-dropdown-menu>
							<el-dropdown-item
								v-if="auth('customconfigitem:add|customconfigitem:edit|customconfigitem:delete|customconfigitem:page')"
								icon="ele-List"
								@click="onConfigItem(row)"
							>
								配置项
							</el-dropdown-item>
							<el-dropdown-item icon="ele-BrushFilled" v-if="auth('customconfig:setjson')" @click="onDesign(row.id)" divided>
								配置设计 </el-dropdown-item
							><el-dropdown-item
								icon="ele-Document"
								divided
								@click="onGenerate(row.id)"
								v-if="isAllowGenerateOrDelete(row.allowCreationEntity) && auth('customconfig:generate')"
							>
								生成实体
							</el-dropdown-item>
							<el-dropdown-item
								icon="ele-Delete"
								v-if="isAllowGenerateOrDelete(row.allowCreationEntity) && row.isGenerate && auth('customconfig:deleteClass')"
								divided
								@click="onDeleteConfigClass(row)"
							>
								删除实体
							</el-dropdown-item>
							<el-dropdown-item icon="ele-Delete" v-if="auth('customconfig:delete')" divided @click="onDeleteConfig(row)">
								删除配置
							</el-dropdown-item>
						</el-dropdown-menu>
					</template>
				</el-dropdown>
			</template>
		</ProTable>
		<ConfigDialog ref="configDialogRef" @refresh="tableRef?.reset" />
		<RenderDialog ref="renderDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="ts" name="customConfig">
import { defineAsyncComponent, reactive, ref } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import { useRouter } from 'vue-router';
import ProTable from '/@/components/ProTable/index.vue';
import CustomConfigApi from '/@/api/CustomConfigApi';
import type { CustomConfigPageOutput } from '/@/api/models';
import type { ColumnProps } from '/@/components/ProTable/interface';
import { auths, auth } from '/@/utils/authFunction';

// 引入组件
const ConfigDialog = defineAsyncComponent(() => import('./configDialog.vue'));
const RenderDialog = defineAsyncComponent(() => import('./renderDialog.vue'));
const router = useRouter();
//  table实例
const tableRef = ref<InstanceType<typeof ProTable>>();
// 表单实例
const configDialogRef = ref<InstanceType<typeof ConfigDialog>>();
const renderDialogRef = ref<InstanceType<typeof RenderDialog>>();
const columns = reactive<ColumnProps[]>([
	{
		label: '序号',
		type: 'index',
		width: 60,
	},
	{
		prop: 'name',
		label: '配置名称',
		search: { el: 'input' },
	},
	{
		prop: 'code',
		label: '唯一编码',
		search: { el: 'input' },
	},
	{
		prop: 'status',
		label: '状态',
		width: 150,
	},
	{
		prop: 'isMultiple',
		label: '配置类别',
		width: 150,
	},
	{
		prop: 'note',
		label: '备注',
	},
	{
		prop: 'createdTime',
		label: '创建时间',
	},
	{
		prop: 'action',
		label: '操作',
		width: 150,
		isShow: auths(['customconfig:edit', 'customconfig:delete']),
	},
]);
// 打开新增/编辑配置弹窗
const onOpenConfig = (row: any) => {
	configDialogRef.value?.openDialog(row, row?.isGenerate ?? false);
};

// 配置表单设计
const onDesign = (id: number) => {
	router.push({ path: '/system/config/design', query: { id } });
};

// 编辑配置项/配置项列表
const onConfigItem = async (row: CustomConfigPageOutput) => {
	if (row.isMultiple) {
		router.push({ path: '/system/config/items', query: { id: row.id } });
	} else {
		await renderDialogRef.value?.openDialog(row.id!);
	}
};

// 生成配置类
const onGenerate = async (id: number) => {
	const { succeeded, errors } = await CustomConfigApi.generate(id);
	if (succeeded) {
		tableRef.value?.reset();
		ElMessage.success('生成成功');
	} else {
		ElMessage.error(errors);
	}
};

// 删除角色
const onDeleteConfig = async (row: CustomConfigPageOutput) => {
	ElMessageBox.confirm(`确定删除配置：【${row.name}】吗?`, '提示', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			const { succeeded } = await CustomConfigApi.delete({ id: row.id });
			if (succeeded) {
				ElMessage.success('删除成功');
				tableRef.value?.reset();
			}
		})
		.catch(() => {});
};

// 删除class文件
const onDeleteConfigClass = async (row: CustomConfigPageOutput) => {
	ElMessageBox.confirm(`确定删除配置：【${row.name}】的实体吗?`, '提示', {
		confirmButtonText: '确定',
		cancelButtonText: '取消',
		type: 'warning',
	})
		.then(async () => {
			const { succeeded } = await CustomConfigApi.deleteClass(row.id!);
			if (succeeded) {
				ElMessage.success('删除成功');
				tableRef.value?.reset();
			}
		})
		.catch(() => {});
};

// 是否显示生成实体或删除实体按钮
const isAllowGenerateOrDelete = (allow: boolean) => {
	return allow && import.meta.env.DEV;
};
</script>
<style scoped lang="scss"></style>
