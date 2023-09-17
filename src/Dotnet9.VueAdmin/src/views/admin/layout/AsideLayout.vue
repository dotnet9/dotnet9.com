<template>
    <transition name="fade">
        <div class="aside-mask" v-if="isCollapse" @click="maskClickHandler()"></div>
    </transition>
    <div class="aside" :class="{ 'aside-open': !isCollapse }">
        <MenuLayout></MenuLayout>
    </div>
</template>

<script lang="ts" setup>

import MenuLayout from "./MenuLayout.vue"

import { isCollapse } from "./MenuStatus"

const maskClickHandler = () => {
    isCollapse.value = !isCollapse.value
}

</script>

<style lang="scss">
.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.3s ease;
}

.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}
</style>