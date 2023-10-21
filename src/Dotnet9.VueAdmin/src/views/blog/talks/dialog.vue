<template>
	<div class="blog-tags-dialog-container">
		<el-dialog :title="state.dialog.title" v-model="state.dialog.isShowDialog" width="769px">
			<el-form ref="talksDialogFormRef" :rules="rules" :model="state.ruleForm" v-loading="state.dialog.loading" size="default" label-width="90px">
				<el-row :gutter="35">
					<el-col class="mb20" :xs="24" :sm="8" :md="8" :lg="8" :xl="8">
						<el-form-item label="置顶" prop="isTop">
							<el-switch
								v-model="state.ruleForm.isTop"
								:active-value="true"
								:inactive-value="false"
								inline-prompt
								active-text="是"
								inactive-text="否"
							></el-switch>
						</el-form-item>
					</el-col>
					<el-col :xs="24" :sm="8" :md="8" :lg="8" :xl="8" class="mb20">
						<el-form-item label="动态状态" prop="status">
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
					<el-col class="mb20" :xs="24" :sm="8" :md="8" :lg="8" :xl="8">
						<el-form-item prop="isAllowComments">
							<template #label>
								<el-tooltip content="启用后用户可以对博文进行评论" placement="left"> 评论 </el-tooltip>
							</template>
							<el-switch
								v-model="state.ruleForm.isAllowComments"
								:active-value="true"
								:inactive-value="false"
								inline-prompt
								active-text="允许"
								inactive-text="禁止"
							></el-switch>
						</el-form-item>
					</el-col>
					<el-col>
						<el-form-item label="图片">
							<UploadImgs v-model:file-list="state.images" width="250px">
								<template #empty>
									<el-icon><Picture /></el-icon>
									<span>请上传照片</span>
								</template>
							</UploadImgs>
						</el-form-item>
					</el-col>
					<el-col>
						<el-form-item label="内容" prop="content">
							<div style="border: 1px solid #ccc; height: 100%; width: 100%">
								<Toolbar style="border-bottom: 1px solid #ccc" :editor="editorRef" :defaultConfig="state.toolbarConfig" :mode="state.mode" />
								<Editor
									style="height: fit-content; min-height: 300px; overflow-y: scroll"
									:defaultConfig="state.editorConfig"
									:mode="state.mode"
									v-model="state.ruleForm.content"
									@onCreated="onCreated"
								/>
							</div>
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

<script setup lang="ts" name="tagDialog">
import '@wangeditor/editor/dist/css/style.css';
import type { FormInstance, FormRules } from 'element-plus';
import { reactive, ref, nextTick, shallowRef, onBeforeUnmount } from 'vue';
import UploadImgs from '/@/components/Upload/Imgs.vue';
import type { UpdateTalksInput } from '/@/api/models';
import TalksApi from '/@/api/TalksApi';
import { Editor, Toolbar } from '@wangeditor/editor-for-vue';
import type { IDomEditor, IEditorConfig, IToolbarConfig } from '@wangeditor/editor';
import http from '/@/utils/http';
type ImageUploadType = (url: string, alt: string, href: string) => void;
type VideoUploadType = (url: string, poster: string) => void;
// 定义子组件向父组件传值/事件
const emit = defineEmits(['refresh']);

// wangEditor富文本编辑器实例
const editorRef = shallowRef<IDomEditor>();

// 表单实例
const talksDialogFormRef = ref<FormInstance>();

//表单验证
const rules = reactive<FormRules>({
	content: [{ required: true, message: '请输入内容', trigger: 'blur' }],
});

//表单状态
const state = reactive({
	ruleForm: {
		id: 0,
		status: 0,
		isAllowComments: true,
	} as UpdateTalksInput,
	images: [] as Array<any>,
	dialog: {
		isShowDialog: false,
		title: '',
		submitTxt: '',
		loading: false,
	},
	editorConfig: {
		placeholder: '请输入内容...',
		MENU_CONF: {
			uploadImage: {
				//富文本编辑器图片上传
				async customUpload(file: File, insertFn: ImageUploadType) {
					const data = await http.upload('/file/upload', { file });
					if (data && data.length > 0) {
						insertFn(data[0].url, '', '');
					}
				},
			},
			uploadVideo: {
				//上传视频
				async customUpload(file: File, insertFn: VideoUploadType) {
					const data = await http.upload('/file/upload', { file });
					if (data && data.length > 0) {
						// 视频url和视频封面
						insertFn(data[0].url, '');
					}
				},
			},
		} as unknown,
	} as IEditorConfig, // 富文本编辑器配置
	toolbarConfig: {
		toolbarKeys: ['emotion'],
	} as IToolbarConfig, // 富文本编辑器工具栏配置
	mode: 'default', // 富文本编辑器模式
});

// 创建wangEditor富文本编辑器实例
const onCreated = (editor: any) => {
	editorRef.value = editor;
	const keys = editorRef.value?.getAllMenuKeys();
	console.log(keys);
};

// 打开弹窗
const openDialog = async (row: UpdateTalksInput | null) => {
	state.dialog.isShowDialog = true;
	state.dialog.loading = true;
	state.images = [];
	if (row != null) {
		state.ruleForm = { ...row };
		if (row.images) {
			state.images = row.images.split(',').map((item) => {
				return { name: '', url: item };
			});
		}
		state.dialog.title = '修改动态';
		state.dialog.submitTxt = '修 改';
	} else {
		state.ruleForm.id = 0;
		state.dialog.title = '新增动态';
		state.dialog.submitTxt = '新 增';
		// 重置表单
		nextTick(() => {
			talksDialogFormRef.value?.resetFields();
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
	talksDialogFormRef.value?.validate(async (v) => {
		if (v) {
			if (state.images.length > 0) {
				state.ruleForm.images = state.images.map((item) => (item as any).url).join(',');
			}
			//仅保留img标签
			state.ruleForm.content = state.ruleForm.content?.replaceAll(/<[^>]+>/g, '');
			const { succeeded } = state.ruleForm.id === 0 ? await TalksApi.add(state.ruleForm) : await TalksApi.edit(state.ruleForm);
			if (succeeded) {
				closeDialog();
				emit('refresh');
			}
		}
	});
};
// 组件销毁时，也及时销毁编辑器
onBeforeUnmount(() => {
	const editor = editorRef.value;
	if (editor == null) return;
	editor.destroy();
});
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
