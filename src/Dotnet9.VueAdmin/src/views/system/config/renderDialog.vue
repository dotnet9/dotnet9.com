<template>
	<div class="system-render-dialog-container">
		<el-dialog title="编辑配置" v-model="state.isShowDialog" width="850px">
			<v-form-render ref="vfRenderRef" />
			<template #footer>
				<span class="dialog-footer">
					<el-button @click="onCancel" size="default">取 消</el-button>
					<el-button type="primary" @click="onSubmit" size="default">保存</el-button>
				</span>
			</template>
		</el-dialog>
	</div>
</template>

<script setup lang="ts">
import { reactive, ref, nextTick } from 'vue';
import CustomConfigApi from '/@/api/CustomConfigApi';
import CustomConfigItemApi from '/@/api/CustomConfigItemApi';
import { ElMessage } from 'element-plus';
// 定义子组件向父组件传值/事件
const emit = defineEmits(['refresh']);
// 表单渲染控件实例
const vfRenderRef = ref();
const state = reactive({
	formJson: {}, //表单渲染的json
	formData: {} as any, //表单数据绑定
	id: 0,
	itemId: 0,
	loading: false,
	isShowDialog: false,
	regex: /{"key":\d+,"type":"(file-upload|picture-upload)".*?"id".*?}/g, //匹配出json中的图片上传控件和附件上传控件
	fileOptions: [], //图片上传控件和附件上传控件
});
//取消
const onCancel = () => {
	state.isShowDialog = false;
};
//保存配置
const onSubmit = async () => {
	try {
		const formData = await vfRenderRef.value?.getFormData();
		//处理上传的附件和图片数据格式
		state.fileOptions.forEach((item: any) => {
			const field = formData[item.options.name];
			if (field && field.length > 0) {
				const urlList = field.filter((f: any) => f.response && f.response.length > 0).map((m: any) => m.response[0]);
				if (urlList.length > 0) {
					formData[item.options.name] = item.options.limit > 1 ? urlList.map((m: any) => m.url) : urlList[0].url;
				} else {
					formData[item.options.name] = null;
				}
			} else {
				formData[item.options.name] = null;
			}
		});
		debugger;
		const { succeeded, errors } =
			state.itemId === 0
				? await CustomConfigItemApi.add({ json: JSON.stringify(formData), configId: state.id })
				: await CustomConfigItemApi.edit({
						json: JSON.stringify(formData),
						configId: state.id,
						id: state.itemId,
				  });
		if (succeeded) {
			ElMessage.success('保存成功');
			state.isShowDialog = false;
			emit('refresh');
			return;
		}
		ElMessage.error(errors);
	} catch (e: any) {
		ElMessage.error(e);
	}
};
// 打开弹窗
state.formJson = {};
const openDialog = async (id: number, itemId?: number) => {
	vfRenderRef.value?.resetForm();
	state.id = id;
	state.isShowDialog = true;
	state.loading = true;
	const { data, succeeded, errors } = await CustomConfigApi.getJson(id, itemId);
	if (!succeeded) {
		ElMessage.error(errors);
		state.isShowDialog = false;
		return;
	}
	state.itemId = data?.itemId ?? 0;
	state.formJson = data!.formJson!;
	const json = JSON.stringify(data!.formJson);
	const jsonString = json.match(state.regex)?.join(',');
	state.formData = data!.dataJson ?? {};
	if (jsonString) {
		state.fileOptions = JSON.parse(`[${jsonString}]`);
	}
	if (data!.dataJson && state.fileOptions.length > 0) {
		//处理上传的附件和图片数据格式
		state.fileOptions.forEach((item: any) => {
			const field = state.formData[item.options.name];
			if (field && field.length > 0) {
				state.formData[item.options.name] =
					typeof field === 'string'
						? [
								{
									name: field.substring(field.lastIndexOf('/') + 1),
									url: field,
									response: [{ name: field.substring(field.lastIndexOf('/') + 1), url: field }],
								},
						  ]
						: field.map((m: string) => {
								return {
									name: m.substring(m.lastIndexOf('/') + 1),
									url: m,
									response: [{ name: m.substring(m.lastIndexOf('/') + 1), url: m }],
								};
						  });
			}
		});
	}
	// 渲染表单
	vfRenderRef.value?.setFormJson(state.formJson);
	nextTick(() => {
		// 绑定表单数据
		vfRenderRef.value?.setFormData(state.formData);
	});
	state.loading = false;
};

//暴露方法
defineExpose({
	openDialog,
});
</script>

<style lang="scss" scoped></style>
