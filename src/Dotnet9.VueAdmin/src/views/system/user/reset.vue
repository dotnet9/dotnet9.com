<template>
	<div class="system-user-reset-container">
		<el-dialog title="重置密码" v-model="state.dialog.isShowDialog" width="769px">
			<el-form ref="pwdDialogFormRef" :rules="rules" v-loading="state.dialog.loading" :model="state.form" size="default" label-width="90px">
				<el-form-item label="密码" prop="password">
					<el-input v-model="state.form.password" type="password" placeholder="密码" maxlength="16" clearable></el-input>
				</el-form-item>
				<el-form-item label="确认密码" prop="rePassword">
					<el-input v-model="state.form.rePassword" type="password" placeholder="请再次输入密码" maxlength="16" clearable></el-input>
				</el-form-item>
			</el-form>
			<template #footer>
				<span class="dialog-footer">
					<el-button
						@click="
							() => {
								state.dialog.isShowDialog = false;
							}
						"
						size="default"
						>取 消</el-button
					>
					<el-button type="primary" @click="onSubmit" size="default">提交</el-button>
				</span>
			</template>
		</el-dialog>
	</div>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue';
import { ResetPasswordInput } from '/@/api/models';
import { ElMessage, FormInstance, FormRules } from 'element-plus';
import SysUserApi from '/@/api/SysUserApi';
type resetPasswordType = ResetPasswordInput & { rePassword: string };
const pwdDialogFormRef = ref<FormInstance>();
// 表单验证
const rules = reactive<FormRules>({
	password: [
		{ required: true, message: '请输入密码' },
		{
			min: 6,
			max: 16,
			message: '密码限制6-16个字符',
		},
	],
	rePassword: [
		{
			required: true,
			message: '请输入确认密码',
			validator: (rule: any, value?: string, callback?: any) => {
				if (state.form.password !== value) {
					callback(new Error('两次输入密码不一致'));
					return;
				}
				callback();
			},
		},
	],
});

// 表单数据
const state = reactive({
	form: {} as resetPasswordType,
	dialog: {
		isShowDialog: false,
		loading: false,
	},
});

const openDialog = (id: number) => {
	state.dialog.isShowDialog = true;
	state.form.id = id;
	pwdDialogFormRef.value?.resetFields();
};

// 提交表单
const onSubmit = () => {
	pwdDialogFormRef.value?.validate(async (v) => {
		if (!v) return;
		state.dialog.loading = true;
		const { succeeded } = await SysUserApi.resetPassword(state.form);
		state.dialog.loading = false;
		if (succeeded) {
			ElMessage.success('密码已重置');
			state.dialog.isShowDialog = false;
		}
	});
};

// 暴露方法
defineExpose({
	openDialog,
});
</script>

<style scoped></style>
