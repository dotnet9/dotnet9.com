<template>
    <h1>会员管理</h1>
    <input v-model="v" />
</template>

<script lang="ts" setup>
import { onMounted, ref } from 'vue';

const v = ref('')

onMounted(() => {
    console.log('user-onMounted')
})
</script>