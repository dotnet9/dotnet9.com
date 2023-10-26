<template>
	<div class="blog-picture-container layout-padding main-box w100">
		<el-card class="w100">
			<el-row class="mb20">
				<el-col :span="24">
					<el-upload
						accept="image/*"
						multiple
						:show-file-list="false"
						:with-credentials="true"
						action="/api/file/upload"
						:on-success="onUploadSuccess"
						><el-button type="primary">上传图片</el-button></el-upload
					>
				</el-col>
			</el-row>
			<el-row :gutter="15" v-if="state.tableData.data.length > 0" v-loading="state.loading">
				<el-col :xs="24" :sm="12" :md="8" :lg="6" :xl="4" class="mb15" v-for="v in state.tableData.data" :key="v.id">
					<div class="img-box">
						<el-image style="width: 250px; height: 180px" :src="v.url" fit="cover" lazy />
						<div class="operate">
							<div
								class="handle-icon"
								@click="
												() => {
													imgViewVisible = true;
													imageUrl = v.url!;
												}
											"
							>
								<el-icon><ZoomIn /></el-icon>
								<span>查看</span>
							</div>
							<div class="handle-icon" @click="onDeleteImg(v.id!)">
								<el-icon><Delete /></el-icon>
								<span>删除</span>
							</div>
						</div>
					</div>
				</el-col>
			</el-row>
			<el-empty v-else description="暂无数据"></el-empty>
			<template v-if="state.tableData.data.length > 0">
				<el-pagination
					style="text-align: right"
					background
					@size-change="onHandleSizeChange"
					@current-change="onHandleCurrentChange"
					:page-sizes="[10, 20, 30]"
					:current-page="state.tableData.param.pageNo"
					:page-size="state.tableData.param.pageSize"
					layout="total, sizes, prev, pager, next, jumper"
					:total="state.tableData.total"
				>
				</el-pagination>
			</template>
		</el-card>
		<el-image-viewer v-if="imgViewVisible" @close="imgViewVisible = false" :url-list="[imageUrl]" />
	</div>
</template>

<script setup lang="ts" name="blogPictures">
import { reactive, onMounted, ref, nextTick } from 'vue';
import PicturesApi from '/@/api/PicturesApi';
import { useRoute } from 'vue-router';
import type { PicturesPageOutput } from '/@/api/models';
// 查看图片
const imgViewVisible = ref(false);
const imageUrl = ref('');
// 定义变量内容
const route = useRoute();
const state = reactive({
	tableData: {
		data: [] as PicturesPageOutput[],
		total: 99,
		loading: false,
		param: {
			pageNo: 1,
			pageSize: 30,
			id: 0,
		},
	},
	loading: false,
});

// 分页点击
const onHandleSizeChange = (val: number) => {
	state.tableData.param.pageSize = val;
};
// 分页点击
const onHandleCurrentChange = (val: number) => {
	state.tableData.param.pageNo = val;
};
//上传成功
const onUploadSuccess = async (res: any) => {
	state.loading = true;
	if (res && res.length > 0) {
		PicturesApi.add({ coverId: state.tableData.param.id, url: res[0].url });
		await loadData();
	}
	state.loading = false;
};
const onDeleteImg = async (id: number) => {
	state.loading = true;
	const { succeeded } = await PicturesApi.delete(id);
	if (succeeded) {
		await loadData();
	}
	state.loading = false;
};
const loadData = async () => {
	const { data } = await PicturesApi.page(state.tableData.param);
	state.tableData.data = data?.rows ?? [];
	state.tableData.total = data?.total ?? 0;
	nextTick(() => {});
};
// 页面加载时
onMounted(async () => {
	state.tableData.param.id = route.query.id as never;
	await loadData();
});
</script>

<style scoped lang="scss">
.img-box {
	position: relative;
	height: 180px;
	width: 250px;
	&:hover {
		cursor: pointer;
		transition: all 0.3s ease;
		box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.03);
		opacity: 1;
		box-sizing: border-box;
		:deep(img) {
			transition: all 0.3s ease;
			transform: translateZ(0) scale(1.05);
		}
		.operate {
			opacity: 1;
		}
	}
	.operate {
		position: absolute;
		top: 0;
		right: 0;
		box-sizing: border-box;
		display: flex;
		align-items: center;
		justify-content: center;
		width: 100%;
		height: 100%;
		cursor: pointer;
		background: rgb(0 0 0 / 60%);
		opacity: 0;
		transition: var(--el-transition-duration-fast);
		.handle-icon {
			display: flex;
			flex-direction: column;
			align-items: center;
			justify-content: center;
			padding: 0 6%;
			color: aliceblue;
			.el-icon {
				margin-bottom: 40%;
				font-size: 130%;
				line-height: 130%;
			}
			span {
				font-size: 85%;
				line-height: 85%;
			}
		}
	}
}
</style>
