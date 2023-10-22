<template>
	<div class="article-operate-container layout-padding">
		<div class="card">
			<el-form ref="formRef" class="form" :model="state.form" :rules="rules" :label-width="80">
				<div class="left">
					<el-row :gutter="20" class="title">
						<el-col :span="19">
							<el-row>
								<el-col class="mb20">
									<el-form-item label="文章标题" prop="title" label-width="80">
										<el-input maxlength="128" v-model="state.form.title" placeholder="请输入文章标题" clearable></el-input>
									</el-form-item>
								</el-col>
								<el-col class="mb20">
									<el-form-item label="内容摘要" prop="summary" label-width="80">
										<el-input
											resize="none"
											v-model="state.form.summary"
											maxlength="256"
											type="textarea"
											placeholder="请输入内容摘要"
											show-word-limit
											clearable
										></el-input>
									</el-form-item>
								</el-col>
							</el-row>
						</el-col>
						<el-col :span="5">
							<el-form-item prop="cover" label-width="0">
								<UploadImg v-model:image-url="state.form.cover" height="104px">
									<template #empty>
										<el-icon><Picture /></el-icon>
										<span>请上传封面图</span>
									</template>
								</UploadImg>
							</el-form-item>
						</el-col>
					</el-row>
					<el-row class="content">
						<el-col>
							<el-form-item label="" prop="content" label-width="0" style="height: 100%; width: 100%">
								<div style="border: 1px solid #ccc; height: 100%" v-if="state.form.isHtml">
									<Toolbar style="border-bottom: 1px solid #ccc" :editor="editorRef" :defaultConfig="state.toolbarConfig" :mode="state.mode" />
									<Editor
										style="height: fit-content; min-height: 300px; overflow-y: scroll"
										v-model="state.form.content"
										:defaultConfig="state.editorConfig"
										:mode="state.mode"
										@onCreated="onCreated"
									/>
								</div>
								<mavon-editor
									ref="mdRef"
									v-else
									style="height: 100%; max-width: 99.5%"
									:toolbars="state.markdownToolbar"
									v-model="state.form.content"
									@imgAdd="onUploadMdImg"
									@change="onMdChange"
								/>
							</el-form-item>
						</el-col>
					</el-row>
				</div>
				<div class="right card">
					<div class="top">
						<el-row>
							<el-col class="mb20">
								<el-form-item label="发布时间" prop="publishTime">
									<el-date-picker
										v-model="state.form.publishTime"
										type="datetime"
										format="YYYY-MM-DD HH:mm:ss"
										value-format="YYYY-MM-DD HH:mm:ss"
										class="w100"
										placeholder="请选择发布时间"
										clearable
									/>
								</el-form-item>
							</el-col>
							<el-col class="mb20">
								<el-form-item prop="expiredTime">
									<template #label>
										<el-tooltip content="过期后文章将不显示" placement="left"> 过期时间 </el-tooltip>
									</template>
									<el-date-picker
										v-model="state.form.expiredTime"
										type="datetime"
										format="YYYY-MM-DD HH:mm:ss"
										value-format="YYYY-MM-DD HH:mm:ss"
										class="w100"
										placeholder="请选择过期时间"
										clearable
									/>
								</el-form-item>
							</el-col>
							<el-col class="mb20">
								<el-form-item label="创作类型" prop="creationType">
									<el-select v-model="state.form.creationType" placeholder="创作类型" clearable class="w100">
										<el-option label="原创" :value="0" />
										<el-option label="转载" :value="1" />
									</el-select>
								</el-form-item>
							</el-col>
							<el-col class="mb20" v-show="state.form.creationType === 1">
								<el-form-item label="来源链接" prop="link">
									<el-input maxlength="32" v-model="state.form.link" placeholder="请输入来源外链" clearable></el-input>
								</el-form-item>
							</el-col>
							<el-col class="mb20">
								<el-form-item label="栏目" prop="categoryId">
									<el-tree-select
										v-model="state.form.categoryId"
										placeholder="请选择栏目"
										:data="state.categoryData"
										check-strictly
										:render-after-expand="false"
										class="w100"
										clearable
									/>
								</el-form-item>
							</el-col>
							<el-col class="mb20">
								<el-form-item label="标签" prop="tags">
									<el-select
										multiple
										v-model="state.form.tags"
										placeholder="请选择标签"
										collapse-tags
										collapse-tags-tooltip
										:max-collapse-tags="3"
										:multiple-limit="3"
										clearable
										class="w100"
									>
										<el-option v-for="item in state.tagsData" :key="item.value" :label="item.label" :value="item.value" />
									</el-select>
								</el-form-item>
							</el-col>
							<el-col class="mb20">
								<el-form-item label="作者" prop="author">
									<el-input maxlength="32" v-model="state.form.author" placeholder="请输入作者" clearable></el-input>
								</el-form-item>
							</el-col>
							<el-col class="mb20">
								<el-form-item label="排序" prop="sort">
									<el-input-number v-model="state.form.sort" controls-position="right" placeholder="请输入排序" class="w100" />
								</el-form-item>
							</el-col>
							<el-col class="mb20" :xs="24" :sm="12" :md="12" :lg="12" :xl="12">
								<el-form-item label="置顶" prop="isTop">
									<el-switch
										v-model="state.form.isTop"
										:active-value="true"
										:inactive-value="false"
										inline-prompt
										active-text="是"
										inactive-text="否"
									></el-switch>
								</el-form-item>
							</el-col>
							<el-col class="mb20" :xs="24" :sm="12" :md="12" :lg="12" :xl="12">
								<el-form-item prop="isAllowComments">
									<template #label>
										<el-tooltip content="启用后用户可以对博文进行评论" placement="left"> 评论 </el-tooltip>
									</template>
									<el-switch
										v-model="state.form.isAllowComments"
										:active-value="true"
										:inactive-value="false"
										inline-prompt
										active-text="允许"
										inactive-text="禁止"
									></el-switch>
								</el-form-item>
							</el-col>
							<el-col class="mb20">
								<el-form-item label="编辑器" prop="status">
									<el-radio-group v-model="state.form.isHtml" @change="onChangeEditor" class="ml-4">
										<el-radio :label="false">Markdown</el-radio>
										<el-radio :label="true">富文本</el-radio>
									</el-radio-group>
								</el-form-item>
							</el-col>
							<el-col class="mb20" :xs="24" :sm="12" :md="12" :lg="12" :xl="12">
								<el-form-item label="状态" prop="status">
									<el-switch
										v-model="state.form.status"
										:active-value="0"
										:inactive-value="1"
										inline-prompt
										active-text="启用"
										inactive-text="禁用"
									></el-switch>
								</el-form-item>
							</el-col>
						</el-row>
					</div>
					<div class="bottom">
						<el-button size="default" @click="onCancel">取 消</el-button>
						<el-button type="primary" size="default" @click="onSave">保存</el-button>
					</div>
				</div>
			</el-form>
		</div>
	</div>
