<template>
    <ElCard>

        <template #header>
            标签
        </template>
      
        <SimpleTable url="/admin/PostTag/GetList">
            <ElTableColumn label="Id" prop="id"></ElTableColumn>
            <ElTableColumn label="标签" prop="tagName"></ElTableColumn>
            <ElTableColumn label="关联文章" prop="count"></ElTableColumn>
            <ElTableColumn label="操作">
                <template #default="scope">
                    <ElSpace>
                        <ElButton :icon="Edit"></ElButton>
                        <ElButton :icon="Delete" type="danger"></ElButton>
                    </ElSpace>
                </template>
            </ElTableColumn>
        </SimpleTable>
    </ElCard>
</template>

<script  lang="ts" setup>
import { onMounted, reactive, ref } from 'vue';
import { Edit, Delete } from '@element-plus/icons-vue';

import { PostTagService, TagDtoModel } from '@/shared/service'
import SimpleTable from '@/components/SimpleTable.vue';
import SimpleForm from '@/components/SimpleForm.vue';
import { SimpleFormModel } from '@/components/SimpleFormModel';










</script>