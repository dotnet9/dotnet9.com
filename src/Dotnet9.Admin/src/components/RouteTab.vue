<template>
    <div class="q-route-container">
        <div class="tab" v-for="(item,i) in 10" :key="i">
            <div>tab{{ item }}</div>
            <i class="icon ri-close-line"></i>
        </div>
    </div>
</template>

<script lang="ts">
export default {
    name: 'q-route-tab'
}
</script>

<style lang="less">
.q-route-container {
    display: flex;
    flex-direction: row;
    background: #f0f2f5;
    user-select: none;
    border-bottom: 1px solid var(--el-border-color-base);
    .tab {
        background-color: var(--el-color-white);
        margin-left: 3px;
        border-radius: 3px;
        height: 2rem;
        color: var(--el-color-primary);
        display: flex;
        flex-direction: row;
        align-items: center;
        padding: 0 10px;
        border: var(--el-border-base);
        cursor: pointer;
        .icon {
            margin-left: 8px;

            align-self: center;
            &:hover {
                background-color: var(--el-color-white);
                border-radius: 50%;
            }
        }
        &:hover {
            background-color: var(--el-color-primary-light-9);
            border-color: var(--el-color-primary-light-6);
        }
    }

    .active {
        background-color: var(--el-color-primary-light-1);
    }
}
</style>