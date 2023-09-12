<template>
    <ElCard>
        <template #header>
            <div class="title-text">设置</div>
        </template>
        <ElTabs>
            <ElTabPane label="系统设置">
                <SiteSetting></SiteSetting>
            </ElTabPane>
            <ElTabPane label="数据备份">
                <DataBack></DataBack>
            </ElTabPane>
        </ElTabs>
    </ElCard>
</template>
<script lang="ts" setup>
import DataBack from './DataBack.vue';
import SiteSetting from './SiteSetting.vue'
import CosSetting from './CosSetting.vue'
import { onActivated, onDeactivated } from 'vue';

onDeactivated(()=>{
    console.log('setting:onDeactivated')
})

onActivated(()=>{
    console.log('setting:onActivated')
})
</script>