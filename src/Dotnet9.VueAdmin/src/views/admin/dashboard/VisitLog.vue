<template>
    <el-card>
        <template #header>
            最近登录
        </template>
        <simple-table url="/admin/account/GetLoginRecord">
            <el-table-column prop="accountName" label="用户名" width="100"></el-table-column>
            <el-table-column prop="ipCity" label="访问位置" width="180"></el-table-column>
            <el-table-column prop="createTime" label="登录时间" width="180"></el-table-column>
            <el-table-column prop="browserOs" label="访问浏览器" ></el-table-column>
        </simple-table>
    </el-card>
</template>
<script setup>
import SimpleTable from '@/components/SimpleTable.vue';



</script>