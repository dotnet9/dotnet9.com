<template>
    <ElCard>
        <template #header>
            附件管理
        </template>
        <SimpleTable url="/admin/Resource/Get">
            <ElTableColumn label="文件名称" prop="url"></ElTableColumn>
            <ElTableColumn label="后缀名" prop="suffix"></ElTableColumn>
            <ElTableColumn label="大小" prop="sizeName"></ElTableColumn>
            <ElTableColumn label="上传人" prop="account"></ElTableColumn>
            <ElTableColumn label="上传时间" prop="createTime"></ElTableColumn>
            <ElTableColumn label="存储方式" prop="strageType">
                <template #default="scope">
                    <ElTag>{{ scope.row.storageName }}</ElTag>
                </template>
            </ElTableColumn>
        </SimpleTable>
    </ElCard>
</template>

<script lang="ts" setup>
import BasicTable from '@/components/BasicTable.vue';
import SimpleTable from '@/components/SimpleTable.vue';
import { BasePageModel } from '@/shared/ElConfig';
import { ResourceService } from "@/shared/service"
import { onMounted, reactive } from 'vue';

</script>