<template>
	<div class="layout-padding" v-loading="vm.loading">
		<v-form-designer ref="vfDesgemRef">
			<!-- 自定义按钮插槽演示 -->
			<template #customToolButtons>
				<el-button type="success" link @click="saveFormJson">
					<SvgIcon name="ele-Document" />
					保存</el-button
				>
			</template>
		</v-form-designer>
	</div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import { reactive } from 'vue';
import { ref } from 'vue';
import { useRoute } from 'vue-router';
import CustomConfigApi from '/@/api/CustomConfigApi';
import { ElMessage } from 'element-plus';
import miitBus from '/@/utils/mitt';
//路由
const route = useRoute();
//表单设计实例
const vfDesgemRef = ref();

//表单数据
const vm = reactive({
	formJson: {},
	id: 0,
	loading: true,
});

// 保存表单设计
const saveFormJson = async () => {
	let formJson = vfDesgemRef.value?.getFormJson();
	if (formJson.widgetList.length === 0) {
		ElMessage.error('请设计表单');
		return;
	}
	let json = JSON.stringify(formJson);
	//替换图片上传和附件上传的api接口地址
	json = json.replace(/"uploadURL":""/g, '"uploadURL":"/api/file/upload"').replace(/"withCredentials":false/g, '"withCredentials":true');
	formJson = JSON.parse(json);
	vm.loading = true;
	const { succeeded, errors } = await CustomConfigApi.setJson({ id: vm.id, json: formJson });
	vm.loading = false;
	if (succeeded) {
		ElMessage.success('保存成功');
		miitBus.emit('onCurrentContextmenuClick', Object.assign({}, { contextMenuClickId: 1, ...route }));
	} else {
		ElMessage.error(errors);
	}
};

onMounted(async () => {
	vm.loading = true;
	const id = route.query.id as unknown;
	vfDesgemRef.value?.clearDesigner();
	if (id !== undefined) {
		vm.id = id as number;
		const { data, succeeded } = await CustomConfigApi.getJson(vm.id);
		if (succeeded) {
			vm.formJson = data!.formJson!;
			vfDesgemRef.value?.setFormJson(vm.formJson);
		}
	}
	vm.loading = false;
});
</script>
<style lang="scss" scoped>
body {
	margin: 0;
}
:deep(.form-widget-container) {
	form {
		height: auto;
		margin-bottom: 100px;
	}
}
/* :deep(.el-header.main-header) {
	display: none;
} */
:deep(.right-toolbar) {
	width: 520px !important;
}
</style>
