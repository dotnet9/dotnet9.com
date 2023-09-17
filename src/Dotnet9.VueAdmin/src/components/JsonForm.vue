<template>
    <ElForm>
        <ElFormItem v-for="item in controls">
            
        </ElFormItem>
    </ElForm>
</template>
<script setup lang="ts">
import { XForm } from './XForm';

const props = defineProps<{ controls: XForm[] }>()


</script>