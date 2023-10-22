<template>
	<div class="system-menu-dialog-container">
		<el-dialog :title="state.dialog.title" v-model="state.dialog.isShowDialog" width="769px">
			<el-form ref="menuDialogFormRef" v-loading="state.dialog.loading" :rules="rules" :model="state.ruleForm" size="default" label-width="80px">
				<el-row :gutter="35">
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<el-form-item label="上级菜单" prop="parentId">
							<el-tree-select
								v-model="state.ruleForm.parentId"
								aria-placeholder="请选择上级菜单"
								:data="state.menuData"
								check-strictly
								:render-after-expand="false"
								class="w100"
								clearable
							/>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="菜单类型" prop="type">
							<el-radio-group v-model="state.ruleForm.type" @change="onChangeType">
								<el-radio-button :label="0">目录</el-radio-button>
								<el-radio-button :label="1">菜单</el-radio-button>
								<el-radio-button :label="2">按钮</el-radio-button>
							</el-radio-group>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="状态" prop="status">
							<el-radio-group v-model="state.ruleForm.status">
								<el-radio :label="0">启用</el-radio>
								<el-radio :label="1">禁用</el-radio>
							</el-radio-group>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="菜单名称" prop="name">
							<el-input v-model="state.ruleForm.name" placeholder="菜单名称" clearable maxlength="32"></el-input>
						</el-form-item>
					</el-col>
					<template v-if="state.ruleForm.type !== MenuType.NUMBER_2">
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
							<el-form-item label="路由名称" prop="routeName">
								<el-input v-model="state.ruleForm.routeName" placeholder="路由中的 name 值" clearable maxlength="32"></el-input>
							</el-form-item>
						</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
							<el-form-item label="路由路径" prop="path">
								<el-input v-model="state.ruleForm.path" placeholder="路由中的 path 值" clearable maxlength="256"></el-input>
							</el-form-item>
						</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
							<el-form-item label="组件路径" prop="component">
								<el-input
									v-model="state.ruleForm.component"
									placeholder="组件路径"
									clearable
									maxlength="128"
									:disabled="state.ruleForm.type === MenuType.NUMBER_0"
								></el-input>
							</el-form-item>
						</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
							<el-form-item label="菜单图标" prop="icon">
								<IconSelector placeholder="请输入菜单图标" v-model="state.ruleForm.icon" />
							</el-form-item>
						</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
							<el-form-item label="重定向" prop="redirect">
								<el-input v-model="state.ruleForm.redirect" placeholder="请输入路由重定向" clearable maxlength="256"></el-input>
							</el-form-item>
						</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
							<el-form-item label="链接地址" prop="link">
								<el-input v-model="state.ruleForm.link" placeholder="外链/内嵌时链接地址" clearable maxlength="256"> </el-input>
							</el-form-item>
						</el-col>
					</template>
					<template v-else>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
							<el-form-item label="权限标识" prop="code">
								<el-input v-model="state.ruleForm.code" placeholder="请输入权限标识" clearable maxlength="128"></el-input>
							</el-form-item>
						</el-col>
					</template>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="菜单排序" prop="sort">
							<el-input-number v-model="state.ruleForm.sort" controls-position="right" placeholder="请输入排序" class="w100" />
						</el-form-item>
					</el-col>
					<template v-if="state.ruleForm.type !== MenuType.NUMBER_2">
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
							<el-form-item label="可见" prop="isVisible">
								<el-radio-group v-model="state.ruleForm.isVisible">
									<el-radio :label="true">显示</el-radio>
									<el-radio :label="false">隐藏</el-radio>
								</el-radio-group>
							</el-form-item>
						</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
							<el-form-item label="页面缓存" prop="isKeepAlive">
								<el-radio-group v-model="state.ruleForm.isKeepAlive">
									<el-radio :label="true">缓存</el-radio>
									<el-radio :label="false">不缓存</el-radio>
								</el-radio-group>
							</el-form-item>
						</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
							<el-form-item label="是否固定" prop="isFixed">
								<el-radio-group v-model="state.ruleForm.isFixed">
									<el-radio :label="true">固定</el-radio>
									<el-radio :label="false">不固定</el-radio>
								</el-radio-group>
							</el-form-item>
						</el-col>
						<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
							<el-form-item label="是否内嵌">
								<el-radio-group v-model="state.ruleForm.isIframe">
									<el-radio :label="true">是</el-radio>
									<el-radio :label="false">否</el-radio>
								</el-radio-group>
							</el-form-item>
						</el-col>
					</template>
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
						<el-form-item label="备注" prop="remark">
							<el-input v-model="state.ruleForm.remark" :rows="3" maxlength="200" type="textarea" placeholder="备注" />
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