</template>

<script setup lang="ts">
import { reactive, onMounted, shallowRef, ref, onBeforeUnmount } from 'vue';
import { mavonEditor } from 'mavon-editor';
import UploadImg from '/@/components/Upload/Img.vue';
import 'mavon-editor/dist/css/index.css';
import '@wangeditor/editor/dist/css/style.css';
import html2markdown from '@notable/html2markdown';
import { Editor, Toolbar } from '@wangeditor/editor-for-vue';
import type { IDomEditor, IEditorConfig, IToolbarConfig } from '@wangeditor/editor';
import type { SelectOutput, TreeSelectOutput, UpdateArticleInput } from '/@/api/models';
import CategoryApi from '/@/api/CategoryApi';
import TagsApi from '/@/api/TagsApi';
import http from '/@/utils/http';
import { type FormRules, type FormInstance, ElMessage } from 'element-plus';
import ArticleApi from '/@/api/ArticleApi';
import { useRoute } from 'vue-router';
type ImageUploadType = (url: string, alt: string, href: string) => void;
type VideoUploadType = (url: string, poster: string) => void;
import miitBus from '/@/utils/mitt';
const route = useRoute();
// wangEditor富文本编辑器实例
const editorRef = shallowRef<IDomEditor>();
// markdown编辑器实例
const mdRef = ref();
// 表单实例
const formRef = ref<FormInstance>();

