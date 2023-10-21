<template>
	<div class="system-user-dialog-container">
		<el-dialog :title="state.dialog.title" v-model="state.dialog.isShowDialog" width="769px">
			<el-form ref="userDialogFormRef" :rules="rules" v-loading="state.dialog.loading" :model="state.ruleForm" size="default" label-width="90px">
				<el-row :gutter="35">
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="用户名" prop="account">
							<el-input v-model="state.ruleForm.account" placeholder="请输入账户名称" maxlength="32" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="姓名" prop="name">
							<el-input v-model="state.ruleForm.name" placeholder="请输入姓名" maxlength="16" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="性别" prop="gender">
							<el-radio-group v-model="state.ruleForm.gender">
								<el-radio-button :label="0">男</el-radio-button>
								<el-radio-button :label="1">女</el-radio-button>
								<el-radio-button :label="2">保密</el-radio-button>
							</el-radio-group>
						</el-form-item></el-col
					>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="昵称" prop="nickName">
							<el-input v-model="state.ruleForm.nickName" placeholder="请输入用户昵称" maxlength="32" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="出生日期" prop="birthday">
							<el-date-picker
								v-model="state.ruleForm.birthday"
								type="date"
								format="YYYY-MM-DD"
								value-format="YYYY-MM-DD"
								class="w100"
								placeholder="请选择出生日期"
								clearable
							/>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="手机号" prop="mobile">
							<el-input v-model="state.ruleForm.mobile" placeholder="请输入手机号" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="机构" prop="orgId">
							<el-tree-select
								v-model="state.ruleForm.orgId"
								placeholder="请选择机构"
								:data="state.deptData"
								check-strictly
								:render-after-expand="false"
								class="w100"
								clearable
							/>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="角色" prop="roles">
							<el-select
								multiple
								v-model="state.ruleForm.roles"
								placeholder="请选择角色"
								collapse-tags
								collapse-tags-tooltip
								:max-collapse-tags="3"
								clearable
								class="w100"
							>
								<el-option v-for="item in state.roleData" :key="item.value" :label="item.label" :value="item.value" />
							</el-select>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="邮箱" prop="email">
							<el-input v-model="state.ruleForm.email" placeholder="请输入" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="状态" prop="status">
							<el-switch
								v-model="state.ruleForm.status"
								:active-value="0"
								:inactive-value="1"
								inline-prompt
								active-text="启"
								inactive-text="禁"
							></el-switch>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
						<el-form-item label="用户描述" prop="remark">
							<el-input v-model="state.ruleForm.remark" type="textarea" placeholder="请输入用户描述" maxlength="200"></el-input>
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

<script setup lang="ts" name="systemUserDialog">
import { reactive, ref, nextTick } from 'vue';
import { UpdateSysUserInput, TreeSelectOutput } from '/@/api/models';
import SysUserApi from '/@/api/SysUserApi';
import SysRoleApi from '/@/api/SysRoleApi';
import { FormInstance, FormRules } from 'element-plus';

// 定义子组件向父组件传值/事件
const emit = defineEmits(['refresh']);

// 定义变量内容
const userDialogFormRef = ref<FormInstance>();
// 表单验证规则
const rules = reactive<FormRules>({
	name: [{ required: true, message: '请输入姓名' }],
	account: [{ required: true, message: '请输入用户名' }],
	orgId: [{ required: true, message: '请选择机构' }],
	roles: [{ required: true, message: '请分配角色' }],
	mobile: [
		{
			validator: (rule, value?: string, callback?: any) => {
				if ((value ?? '').trim().length > 0 && !/^1[3456789]\d{9}$/.test(value ?? '')) {
					callback(new Error('手机号码格式不正确'));
					return;
				}
				callback();
			},
		},
	],
	email: [
		{
			validator: (rule, value?: string, callback?: any) => {
				if ((value ?? '').trim().length > 0 && !/^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])+/.test(value ?? '')) {
					callback(new Error('邮箱码格式不正确'));
					return;
				}
				callback();
			},
		},
	],
});

// 表单状态
const state = reactive({
	ruleForm: {
		gender: 0,
		status: 0,
	} as UpdateSysUserInput,
	deptData: [] as TreeSelectOutput[], // 部门数据
	roleData: [] as TreeSelectOutput[], //角色下拉选项
	dialog: {
		isShowDialog: false,
		title: '',
		submitTxt: '',
		loading: false,
	},
});

// 打开弹窗
const openDialog = async (id: number, orgs: TreeSelectOutput[]) => {
	state.dialog.loading = true;
	state.dialog.isShowDialog = true;
	state.deptData = orgs;
	const { data } = await SysRoleApi.getRoleSelect();
	state.roleData = data ?? [];
	if (id > 0) {
		const { data: user } = await SysUserApi.getSysUserDetail(id);
		state.ruleForm = user;
		state.dialog.title = '修改用户';
		state.dialog.submitTxt = '修 改';
	} else {
		state.ruleForm.id = 0;
		state.dialog.title = '新增用户';
		state.dialog.submitTxt = '新 增';
		// 重置表单
		nextTick(() => {
			userDialogFormRef.value?.resetFields();
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
const onSubmit = () => {
	userDialogFormRef.value?.validate(async (v) => {
		if (!v) return;
		const { succeeded } = state.ruleForm.id === 0 ? await SysUserApi.add(state.ruleForm) : await SysUserApi.edit(state.ruleForm);
		if (succeeded) {
			closeDialog();
			emit('refresh');
		}
	});
};

// 暴露变量
defineExpose({
	openDialog,
});
</script>
<style scoped lang="scss"></style>
