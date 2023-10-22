<template>
	<div class="blog-category-dialog-container">
		<el-dialog :title="state.dialog.title" v-model="state.dialog.isShowDialog" width="769px">
			<el-form :rules="rules" v-loading="state.dialog.loading" ref="categoryDialogFormRef" :model="state.ruleForm" size="default" label-width="90px">
				<el-row :gutter="35">
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<el-form-item label="封面" prop="cover">
							<el-upload class="avatar-uploader" action="/api/file/upload" accept="image/*" :show-file-list="false" :on-success="onCoverSuccess">
								<img v-if="state.ruleForm.cover" :src="state.ruleForm.cover" class="avatar" />
								<el-icon v-else class="avatar-uploader-icon fa fa-plus"> </el-icon>
							</el-upload>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<el-form-item label="上级栏目" prop="parentId">
							<el-tree-select
								v-model="state.ruleForm.parentId"
								placeholder="请选择栏目"
								:data="state.categoryData"
								check-strictly
								:render-after-expand="false"
								class="w100"
								clearable
							/>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="栏目名称" prop="name">
							<el-input v-model="state.ruleForm.name" maxlength="23" placeholder="请输入栏目名称" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="排序" prop="sort">
							<el-input-number v-model="state.ruleForm.sort" controls-position="right" placeholder="请输入排序" class="w100" />
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="栏目状态" prop="status">
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
						<el-form-item label="备注" prop="remark">
							<el-input v-model="state.ruleForm.remark" type="textarea" placeholder="请输入栏目描述" maxlength="200"></el-input>
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

<script setup lang="ts" name="systemDeptDialog">
import { reactive, ref, nextTick } from 'vue';
import type { UpdateCategoryInput, TreeSelectOutput } from '/@/api/models';
import CategoryApi from '/@/api/CategoryApi';
import type { FormInstance, FormRules } from 'element-plus';

// 定义子组件向父组件传值/事件
const emit = defineEmits(['refresh']);

// 定义变量内容
const categoryDialogFormRef = ref<FormInstance>();
const rules = reactive<FormRules>({
	name: [
		{
			required: true,
			message: '请输入栏目名称',
		},
	],
	sort: [
		{
			required: true,
			message: '请输入排序',
		},
	],
	cover: [
		{
			validator: (rule: any, value?: string, callback?: any) => {
				if (!value) {
					callback(new Error('请上传封面'));
					return;
				}
				callback();
			},
		},
	],
});
const state = reactive({
	ruleForm: {
		id: 0,
		status: 0,
		sort: 100,
	} as UpdateCategoryInput,
	categoryData: [] as TreeSelectOutput[], // 栏目数据
	dialog: {
		isShowDialog: false,
		type: '',
		title: '',
		submitTxt: '',
		loading: true,
	},
});

// 打开弹窗
const openDialog = async (row: UpdateCategoryInput | null = null) => {
	if (row !== null) {
		state.ruleForm = { ...row };
		state.dialog.title = '修改栏目';
		state.dialog.submitTxt = '修 改';
	} else {
		state.ruleForm = {
			id: 0,
			status: 0,
			sort: 100,
		};
		state.dialog.title = '新增栏目';
		state.dialog.submitTxt = '新 增';
		nextTick(() => {
			categoryDialogFormRef.value?.resetFields();
		});
	}
	state.dialog.isShowDialog = true;
	const { data } = await CategoryApi.treeSelect();
	state.categoryData = data ?? [];
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

// 上传图片成功
const onCoverSuccess = (response: any) => {
	state.ruleForm.cover = response[0].url;
};
// 提交
const onSubmit = () => {
	categoryDialogFormRef.value!.validate(async (v) => {
		if (v) {
			const { succeeded } = state.ruleForm.id! > 0 ? await CategoryApi.edit(state.ruleForm) : await CategoryApi.add(state.ruleForm);
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
<style scoped lang="scss">
.avatar-uploader {
	.avatar {
		width: 178px;
		height: 178px;
		display: block;
	}
	:deep(.el-upload) {
		border: 1px dashed var(--el-border-color);
		border-radius: 6px;
		cursor: pointer;
		position: relative;
		overflow: hidden;
		transition: var(--el-transition-duration-fast);
		:hover {
			border-color: var(--el-color-primary);
		}
		.el-icon.avatar-uploader-icon {
			color: #8c939d;
			width: 178px;
			height: 178px;
			line-height: 178px;
			text-align: center;
		}
	}
}
</style>
