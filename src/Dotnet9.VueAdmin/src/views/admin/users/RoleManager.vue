<template>
    <ElCard>
        <template #header>
            角色管理
        </template>
        <ElRow>
            <ElCol>
                <ElSpace>
                    <ElButton @click="Test()">确定</ElButton>
                    <ElButton @click="reLoad()">重新加载</ElButton>
                </ElSpace>
            </ElCol>
        </ElRow>
        <SimpleTable url="/admin/account/getlist" :condition="condition" ref="table">
            <ElTableColumn prop="id" label="Id" width="180"></ElTableColumn>
            <ElTableColumn prop="userName" label="用户名"></ElTableColumn>
        </SimpleTable>
    </ElCard>
</template>

<script lang="ts" setup>
import { ref } from 'vue';

import SimpleTable from '@/components/SimpleTable.vue';

const v = ref('')

const condition = ref({
    id: 1000
})


const Test = () => {
    condition.value.id += 1;
}

const table = ref<InstanceType<typeof SimpleTable>>();

const reLoad = () => {
    table.value?.loadData();
}


</script> 