// 表单验证规则
const rules = reactive<FormRules>({
	title: [
		{
			required: true,
			message: '标题不能为空',
		},
	],
	summary: [
		{
			required: true,
			message: '摘要不能为空',
		},
	],
	cover: [
		{
			required: true,
			message: '请上传封面',
		},
	],
	creationType: [
		{
			required: true,
			message: '请选择创作类型',
			trigger: 'change',
		},
	],
	categoryId: [
		{
			required: true,
			message: '请选择栏目',
			trigger: 'change',
		},
	],
	tags: [
		{
			required: true,
			message: '请选择标签',
			trigger: 'change',
		},
	],
	author: [
		{
			required: true,
			message: '请输入作者',
		},
	],
	sort: [
		{
			required: true,
			message: '请输入排序',
		},
	],
	content: [
		{
			required: true,
			message: '请输入文章内容',
		},
	],
	link: [
		{
			validator(rule: any, value?: string, callback?: any) {
				if (state.form.creationType === 1 && !value) {
					callback(new Error('请输入文章来源链接'));
				} else {
					callback();
				}
			},
		},
	],
});
const state = reactive({
	form: {
		status: 0,
		isAllowComments: true,
		sort: 100,
		isTop: false,
		isHtml: false,
		content: ``,
		id: 0,
	} as UpdateArticleInput,
	categoryData: [] as TreeSelectOutput[],
	tagsData: [] as SelectOutput[],
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
	toolbarConfig: {} as IToolbarConfig, // 富文本编辑器工具栏配置
	mode: 'default', // 富文本编辑器模式
	markdownToolbar: {
		bold: true, // 粗体
		italic: true, // 斜体
		header: true, // 标题
		underline: true, // 下划线
		strikethrough: true, // 中划线
		mark: true, // 标记
		superscript: true, // 上角标
		subscript: true, // 下角标
		quote: true, // 引用
		ol: true, // 有序列表
		ul: true, // 无序列表
		link: true, // 链接
		imagelink: true, // 图片链接
		code: true, // code
		table: true, // 表格
		fullscreen: true, // 全屏编辑
		readmodel: true, // 沉浸式阅读
		htmlcode: true, // 展示html源码
		help: true, // 帮助
		/* 1.3.5 */
		undo: true, // 上一步
		redo: true, // 下一步
		trash: true, // 清空
		save: false, // 保存（触发events中的save事件）
		/* 1.4.2 */
		navigation: true, // 导航目录
		/* 2.1.8 */
		alignleft: true, // 左对齐
		aligncenter: true, // 居中
		alignright: true, // 右对齐
		/* 2.2.1 */
		subfield: true, // 单双栏模式
		preview: true, // 预览
	},
	mdHtml: '',
});

// 创建wangEditor富文本编辑器实例
const onCreated = (editor: any) => {
	editorRef.value = editor;
};

// markdown编辑器内容发生改变
const onMdChange = (value: string, render: string) => {
	state.mdHtml = render;
	state.form.content = value;
};

// 切换编辑器
const onChangeEditor = (v: boolean) => {
	if (v) {
		state.form.content = state.form.content.length > 0 ? state.mdHtml : state.form.content;
		state.mdHtml = '';
	} else {
		state.form.content = html2markdown(state.form.content);
	}
};
// markdown上传文件
const onUploadMdImg = async (pos: any, file: any) => {
	const fd = new FormData();
	fd.append('file', file);
	const data = await http.upload('/file/upload', fd);
	if (data && data.length > 0) {
		mdRef.value.$img2Url(pos, data[0].url);
	}
};
// 保存
const onSave = async () => {
	await formRef.value?.validate(async (v) => {
		if (v) {
			const { succeeded } = state.form.id === 0 ? await ArticleApi.add(state.form) : await ArticleApi.edit(state.form);
			if (succeeded) {
				ElMessage.success('保存成功');
				miitBus.emit('onCurrentContextmenuClick', Object.assign({}, { contextMenuClickId: 1, ...route }));
			}
		}
	});
};

// 取消
const onCancel = () => {
	miitBus.emit('onCurrentContextmenuClick', Object.assign({}, { contextMenuClickId: 1, ...route }));
};

onMounted(async () => {
	// 获取栏目和标签
	const [c, t] = await Promise.all([CategoryApi.treeSelect(), TagsApi.select()]);
	state.form.id = (route.query.id as never) ?? 0;
	if (state.form.id > 0) {
		const { data, succeeded } = await ArticleApi.detail(state.form.id);
		if (succeeded && data) {
			state.form = data as UpdateArticleInput;
		}
	}
	state.categoryData = c.data ?? [];
	state.tagsData = t.data ?? [];
});

// 组件销毁时，也及时销毁编辑器
onBeforeUnmount(() => {
	const editor = editorRef.value;
	if (editor == null) return;
	editor.destroy();
});
</script>

<style lang="scss" scoped>
.card {
	height: 100%;
	.form {
		height: inherit;
		display: flex;
		flex-direction: row;
		.left {
			min-width: 80%;
			height: inherit;
			flex: 0.8;
			padding-right: 10px;
			display: flex;
			flex-direction: column;
			justify-content: space-between;
			:deep(.upload-box) {
				width: 100%;
				.el-upload {
					width: inherit;
				}
			}
			.title {
				flex: 0.1;
			}
			.content {
				flex: 0.9;
				:deep(.w-e-full-screen-container) {
					z-index: 1000;
				}
				:deep(.markdown-body) {
					width: 100%;
				}
			}
		}
		.right {
			// width: 17%;
			height: inherit;
			flex: 0.2;
			justify-content: center;
			// .card {
			display: flex;
			flex-direction: column;
			justify-content: space-between;
			// }
			.bottom {
				text-align: center;
			}
		}
	}
}
</style>
