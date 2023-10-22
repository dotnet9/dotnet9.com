<template>
	<div class="blog-link-dialog-container">
		<el-dialog :title="state.dialog.title" v-model="state.dialog.isShowDialog" width="769px">
			<el-form ref="tagDialogFormRef" :rules="rules" :model="state.ruleForm" v-loading="state.dialog.loading" size="default" label-width="90px">
				<el-row :gutter="35">
					<el-col class="mb20">
						<el-form-item label="站点名称" prop="siteName">
							<el-input v-model="state.ruleForm.siteName" maxlength="32" placeholder="请输入站点名称" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col class="mb20">
						<el-form-item label="友链地址" prop="link">
							<el-input v-model="state.ruleForm.link" maxlength="256" placeholder="请输入友链地址" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col class="mb20">
						<el-form-item label="Logo" prop="logo">
							<el-input v-model="state.ruleForm.logo" maxlength="256" placeholder="请输入Logo地址" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col class="mb20">
						<el-form-item label="排序" prop="sort">
							<el-input-number v-model="state.ruleForm.sort" controls-position="right" placeholder="请输入排序" class="w100" />
						</el-form-item>
					</el-col>
					<el-col class="mb20" v-if="!state.ruleForm.isIgnoreCheck">
						<el-form-item label="对方友链" prop="url">
							<el-input v-model="state.ruleForm.url" maxlength="256" placeholder="请输入对方友情链接页面地址" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item prop="isIgnoreCheck">
							<template #label>
								<el-tooltip content="校验对方网站是否包含自己的站点地址，无效将会禁用当前友链" placement="left"> 互链校验 </el-tooltip>
							</template>
							<el-switch
								v-model="state.ruleForm.isIgnoreCheck"
								inline-prompt
								:active-value="false"
								:inactive-value="true"
								active-text="是"
								inactive-text="否"
							></el-switch>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="友链状态" prop="status">
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
						<el-form-item label="网站描述" prop="remark">
							<el-input v-model="state.ruleForm.remark" type="textarea" placeholder="请输入角色描述" maxlength="200"></el-input>
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

<script setup lang="ts" name="friendLinkDialog">
import type { FormInstance, FormRules } from 'element-plus';
import { reactive, ref, nextTick, watch } from 'vue';
import type { UpdateFriendLinkInput } from '/@/api/models';
import FriendLinkApi from '/@/api/FriendLinkApi';

// 定义子组件向父组件传值/事件
const emit = defineEmits(['refresh']);

// 表单实例
const tagDialogFormRef = ref<FormInstance>();

//表单验证
const rules = reactive<FormRules>({
	siteName: [{ required: true, message: '请输入标签名称' }],
	link: [{ required: true, message: '请输入友链地址' }],
	logo: [
		{
			required: true,
			message: '请输入logo链接地址',
		},
	],
	sort: [{ required: true, message: '请输入排序' }],
	url: [
		{
			validator: (rule: any, value: any, callback: any) => {
				if (!state.ruleForm.isIgnoreCheck && !value) {
					callback(new Error('请输入互链地址'));
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
		id: 0,
		status: 0,
		sort: 100,
		isIgnoreCheck: true,
	} as UpdateFriendLinkInput,
	dialog: {
		isShowDialog: false,
		title: '',
		submitTxt: '',
		loading: false,
	},
});

watch(
	() => state.ruleForm.isIgnoreCheck,
	(v) => {
		if (v) {
			state.ruleForm.url = '';
		}
	}
);

// 打开弹窗
const openDialog = async (row: UpdateFriendLinkInput | null) => {
	state.dialog.isShowDialog = true;
	state.dialog.loading = true;
	if (row != null) {
		state.ruleForm = { ...row };
		state.dialog.title = '修改友链';
		state.dialog.submitTxt = '修 改';
	} else {
		state.ruleForm.id = 0;
		state.dialog.title = '新增友链';
		state.dialog.submitTxt = '新 增';
		// 重置表单
		nextTick(() => {
			tagDialogFormRef.value?.resetFields();
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
	tagDialogFormRef.value?.validate(async (v) => {
		if (v) {
			const { succeeded } = state.ruleForm.id === 0 ? await FriendLinkApi.add(state.ruleForm) : await FriendLinkApi.edit(state.ruleForm);
			if (succeeded) {
				closeDialog();
				emit('refresh');
			}
		}
	});
};

// 暴露变量
defineExpose({
	openDialog,
});
</script>

<style scoped lang="scss"></style>
