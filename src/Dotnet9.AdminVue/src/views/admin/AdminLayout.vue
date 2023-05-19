<template>
    <!-- 后台仪表盘，根目录 -->

    <div class="layout">
        <AsideLayout></AsideLayout>
        <div class="main">
            <div class="header">
                <HeaderItem @click="expandHandler()">
                    <el-icon class="expand-icon">
                        <Expand />
                    </el-icon>
                </HeaderItem>
                <ElDropdown>
                    <HeaderItem>
                        <ElIcon>
                            <User></User>
                        </ElIcon>
                    </HeaderItem>
                    <template #dropdown>
                        <ElDropdownItem @click="showChangePwd()">修改密码</ElDropdownItem>
                        <ElDropdownItem @click="LoginOut()">退出登录</ElDropdownItem>
                    </template>
                </ElDropdown>
            </div>
            <TabsView></TabsView>
            <div class="route-view">
                <RouterView #default="{ Component }">
                    <KeepAlive :include="tabStore.getKeepAliveComponentNames" :exclude="tabStore.exCludeList">
                        <component :is="Component" :key="$route.name"></component>
                    </KeepAlive>
                </RouterView>
            </div>
        </div>
    </div>
    <ChangePwd v-model="ChangePwdStatus"></ChangePwd>
</template>
<script lang="ts" setup>
import HeaderItem from './layout/header/HeaderItem.vue';
import { expandHandler, closeSlideMenu } from './layout/MenuStatus'
import { Expand, User } from '@element-plus/icons-vue'
import AsideLayout from './layout/AsideLayout.vue'
import { useRoute, useRouter } from 'vue-router'
import { nextTick, ref } from 'vue';

import { useVSetting } from '@/store/VSetting'

import { AuthService } from '@/shared/service'
import { ElLoading, ElMessage } from 'element-plus';
import ChangePwd from './users/ChangePwd.vue';

import TabsView from '@/components/TabsView.vue';
import { useTabsViewStore } from '@/store/TabsViewStore';

const tabStore = useTabsViewStore();

const tabInlcude = ref(tabStore.getKeepAliveComponentNames);


const close = (tag: string) => {
    tabInlcude.value = []
}

const ChangePwdStatus = ref(false)

const showChangePwd = () => {
    ChangePwdStatus.value = true
}


const router = useRouter();

const route = useRoute();

const setting = useVSetting();



router.afterEach((to, from, fail) => {
    closeSlideMenu();
    if (!fail) {
    }
    if (route.meta.title) {
        document.title = route.meta.title as string
    }

    nextTick(() => {
        console.log('路由跳转完成:', to.meta.title);
        document.title = route.meta.title ?? '';
    })
})


const LoginOut = () => {
    let loading = ElLoading.service({
        lock: true
    })
    AuthService.loginOut().then(() => {
        tabStore.clear();
        ElMessage.success('退出成功')
        router.replace({
            name: '登录'
        })
    }).finally(() => {
        loading.close();
    })
}

router.beforeEach(guard => {
    console.log('路由跳转之前')
})




</script>

<style lang="scss">
@import "../../../src/styles/config.scss";

@import "@/styles/aside.scss";

.app-content {
    max-height: 100%;
    overflow-y: auto;
}

.menu-box {
    max-height: calc(100% - var(--el-header-height));
    overflow-y: auto;
}

.nav-crumb {
    display: flex;
    flex-direction: row;
    align-items: center;
    flex: 1;
}



.layout {
    display: flex;
    flex-direction: row;
    height: 100%;
    max-height: 100%;
    overflow-y: auto;
    background-color: var(--el-bg-color-page);

    .logo {
        width: 100%;
        height: $header-h;
        line-height: $header-h;
        text-align: center;

        font-size: 14px;
        user-select: none;

        h1 {
            color: var(--logo-text-color);
        }
    }




    .main {
        flex: 1;
        display: flex;
        flex-direction: column;

        box-sizing: border-box;
        padding-top: $header-h;
        position: relative;

        .route-view {
            width: 100%;
            box-sizing: border-box;
            min-height: 200px;
            overflow-y: auto;
            overflow-x: auto;
            margin-top: 20px;
            padding: 10px;
        }

        .header {
            position: absolute;
            top: 0;
            right: 0;
            left: 0;
            height: $header-h;
            width: 100%;
            // box-shadow: 3px 3px 5px rgba(0, 0, 0, 0.1);
            z-index: 100;
            background-color: var(--el-bg-color);
            display: flex;
            flex-direction: row;
            justify-content: space-between;

            .expand-icon {
                font-size: 25px;
                color: var(--el-text-color-primary);
            }
        }
    }
}
</style>