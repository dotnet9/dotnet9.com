<template>
    <el-breadcrumb separator="/">
        <el-breadcrumb-item :to="{ path: '/' }">首页</el-breadcrumb-item>
        <el-breadcrumb-item>
            <a href="/">活动管理</a>
        </el-breadcrumb-item>
        <el-breadcrumb-item>活动列表</el-breadcrumb-item>
        <el-breadcrumb-item>活动详情</el-breadcrumb-item>
    </el-breadcrumb>
</template>

<script lang="ts">
export default {
    name: 'q-breadcrumb'
    , setup() {

    }
}
</script>

<style lang="less">
.el-breadcrumb{
    display: flex;
    flex-direction: row;
    align-items: center;
}
</style>