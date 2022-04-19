<template>
    <div class="left-menu">
        <div class="menu">
            <slot name="menu"></slot>
        </div>
        <div class="content">
            <slot name="content"></slot>
        </div>
    </div>
</template>

<script>
export default {
    name: 'left-menu-layout'
}
</script>

<style lang="scss">
.left-menu {
    display: flex;
    flex-direction: row;
    width: 100%;
   
}
.menu {
    min-width: 180px;
    // border-right: 1px solid var(--el-border-color-light);
    display: flex;
    flex-direction: column;
    padding-right: 10px;
    box-sizing: border-box;
     h2 {
        background: #b3c0d1;
        font-size: 14px;
        text-align: center;
        color: var(--el-text-color-regular);
        line-height: 2;
    }
    a {
        padding: 6px 6px;
        box-sizing: border-box;
        color: var(--el-text-color-regular);
        font-size: 13px;
        &:hover {
            color: var(--el-color-primary);
            text-decoration: underline;
        }
    }
}
.content {
    display: flex;
    flex-direction: column;
    flex: 1;
    padding-left: 10px;
    box-sizing: border-box;
}
</style>