<template>
	<div class="system-custom-config-dialog-container">
		<el-dialog :title="state.dialog.title" v-model="state.dialog.isShowDialog" width="769px">
			<el-form ref="configDialogFormRef" :rules="rules" :model="state.ruleForm" v-loading="state.dialog.loading" size="default" label-width="90px">
				<el-row :gutter="35">
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="配置名称" prop="name">
							<el-input v-model="state.ruleForm.name" maxlength="32" placeholder="请输入配置名称" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="唯一编码" prop="code">
							<el-input
								v-model="state.ruleForm.code"
								maxlength="32"
								placeholder="请输入配置唯一标识"
								:disabled="state.isGenerate"
								clearable
							></el-input>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="配置类别">
							<el-radio-group v-model="state.ruleForm.isMultiple">
								<el-radio :label="false">单项</el-radio>
								<el-radio :label="true">多项</el-radio>
							</el-radio-group>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="配置状态" prop="status">
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
						<el-form-item label="生成类型">
							<el-radio-group v-model="state.ruleForm.allowCreationEntity">
								<el-radio :label="true">实体</el-radio>
								<el-radio :label="false">Json</el-radio>
							</el-radio-group>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
						<el-form-item label="备注" prop="remark">
							<el-input v-model="state.ruleForm.remark" type="textarea" placeholder="请输入备注" maxlength="200"></el-input>
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
import { ElMessage, FormInstance, FormRules } from 'element-plus';
import { reactive, ref, nextTick } from 'vue';
import { UpdateCustomConfigInput } from '/@/api/models';
import CustomConfigApi from '/@/api/CustomConfigApi';

// 定义子组件向父组件传值/事件
const emit = defineEmits(['refresh']);

// 表单实例
const configDialogFormRef = ref<FormInstance>();

//表单验证
const rules = reactive<FormRules>({
	name: [
		{
			required: true,
			message: '请输入配置名称',
		},
	],
	code: [
		{
			required: true,
			validator: (rule?: any, value?: any, callback?: any) => {
				if (!value) {
					callback(new Error('请输入唯一编码'));
					return;
				}
				if (!/^[A-Z]([A-Za-z]|\d)+/g.test(value)) {
					callback(new Error('请使用帕斯卡命名法'));
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
		isMultiple: false,
		allowCreationEntity: true,
	} as UpdateCustomConfigInput,
	dialog: {
		isShowDialog: false,
		title: '',
		submitTxt: '',
		loading: false,
	},
	isGenerate: false,
});

// 打开弹窗
const openDialog = async (row: UpdateCustomConfigInput | null, isGenerate: boolean) => {
	state.dialog.isShowDialog = true;
	state.isGenerate = isGenerate;
	state.dialog.loading = true;
	if (row != null) {
		state.ruleForm = { ...row };
		state.dialog.title = '修改配置';
		state.dialog.submitTxt = '修 改';
	} else {
		state.ruleForm.id = 0;
		state.dialog.title = '新增配置';
		state.dialog.submitTxt = '新 增';
		// 重置表单
		nextTick(() => {
			configDialogFormRef.value?.resetFields();
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
	configDialogFormRef.value?.validate(async (v) => {
		if (v) {
			const { succeeded } = state.ruleForm.id === 0 ? await CustomConfigApi.add(state.ruleForm) : await CustomConfigApi.edit(state.ruleForm);
			if (succeeded) {
				ElMessage.success('保存成功');
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
