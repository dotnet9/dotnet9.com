<template>
    <h2 class>操作</h2>
    <router-link to="/admin/post/write">写一篇</router-link>
    <router-link to="/admin/post/write">草稿箱</router-link>
    <router-link to="/admin/post/write">标签管理</router-link>
    <router-link to="/admin/post/write">博客搬家</router-link>
    <h2 class>分类</h2>
    <router-link to="/admin/categories">编辑分类</router-link>
</template>

<script>
import { ref } from 'vue'
export default {
    name: 'left-menu',
    setup() {
        var cates = ref()

        return {
            cates
        }
    }
}
</script>

<style>
</style>