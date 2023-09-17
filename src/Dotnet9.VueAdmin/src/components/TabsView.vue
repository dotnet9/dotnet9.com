<template>
    <div class="v-tabs">
        <ElScrollbar ref="scrollbarRef" @scroll="onScroll">
            <ul>
                <li v-for="item in store.tableList" :key="item.fullPath"
                    :class="{ 'active': store.activePath == item.fullPath }" @click="selectPath(item.fullPath)">
                    <div>
                        {{ item.title }}
                    </div>
                    <div class="v-tabs-close">
                        <el-icon>
                            <Close @click.stop="close(item.fullPath)" v-if="!item.isFix" />
                        </el-icon>
                    </div>
                </li>
            </ul>
        </ElScrollbar>
        <div class="v-tabs-tools">
            <!-- <div>{{ scrollVaue }}</div> -->
            <div class="tools-button" @click="scrollHandler(-50)">
                <el-icon>
                    <ArrowLeft />
                </el-icon>
            </div>
            <div class="tools-button" @click="scrollHandler(50)">
                <el-icon>
                    <ArrowRight />
                </el-icon>
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { Close, ArrowLeft, ArrowRight } from '@element-plus/icons-vue'
import { onMounted, reactive, ref } from 'vue';
import { ElScrollbar } from 'element-plus'
import { useRouter } from 'vue-router';
import { useRoute } from 'vue-router';
import { useTabsViewStore } from '@/store/TabsViewStore';
import { extractPath, parseQueryParams } from '@/shared/Utils';


const store = useTabsViewStore();

const scrollbarRef = ref<InstanceType<typeof ElScrollbar>>();
const scrollVaue = ref(0);


const onScroll = (item: {
    scrollTop: number;
    scrollLeft: number;
}) => {
    scrollVaue.value = item.scrollLeft;

}

const close = (url: string) => {
    store.close(url);
    router.push({ path: store.getLastUrl })
}

const scrollHandler = (v: number) => {
    scrollbarRef.value?.setScrollLeft(scrollVaue.value + v)
}


const router = useRouter();

router.afterEach((to, from) => {
    let index = store.tableList.findIndex(a => a.fullPath == to.fullPath);
    console.log('to:' + to.fullPath)
    store.activePath = to.fullPath;
    if (index == -1) {
        store.add({
            fullPath: to.fullPath,
            isFix: false,
            title: to.meta.title ?? to.fullPath,
            componentName: to.meta.keepName
        })
    }
})

const route = useRoute();

onMounted(() => {
    store.add({
        fullPath: route.fullPath,
        isFix: false,
        title: route.meta.title ?? route.fullPath,
        componentName: route.meta.keepName
    });
    store.activePath = route.fullPath;
})

const selectPath = (path: string) => {
    console.log('切换:', path)
    if (store.activePath != path) {
        router.push({
            path: extractPath(path),
            query: parseQueryParams(path)
        })
    }
}

</script>

<style lang="scss" scoped>
.v-tabs {
    width: 100%;
    box-sizing: border-box;
    background-color: var(--el-bg-color);
    border: 1px solid var(--el-border-color-light);
    user-select: none;
    display: flex;
    flex-direction: row;
    flex-wrap: nowrap;

    .el-scrollbar {
        flex: 1;
    }

    .v-tabs-tools {
        display: flex;
        flex-direction: row;
        border-left: 1px solid var(--el-border-color-light);
        border-right: 1px solid var(--el-border-color-light);

        .tools-button {
            width: 40px;
            height: 40px;
            line-height: 40px;
            border-right: 1px solid var(--el-border-color-light);
            text-align: center;

            &:hover {
                cursor: pointer;
            }
        }
    }

    ul {
        padding: 0;
        margin: 0;
        height: 100%;
        display: flex;
        flex-direction: row;
        list-style: none;
        align-items: center;

        li {
            padding: 0 0px 0 20px;
            height: 40px;
            display: flex;
            flex-direction: row;
            justify-content: center;
            align-items: center;
            border-right: 1px solid var(--el-border-color-light);
            border-bottom: 2px solid transparent;
            color: var(--el-text-color-regular);
            // color: green;
            white-space: nowrap;

            &.active {
                border-bottom: 2px solid var(--el-color-primary);
                color: var(--el-color-primary);
            }

            &:hover {
                border-bottom: 2px solid var(--el-color-primary-light-3);
                cursor: pointer;
            }

            .v-tabs-close {
                margin-left: 4px;
                margin-right: 4px;
                display: flex;
                flex-direction: row;
                align-items: center;
                opacity: 0;
                color: red;

                .el-icon {
                    transform: scale(1);
                }
            }

            &:hover {
                .v-tabs-close {
                    opacity: 1;
                    transition: all 0.5s;
                }

                .el-icon {
                    transform: scale(1.4);
                    transition: all 0.5s;
                    cursor: pointer;
                }
            }


        }
    }
}

.el-scrollbar .el-scrollbar__wrap .el-scrollbar__view {
    white-space: nowrap;
}
</style>