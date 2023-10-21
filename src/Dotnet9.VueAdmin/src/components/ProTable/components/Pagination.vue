<template>
	<!-- 分页组件 -->
	<el-pagination
		:background="true"
		:current-page="pageable.pageNo"
		:page-size="pageable.pageSize"
		:page-sizes="pageSizes"
		:total="pageable.total"
		layout="total, sizes, prev, pager, next, jumper"
		@size-change="handleSizeChange"
		@current-change="handleCurrentChange"
		small
	></el-pagination>
</template>

<script setup lang="ts" name="Pagination">
interface Pageable {
	pageNo: number;
	pageSize: number;
	total: number;
}

interface PaginationProps {
	pageSizes: number[];
	pageable: Pageable;
	handleSizeChange: (size: number) => void;
	handleCurrentChange: (currentPage: number) => void;
}

defineProps<PaginationProps>();
</script>