<script setup lang="ts" name="systemMenuDialog">
import { defineAsyncComponent, reactive, ref, nextTick } from 'vue';
import { AvailabilityStatus, MenuType, TreeSelectOutput, UpdateSysMenuInput } from '/@/api/models';
import SysMenuApi from '/@/api/SysMenuApi';
import type { FormInstance, FormRules } from 'element-plus';

// 定义子组件向父组件传值/事件
const emit = defineEmits(['refresh']);

// 图标选择器组件
const IconSelector = defineAsyncComponent(() => import('/@/components/iconSelector/index.vue'));

// 表单实例
const menuDialogFormRef = ref<FormInstance>();
//验证规则
const rules = reactive<FormRules>({
	name: [
		{
			required: true,
			message: '请输入菜单名称',
		},
	],
	routeName: [
		{
			trigger: 'blur',
			validator: (rule: any, value: string, callback: any) => {
				if ((state.ruleForm.type !== MenuType.NUMBER_2 && value === undefined) || value === '') {
					callback(new Error('请输入路由名称'));
				} else {
					callback();
				}
			},
		},
	],
	path: [
		{
			trigger: 'blur',
			validator: (rule: any, value: string, callback: any) => {
				if ((state.ruleForm.type !== MenuType.NUMBER_2 && value === undefined) || value === '') {
					callback(new Error('请输入路由名称'));
				} else {
					callback();
				}
			},
		},
	],
	sort: [
		{
			required: true,
			message: '请输入排序',
		},
	],
	code: [
		{
			validator: (rule: any, value?: string, callback?: any) => {
				if (state.ruleForm.type === 2 && (value ?? '').trim().length === 0) {
					callback(new Error('请输入权限标识'));
					return;
				}
				callback();
			},
		},
	],
});

//表单数据
const state = reactive({
	// 参数请参考 `/src/router/route.ts` 中的 `dynamicRoutes` 路由菜单格式
	ruleForm: {
		id: 0,
		type: MenuType.NUMBER_0,
		status: AvailabilityStatus.NUMBER_0,
		isFixed: false,
		isKeepAlive: true,
		isVisible: true,
		isIframe: false,
		sort: 100,
	} as UpdateSysMenuInput,
	menuData: [] as TreeSelectOutput, // 上级菜单数据
	dialog: {
		isShowDialog: false,
		type: '',
		title: '',
		submitTxt: '',
		loading: true,
	},
});

// 打开弹窗
const openDialog = async (id: number) => {
	state.dialog.isShowDialog = true;
	state.dialog.loading = true;
	const { data } = await SysMenuApi.getTreeSelect();
	state.menuData = data ?? ([] as TreeSelectOutput);
	if (id > 0) {
		state.dialog.title = '修改菜单';
		state.dialog.submitTxt = '修 改';
		const { data } = await SysMenuApi.getMenuDetail(id!);
		state.ruleForm = data! as UpdateSysMenuInput;
	} else {
		state.ruleForm.id = 0;
		state.dialog.title = '新增菜单';
		state.dialog.submitTxt = '新 增';
		// 清空表单，此项需加表单验证才能使用
		nextTick(() => {
			menuDialogFormRef.value?.resetFields();
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
	menuDialogFormRef.value?.validate(async (v) => {
		if (v) {
			const { succeeded } = state.ruleForm.id! > 0 ? await SysMenuApi.edit(state.ruleForm) : await SysMenuApi.add(state.ruleForm);
			if (succeeded) {
				closeDialog(); // 关闭弹窗
				emit('refresh'); //父级组件刷新列表
			}
		}
	});
	return;
};

const onChangeType = (value: number) => {
	if (value === 0) {
		state.ruleForm.component = '';
	}
};

// 暴露变量
defineExpose({
	openDialog,
});
</script>
<style scoped lang="scss"></style>
