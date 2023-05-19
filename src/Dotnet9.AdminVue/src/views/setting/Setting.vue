<template>
    <ElCard>
        <template #header>
            <div class="title-text">设置</div>
        </template>
        <ElTabs>
            <ElTabPane label="网站设置">
                <SiteSetting></SiteSetting>
            </ElTabPane>
            <ElTabPane label="数据备份">
                <DataBack></DataBack>
            </ElTabPane>
            <ElTabPane label="腾讯云Cos配置">
                <CosSetting></CosSetting>
            </ElTabPane>
        </ElTabs>
    </ElCard>
</template>
<script lang="ts" setup>
import DataBack from './DataBack.vue';
import SiteSetting from './SiteSetting.vue'
import CosSetting from './CosSetting.vue'
</script>