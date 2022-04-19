<template>
    <i class="ri-fullscreen-line" v-if="!isFullScreen" @click="openFullScreen()"></i>
    <i class="ri-fullscreen-exit-line" v-if="isFullScreen" @click="exitFullScreen()"></i>
</template>

<script lang="ts">
import { ref } from "@vue/reactivity";
import { onMounted } from "@vue/runtime-core";

export default {
    name: 'q-fullscreen',
    setup() {
        const isFullScreen = ref(false)
        const openFullScreen = () => {
            let docElm = document.documentElement
            if (docElm?.requestFullscreen) {
                docElm.requestFullscreen();
                isFullScreen.value = true;
            }
        }
        const exitFullScreen = () => {
            if (document.exitFullscreen) {
                document.exitFullscreen();
                isFullScreen.value = false;
            }
        }
        const checkFullScreen = () => {
            let doc = document.documentElement
            if (document.fullscreenElement) {
                // console.log('全屏');
                isFullScreen.value = true
            } else {
                // console.log('不是全屏');
                isFullScreen.value = false
            }
        }
        onMounted(() => {
            window.onresize = () => {
                checkFullScreen();
            }
        })
        return {
            isFullScreen,
            openFullScreen,
            exitFullScreen
        }
    }
}
</script>

<style>
</style>