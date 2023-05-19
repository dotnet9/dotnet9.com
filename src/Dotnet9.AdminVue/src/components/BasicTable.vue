<template>
    <ElRow>
        <ElCol>
            <ElTable v-loading="props.loading" :data="props.data" v-bind="table">
                <slot></slot>
            </ElTable>
        </ElCol>
    </ElRow>
    <ElRow>
        <ElCol>
            <ElPagination v-bind="pagination" :total="paginaModel.total" v-model:current-page="paginaModel.index"
                v-model:page-size="paginaModel.size" />
        </ElCol>
    </ElRow>
</template>

<script lang="ts" setup>
import { table, pagination, BasePageModel } from '@/shared/ElConfig'
import { reactive, watch } from 'vue';

const props = defineProps({
    total: Number,
    index: Number,
    loading: Boolean,
    data: {
        type: Array
    },
    pageSize: Number
})

const emit = defineEmits<{
    (e: 'page-change', index: number, size: number): void
}>()

const paginaModel = reactive({
    total: props.total ?? 0,
    index: props.index ?? 0,
    size: props.pageSize ?? pagination['page-sizes'][0]
})

const pageChange = () => {
    emit('page-change', paginaModel.index, paginaModel.size)
}

watch(() => paginaModel.index, pageChange)
watch(() => paginaModel.size, pageChange)

watch(() => props.index, () => {
    paginaModel.index = props.index ?? 0
})
watch(() => props.pageSize, () => {
    paginaModel.size = props.pageSize ?? 0
})


</script>