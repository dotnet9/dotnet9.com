

<template>
  <div ref="editor"></div>
</template>

<script lang="ts" setup>
import { onMounted, ref } from "vue";
const editor = ref<HTMLElement>()

onMounted(()=>{
    setTimeout(() => {
        console.log(editor.value)
    }, 1000);
})

</script>

<style>

</style>