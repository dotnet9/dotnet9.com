<template>
    <div class="menu-box" style="background-color:#393d49">
        <!-- {{ defaultActive }} -->
        <el-scrollbar>
            <el-menu class="el-menu-vertical-demo" background-color="#393d49" active-text-color="white" text-color="#ccc"
                unique-opened :collapse="isCollapse" :collapse-transition="false" :router="true"
                :default-active="defaultActive">
                <template v-for="item in memuTree" :key="item.path">
                    <el-menu-item :index="item.url" v-if="item.children.length == 0">
                        <el-icon>
                            <component :is="item.icon"></component>
                        </el-icon>
                        <template #title>
                            <span>{{ item.title }}</span>
                        </template>
                    </el-menu-item>

                    <el-sub-menu v-if="item.children.length > 0" :index="item.url">
                        <template #title>
                            <el-icon>
                                <component :is="item.icon"></component>
                            </el-icon>
                            <span>{{ item.title }} </span>
                        </template>
                        <el-menu-item :index="child.url" v-for="child in item.children" :key="child.url">{{
                            child.title }}</el-menu-item>
                    </el-sub-menu>
                </template>


            </el-menu>
        </el-scrollbar>
    </div>
</template>
<script setup>
import { isCollapse } from "./MenuStatus"
import { useSlideMenu } from "@/shared/Utils/useSlideMenu"
import { onMounted, ref } from "vue";
import { useRoute, useRouter } from "vue-router";

const defaultActive = ref('')

const route = useRoute();

onMounted(() => {
    getActivePath(route.path)
})

const { memuTree } = useSlideMenu();

const router = useRouter();

router.afterEach((to, from, fail) => {
    if (!fail) {
        getActivePath(to.path)
    }
})

const getActivePath = (path) => {
    let rg = path.match('^/([^/]+)/([^/]+)');
    console.log('匹配出:', rg[0])
    defaultActive.value = rg[0]
}

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
</style>@/router/useSlideMenu