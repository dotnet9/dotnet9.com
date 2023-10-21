<template>
	<div class="custom-config-item layout-padding">
		<ProTable
			v-if="state.isShow"
			ref="tableRef"
			:columns="state.columns"
			:tool-button="false"
			:init-param="state.params"
			:request-api="CustomConfigItemApi.page"
		>
			<template #tools>
				<el-button
					v-auth="'customconfigitem:add|customconfigitem:edit|customconfigitem:delete|customconfigitem:page'"
					type="primary"
					icon="ele-Plus"
					@click="onOpenRender(0)"
				>
					新增
				</el-button></template
			>
			<template #__Status="scope">
				<el-tag :type="scope.row.__Status === 0 ? 'success' : 'danger'"> {{ scope.row.__Status === 0 ? '启用' : '禁用' }}</el-tag>
			</template>
			<template #action="{ row }">
				<el-button
					v-auth="'customconfigitem:add|customconfigitem:edit|customconfigitem:delete|customconfigitem:page'"
					icon="ele-Edit"
					size="small"
					text
					type="primary"
					@click="onOpenRender(row.__Id)"
				>
					编辑
				</el-button>
				<el-popconfirm title="确认删除吗？" @confirm="onDeleteRole(row.__Id)">
					<template #reference>
						<el-button
							v-auth="'customconfigitem:add|customconfigitem:edit|customconfigitem:delete|customconfigitem:page'"
							icon="ele-Delete"
							size="small"
							text
							type="danger"
						>
							删除
						</el-button>
					</template>
				</el-popconfirm>
			</template>
		</ProTable>
		<RenderDialog ref="renderDialogRef" @refresh="tableRef?.reset" />
	</div>
</template>

<script setup lang="tsx" name="customItemList">
import { ref, reactive, onMounted, nextTick, defineAsyncComponent } from 'vue';
import { useRoute } from 'vue-router';
import ProTable from '/@/components/ProTable/index.vue';
import CustomConfigApi from '/@/api/CustomConfigApi';
import CustomConfigItemApi from '/@/api/CustomConfigItemApi';
import type { ColumnProps } from '/@/components/ProTable/interface';
import { ElMessage } from 'element-plus';
const route = useRoute();
// 引入组件
const RenderDialog = defineAsyncComponent(() => import('./renderDialog.vue'));

// 表格实例
const tableRef = ref<InstanceType<typeof ProTable>>();

// 数据编辑弹窗实例
const renderDialogRef = ref<InstanceType<typeof RenderDialog>>();

// 页面数据状态
const state = reactive({
	columns: [
		{
			type: 'index',
			label: '序号',
			width: 60,
		},
	] as ColumnProps[],
	isShow: false,
	params: { id: route.query.id as never as number },
});

// 打开新增、编辑弹窗
const onOpenRender = async (itemId?: number) => {
	await renderDialogRef.value?.openDialog(route.query.id as never, itemId);
};

// 删除
const onDeleteRole = async (id: number) => {
	const { succeeded, errors } = await CustomConfigItemApi.delete({ id });
	if (succeeded) {
		ElMessage.success('删除成功');
		tableRef.value?.reset();
	} else {
		ElMessage.error(errors);
	}
};

onMounted(async () => {
	const { data } = await CustomConfigApi.getJson(route.query.id as never);
	const json = JSON.stringify(data!.formJson);
	const reg =
		/{"key":\d+,"type":"(input|select|date|switch|number|textarea|radio|checkbox|time|time-range|date-range|rate|color|slider|cascader|rich-editor|file-upload|picture-upload)".*?"id".*?}/g;
	const optionSting = json.match(reg)?.join(',');
	if (optionSting) {
		const options: Array<any> = JSON.parse(`[${optionSting}]`);
		options
			.filter((i) => i.type !== 'rich-editor' && i.type !== 'file-upload')
			.forEach((item) => {
				let option = item.options;
				state.columns.push({
					prop: option.name,
					label: option.label,
					render: (scope) => {
						let row = scope.row;
						let v = row[option.name];
						if (v === null || v === undefined) {
							return v;
						}
						switch (item.type) {
							case 'picture-upload':
								return <el-image shape="square" size={100} fit="cover" src={option.limit == 1 ? v : v[0]} />;
							case 'select':
							case 'checkbox':
							case 'radio':
								let options = option.optionItems;
								return option.multiple || item.type === 'checkbox'
									? (options as Array<any>)
											.filter((f) => (v as Array<any>).includes(f.value))
											.map((m) => m.label)
											.join(',')
									: (options as Array<any>).find((f) => f.value.toString() === v.toString()).label;
							case 'switch':
								let type = option.label.indexOf('启') > -1 || option.label.indexOf('禁') > -1 ? ['启用', '禁用'] : ['是', '否'];
								return v ? type[0] : type[1];
							case 'time-range':
							case 'date-range':
								return (v ?? []).join('-');
							default:
								return v;
						}
					},
				});
			});
	}
	state.columns.push(
		...[
			{ label: '状态', prop: '__Status', width: 80 },
			{
				width: 160,
				label: '创建时间',
				prop: '__CreatedTime',
			},
			{
				prop: 'action',
				label: '操作',
				width: 150,
				fixed: 'right',
			},
		]
	);
	nextTick(() => {
		state.isShow = true;
	});
});
</script>

<style scoped></style>
