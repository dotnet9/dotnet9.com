<template>
    <ElRow>
        <ElCol>
            <ElTable v-loading="loading" :data="config.data" :border="true">
                <slot></slot>
            </ElTable>
        </ElCol>
    </ElRow>
    <ElRow>
        <ElCol>
            <ElSpace class="m-top-20">
                <el-pagination :page-sizes="[10, 20, 50, 100]" background layout="total,prev, pager, next,sizes"
                    :total="config.total" v-model:current-page="params.index" v-model:page-size="params.pageSize" />
                <ElButton :icon="Refresh" @click="loadData()"></ElButton>
            </ElSpace>
        </ElCol>
    </ElRow>
</template>
<script lang="ts" setup>
import { onMounted, reactive, ref, watch } from 'vue';

import { serviceOptions } from '@/shared/service'
import { Refresh } from '@element-plus/icons-vue'

const props = defineProps({
    url: String
})

const loading = ref(false)

const config = reactive({
    total: 0,
    data: []
})

const url = ref(props.url);

const params = reactive({
    index: 1,
    pageSize: 10,

})


let condition: Record<string, any> = {}

/**
 * 加载table数据
 */
const loadData = () => {
    loading.value = true;
    console.log('加载数据:' + url.value)
    if(!url.value){
        console.log('为空')
    }
    serviceOptions.axios?.get(url.value!, {
        params: {
            ...params,
            ...condition
        }
    })
        .then(res => {
            console.log(res)
            if (res.data) {
                config.total = res.data.total;
                config.data = res.data.data
            }
        }).finally(() => {
            loading.value = false;
        })
}




watch(() => params, () => {
    console.log('修改分页')
    loadData();
}, { deep: true })


/**
 * 设置参数后，重新加载table
 * @param params 参数
 */
const setConditionReload = (params: Record<string, any>) => {
    condition = {}
    condition = { ...params }
    loadData();
}


onMounted(() => {
    loadData()
})
defineExpose({
    loadData,
    setConditionReload
})

</script>

<style>
.el-pagination {
    display: inline-flex;
    margin-top: 0;
}
</style>