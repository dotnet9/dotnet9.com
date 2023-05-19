<template>
    <div class="menu-box" style="background-color:#393d49">
        <el-scrollbar>
            <el-menu class="el-menu-vertical-demo" background-color="#393d49" active-text-color="white" text-color="#ccc"
                unique-opened :collapse="isCollapse" :collapse-transition="false" :router="true"
                :default-active="defaultActive">
                <el-menu-item index="/admin/dashboard">
                    <el-icon>
                        <Odometer />
                    </el-icon>
                    <template #title>
                        <span>控制台</span>
                    </template>
                </el-menu-item>
                <el-sub-menu index="1">
                    <template #title>
                        <el-icon>
                            <Setting />
                        </el-icon>
                        <span>常规管理</span>
                    </template>
                    <el-menu-item index="/admin/site/setting">系统设置</el-menu-item>
                    <el-menu-item index="/admin/site/file-manager">附件管理</el-menu-item>
                </el-sub-menu>
                <el-sub-menu index="/admin/blogs">
                    <template #title>
                        <ElIcon>
                            <User></User>
                        </ElIcon>
                        <span>博客</span>
                    </template>
                    <ElMenuItem index="/admin/blog/post">文章</ElMenuItem>
                    <ElMenuItem index="/admin/blog/cate">分类</ElMenuItem>
                    <ElMenuItem index="/admin/blog/tag">标签</ElMenuItem>
                    <ElMenuItem index="/admin/site/friendlink">友情链接</ElMenuItem>
                    <ElMenuItem index="/admin/site/setting">网站设置</ElMenuItem>
                </el-sub-menu>
                <el-sub-menu index="/admin/user">
                    <template #title>
                        <el-icon>
                            <User />
                        </el-icon>
                        <span>用户管理</span>
                    </template>
                    <el-menu-item index="/admin/user/user">账号管理</el-menu-item>
                    <el-menu-item index="/admin/user/role">角色组</el-menu-item>
                    <el-menu-item index="/admin/user/permission">权限</el-menu-item>
                </el-sub-menu>

                <el-sub-menu index="/admin/test">
                    <template #title>
                        测试
                    </template>
                    <ElMenuItem index="/admin/test/test-form">测试表单</ElMenuItem>
                    <ElMenuItem index="/admin/test/test-table">测试Table</ElMenuItem>
                </el-sub-menu>
            </el-menu>
        </el-scrollbar>
    </div>
</template>
<script lang="ts" setup>
import { isCollapse } from "./MenuStatus"

import { User, Setting, Odometer } from '@element-plus/icons-vue'
import { onMounted, ref } from "vue";
import { useRoute, useRouter } from "vue-router";

const defaultActive = ref('')

const route = useRoute();

onMounted(() => {
    defaultActive.value = route.path;
})

const router = useRouter();

router.afterEach((to, from, fail) => {
    if (!fail) {
        defaultActive.value = to.path
    }
})

//动态添加路由






</script>

<style lang="scss">
.menu-box {
    // max-height: 100%;
    // overflow-y: auto;
    user-select: none;

    .el-menu-item .is-active {
        background-color: var(--menu-item-active-bg);
    }

    .el-menu-vertical-demo:not(.el-menu--collapse) {
        width: 230px;
        min-height: 400px;
    }
}

.el-menu.el-menu--inline {
    padding: 8px;
    box-sizing: border-box;

    .el-menu-item.is-active {
        background-color: var(--el-color-primary);
        color: white;

    }
}

.el-sub-menu.is-active.is-opened {
    .el-menu-item {
        border-radius: 4px;
    }
}
</style>