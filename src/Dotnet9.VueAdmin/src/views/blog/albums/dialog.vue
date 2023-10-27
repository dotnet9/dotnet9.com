<template>
	<div class="blog-albums-dialog-container">
		<el-dialog :title="state.dialog.title" v-model="state.dialog.isShowDialog" width="769px">
			<el-form ref="albumDialogFormRef" :rules="rules" :model="state.ruleForm" v-loading="state.dialog.loading" size="default" label-width="90px">
				<el-row :gutter="35">
					<el-col :xs="24" :sm="24" :md="24" :lg="24" :xl="24" class="mb20">
						<el-form-item label="封面" prop="cover">
							<el-upload class="avatar-uploader" action="/api/file/upload" :headers="state.uploadHeaders" accept="image/*" :show-file-list="false" :on-success="onCoverSuccess">
								<img v-if="state.ruleForm.cover" :src="state.ruleForm.cover" class="avatar" />
								<el-icon v-else class="avatar-uploader-icon fa fa-plus"> </el-icon>
							</el-upload>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="专辑名称" prop="name">
							<el-input v-model="state.ruleForm.name" maxlength="32" placeholder="请输入专辑名称" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="专辑别名" prop="slug">
							<el-input v-model="state.ruleForm.slug" maxlength="32" placeholder="请输入专辑别名" clearable></el-input>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="排序" prop="sort">
							<el-input-number v-model="state.ruleForm.sort" controls-position="right" placeholder="请输入排序" class="w100" />
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="12" :md="12" :lg="12" :xl="12" class="mb20">
						<el-form-item label="专辑状态" prop="status">
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
						<el-form-item label="专辑描述" prop="remark">
							<el-input v-model="state.ruleForm.remark" type="textarea" placeholder="请输入专辑描述" maxlength="200"></el-input>
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

<script setup lang="ts" name="albumDialog">
import type { FormInstance, FormRules } from 'element-plus';
import { reactive, ref, nextTick } from 'vue';
import { accessTokenKey, refreshAccessTokenKey } from '/@/utils/http'
import type { UpdateAlbumInput } from '/@/api/models';
import AlbumsApi from '/@/api/AlbumsApi';
import { Session } from '/@/utils/storage';

// 定义子组件向父组件传值/事件
const emit = defineEmits(['refresh']);

// 表单实例
const albumDialogFormRef = ref<FormInstance>();

//表单验证
const rules = reactive<FormRules>({
	cover: [
		{
			required: true,
			message: '请上传封面',
		},
	],
	name: [{ required: true, message: '请输入专辑名称' }],
	slug: [{ required: true, message: '请输入专辑别名' }],
	sort: [{ required: true, message: '请输入排序' }],
});

const accessToken = Session.get<string>(accessTokenKey);
const refreshAccessToken = Session.get<string>(refreshAccessTokenKey);
//表单状态
const state = reactive({
	ruleForm: {
		id: 0,
		status: 0,
		sort: 100,
	} as UpdateAlbumInput,
	dialog: {
		isShowDialog: false,
		title: '',
		submitTxt: '',
		loading: false,
	},
	uploadHeaders: {
		Authorization: `Bearer ${accessToken}`,
		'X-Authorization': `Bearer ${refreshAccessToken}`
	}
});

// 打开弹窗
const openDialog = async (row: UpdateAlbumInput | null) => {
	state.dialog.isShowDialog = true;
	state.dialog.loading = true;
	if (row != null) {
		state.ruleForm = { ...row };
		state.dialog.title = '修改专辑';
		state.dialog.submitTxt = '修 改';
	} else {
		state.ruleForm.id = 0;
		state.dialog.title = '新增专辑';
		state.dialog.submitTxt = '新 增';
		// 重置表单
		nextTick(() => {
			albumDialogFormRef.value?.resetFields();
		});
	}
	state.dialog.loading = false;
};
const onCoverSuccess = (response: any) => {
	state.ruleForm.cover = response[0].url;
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
	albumDialogFormRef.value?.validate(async (v) => {
		if (v) {
			const { succeeded } = state.ruleForm.id === 0 ? await AlbumsApi.add(state.ruleForm) : await AlbumsApi.edit(state.ruleForm);
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
