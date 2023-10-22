<template>
	<div class="system-role-dialog-container">
		<el-dialog :title="state.dialog.title" v-model="state.dialog.isShowDialog" width="769px">
			<el-form ref="roleDialogFormRef" :rules="rules" :model="state.ruleForm" v-loading="state.dialog.loading" size="default" label-width="90px">
				<el-row :gutter="35">
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="角色名称" prop="name">
							<el-input v-model="state.ruleForm.name" maxlength="32" placeholder="请输入角色名称" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="角色标识" prop="code">
							<el-input v-model="state.ruleForm.code" maxlength="32" placeholder="请输入角色标识" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="排序" prop="sort">
							<el-input-number v-model="state.ruleForm.sort" controls-position="right" placeholder="请输入排序" class="w100" />
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="角色状态" prop="status">
							<el-switch
								v-model="state.ruleForm.status"
								inline-prompt
								:active-value="0"
								:inactive-value="1"
								active-text="启"
								inactive-text="禁"
							></el-switch>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<el-form-item label="角色描述" prop="remark">
							<el-input v-model="state.ruleForm.remark" type="textarea" placeholder="请输入角色描述" maxlength="200"></el-input>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<el-form-item label="菜单权限" prop="menus">
							<el-tree
								ref="treeRef"
								node-key="value"
								:data="state.menuData"
								:props="{ children: 'children', label: 'label', class: treeNodeClass }"
								show-checkbox
								highlight-current
								default-expand-all
								class="menu-data-tree"
							/>
						</el-form-item>
					</el-col>
				</el-row>
			</el-form>
			<template #footer>
				<span class="dialog-footer">
					<el-button @click="onCancel" size="default">取 消</el-button>
					<el-button type="primary" @click="onSubmit" size="default">{{ state.dialog.submitTxt }}</el-button>
				</span>
			</template>
		</el-dialog>
	</div>
</template>

<script setup lang="ts" name="systemRoleDialog">
import { FormInstance, FormRules, ElTree } from 'element-plus';
import { reactive, ref, nextTick } from 'vue';
import { TreeSelectOutput, UpdateSysRoleInput } from '/@/api/models';
import SysRoleApi from '/@/api/SysRoleApi';
import SysMenuApi from '/@/api/SysMenuApi';

// 定义子组件向父组件传值/事件
const emit = defineEmits(['refresh']);

// 表单实例
const roleDialogFormRef = ref<FormInstance>();

//树形控件
const treeRef = ref<InstanceType<typeof ElTree>>();

//表单验证
const rules = reactive<FormRules>({
	name: [{ required: true, message: '请输入角色名称' }],
	code: [{ required: true, message: '请输入角色标识' }],
	sort: [{ required: true, message: '请输入排序' }],
	menus: [
		{
			validator: (rule: any, value?: number[], callback?: any) => {
				if ((value ?? []).length === 0) {
					callback(new Error('请为角色分配权限'));
					return;
				}
				callback();
			},
		},
	],
});

//表单状态
const state = reactive({
	ruleForm: {
		status: 0,
		sort: 100,
	} as UpdateSysRoleInput,
	menuData: [] as TreeSelectOutput[],
	dialog: {
		isShowDialog: false,
		title: '',
		submitTxt: '',
		loading: false,
	},
});

// 打开弹窗
const openDialog = async (row: UpdateSysRoleInput | null) => {
	state.dialog.isShowDialog = true;
	state.dialog.loading = true;
	const { data: menus } = await SysMenuApi.getTreeMenuButton();
	state.menuData = menus ?? [];
	if (row != null) {
		state.ruleForm = { ...row };
		const { data } = await SysRoleApi.getRuleMenu(row.id!);
		state.ruleForm.menus = data ?? [];
		state.dialog.title = '修改角色';
		state.dialog.submitTxt = '修 改';
		treeRef.value?.setCheckedKeys(data ?? []);
	} else {
		state.ruleForm.id = 0;
		state.dialog.title = '新增角色';
		state.dialog.submitTxt = '新 增';
		// 重置表单
		nextTick(() => {
			roleDialogFormRef.value?.resetFields();
		});
	}
	state.dialog.loading = false;
};
// 关闭弹窗
const closeDialog = () => {
	state.dialog.isShowDialog = false;
};
// 取消
const onCancel = () => {
	closeDialog();
};
// 提交
const onSubmit = async () => {
	state.ruleForm.menus = treeRef.value!.getCheckedKeys() as number[];
	roleDialogFormRef.value?.validate(async (v) => {
		if (v) {
			const { succeeded } = state.ruleForm.id === 0 ? await SysRoleApi.add(state.ruleForm) : await SysRoleApi.edit(state.ruleForm);
			if (succeeded) {
				closeDialog();
				emit('refresh');
			}
		}
	});
};

// 叶子节点同行显示样式
const treeNodeClass = (node: TreeSelectOutput) => {
	let addClass = true; // 添加叶子节点同行显示样式
	// debugger
	for (const key in node.children ?? []) {
		// 如果存在子节点非叶子节点，不添加样式
		if (node.children![key].children?.length ?? 0 > 0) {
			addClass = false;
			break;
		}
	}
	return addClass ? 'penultimate-node' : '';
};

// 暴露变量
defineExpose({
	openDialog,
});
</script>

<style scoped lang="scss">
.system-role-dialog-container {
	.menu-data-tree {
		width: 100%;
		border: 1px solid var(--el-border-color);
		border-radius: var(--el-input-border-radius, var(--el-border-radius-base));
		padding: 5px;
	}
}
:deep(.penultimate-node) {
	.el-tree-node__children {
		padding-left: 40px;
		white-space: pre-wrap;
		line-height: 100%;

		.el-tree-node {
			display: inline-block;
		}

		.el-tree-node__content {
			padding-left: 5px !important;
			padding-right: 5px;

			// .el-tree-node__expand-icon {
			// 	display: none;
			// }
		}
	}
}
</style>
