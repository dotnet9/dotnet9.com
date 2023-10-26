<template>
	<div class="blog-cover-dialog-container">
		<el-dialog :title="state.dialog.title" v-model="state.dialog.isShowDialog" width="769px">
			<el-form ref="coverDialogFormRef" :rules="rules" :model="state.ruleForm" v-loading="state.dialog.loading" size="default" label-width="90px">
				<el-row :gutter="20">
					<el-col :span="18">
						<el-row>
							<el-col class="mb20">
								<el-form-item label="模块名称" prop="name">
									<el-input maxlength="32" v-model="state.ruleForm.name" placeholder="请输入模块名称" clearable></el-input>
								</el-form-item>
							</el-col>
							<el-col class="mb20">
								<el-form-item label="模块类型" prop="type">
									<el-select v-model="state.ruleForm.type" placeholder="模块类型" clearable class="w100">
										<el-option v-for="(item, index) in state.coverType" :key="index" :label="item" :value="index" />
									</el-select>
								</el-form-item>
							</el-col>
						</el-row>
					</el-col>
					<el-col :span="6">
						<el-form-item prop="cover" label-width="0">
							<UploadImg v-model:image-url="state.ruleForm.cover" height="84px">
								<template #empty>
									<el-icon><Picture /></el-icon>
									<span>请上传封面图</span>
								</template>
							</UploadImg>
						</el-form-item>
					</el-col>
				</el-row>
				<el-row :gutter="35">
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<el-form-item label="排序" prop="sort">
							<el-input-number v-model="state.ruleForm.sort" controls-position="right" placeholder="请输入排序" class="w100" />
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="可见" prop="isVisible">
							<el-switch
								v-model="state.ruleForm.isVisible"
								inline-prompt
								:active-value="true"
								:inactive-value="false"
								active-text="显示"
								inactive-text="隐藏"
							></el-switch>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="模块状态" prop="status">
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
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24">
						<el-form-item label="模块描述" prop="remark">
							<el-input v-model="state.ruleForm.remark" type="textarea" placeholder="请输入模块描述" :rows="3" maxlength="200"></el-input>
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

<script setup lang="ts" name="coverDialog">
import type { FormInstance, FormRules } from 'element-plus';
import { reactive, ref, nextTick } from 'vue';
import type { UpdateCoversInput } from '/@/api/models';
import CoversApi from '/@/api/CoversApi';
import UploadImg from '/@/components/Upload/Img.vue';

// 定义子组件向父组件传值/事件
const emit = defineEmits(['refresh']);

// 表单实例
const coverDialogFormRef = ref<FormInstance>();

//表单验证
const rules = reactive<FormRules>({
	cover: [
		{
			required: true,
			message: '请上传封面',
		},
	],
	name: [{ required: true, message: '请输入模块名称' }],
	sort: [{ required: true, message: '请输入排序' }],
});

//表单状态
const state = reactive({
	ruleForm: {
		id: 0,
		status: 0,
		sort: 100,
		isVisible: true,
	} as UpdateCoversInput,
	dialog: {
		isShowDialog: false,
		title: '',
		submitTxt: '',
		loading: false,
	},
	coverType: [] as string[],
});

// 打开弹窗
const openDialog = async (row: UpdateCoversInput | null, types: string[]) => {
	state.dialog.isShowDialog = true;
	state.dialog.loading = true;
	state.coverType = types;
	if (row != null) {
		state.ruleForm = { ...row };
		state.dialog.title = '修改模块';
		state.dialog.submitTxt = '修 改';
	} else {
		state.ruleForm.id = 0;
		state.dialog.title = '新增模块';
		state.dialog.submitTxt = '新 增';
		// 重置表单
		nextTick(() => {
			coverDialogFormRef.value?.resetFields();
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
	coverDialogFormRef.value?.validate(async (v) => {
		if (v) {
			const { succeeded } = state.ruleForm.id === 0 ? await CoversApi.add(state.ruleForm) : await CoversApi.edit(state.ruleForm);
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
