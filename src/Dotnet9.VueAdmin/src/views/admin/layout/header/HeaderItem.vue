
<template>
    <div class="header-item">
        <slot></slot>
    </div>
</template>

<script lang="ts" setup></script>

<style lang="scss">
.header-item {
    height: 100%;
    box-sizing: border-box;
    padding: 0 20px;
    display: flex;
    flex-direction: row;
    align-items: center;

    &:hover {
        background-color: var(--el-bg-color-page);
        cursor: pointer;
    }

    .el-icon {
        $w:1.2rem;
        width: $w;
        height: $w;

        svg {
            width: $w;
            height: $w;
        }
    }
}
</style>