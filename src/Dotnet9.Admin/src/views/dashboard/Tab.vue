<template>
  <div class="tab">
    <ul>
      <li
          class="q-module-item"
          v-for="(item, i) in moduleList"
          :key="i"
          :class="item.active ? 'active' : ''"
          @click="handlerItem(item,true)"
      >
        <i v-if="item.icon" :class="item.icon"></i>
        <span>{{ item.label }}</span>
      </li>
    </ul>
  </div>
  <div class="q-menu-container" :class="[expandState ? 'q-menu-open' : 'q-menu-close']">
    <template v-for="(item, i) in moduleList" :key="i">
      <SlideMenu :sub-menu="item.subMenu" v-if="item.active"></SlideMenu>
    </template>
  </div>

  <TopBar v-model:expand="expand"></TopBar>
</template>

<script lang="ts">
import {useRouter} from 'vue-router'
import {onMounted, ref, computed, defineComponent, nextTick} from 'vue';
import SlideMenu from "../../components/SlideMenu.vue";
import {reactive} from "@vue/reactivity";
import {SubMenu, PostMenu, tagMenu} from "../../components/MenuConfig"
import {ElMessage} from 'element-plus'
import TopBar from "./TopBar.vue";
import {expandState, handlerExpand, showExpandButton} from "./ExpandState"

export interface ModuleItem {
  label: string
  index?: string
  icon?: string
  subMenu?: SubMenu[]
  active?: boolean
}

export default defineComponent({
  name: 'tab',
  components: {SlideMenu, TopBar},
  props: {
    expand: Boolean
  },
  emits: ['update:expand'],
  setup(props) {
    const router = useRouter();
    const isOpen = computed(() => {
      return props.expand
    })
    const activePath = ref('')
    onMounted(() => {
      activePath.value = router.currentRoute.value.path
      nextTick(() => {
        updateSelectStatus();
      })
    })
    router.afterEach(() => {
      activePath.value = router.currentRoute.value.path
      nextTick(() => {
        updateSelectStatus();
      })
    })
    const moduleList = reactive<ModuleItem[]>([
      {
        label: '仪表盘',
        active: true,
        index: '/admin/dash',
        icon: 'ri-dashboard-2-line'
      },
      {
        label: '文章',
        icon: 'ri-article-line',
        subMenu: PostMenu
      },
      {
        label: '评论',
        icon: 'ri-message-3-line'
      },
      {
        label: '标签',
        index: '/admin/tag',
        icon: 'ri-price-tag-3-line',
        subMenu: tagMenu

      },
      {
        label: '友链',
        icon: 'ri-links-line',
        index: '/admin/friendlink'
      },
      {
        label: '用户', icon: 'ri-user-line'
      },
      {
        label: '设置',
        index: '/admin/setting',
        icon: 'ri-settings-3-line'
      }
    ])

    const handlerItem = (item: ModuleItem, updateRouter: boolean) => {
      moduleList.forEach(temp => {
        temp.active = temp == item;
        if (temp.active) {
          if (!temp.subMenu || temp.subMenu.length == 0) {
            handlerExpand(false)
            showExpandButton.value = false
          } else {
            handlerExpand(true)
          }
          if (updateRouter) {
            let path = '';
            if (temp.subMenu && temp.subMenu.length > 0) {
              path = temp.subMenu[0].children?.[0]?.index!;
              showExpandButton.value = true
            } else {
              path = temp.index!
              showExpandButton.value = false
            }
            if (path) {
              router.replace(path)
            } else {
              ElMessage({
                message: '跳转路径不存在',
                type: 'error'
              })
            }
          }
        }
      });


    }
    const updateSelectStatus = () => {
      console.log('当前的路由:', activePath.value)
      let path = activePath.value;
      moduleList.forEach(item => {
        if (item.index == path) {
          handlerItem(item, false)
        }
        item.subMenu?.forEach(subItem => {
          if (subItem.label == path) {
            handlerItem(item, false)
          }
          subItem.children?.forEach(childrenItem => {
            if (childrenItem.index == path) {
              handlerItem(item, false)
            }
          })
        })
      })
    }
    return {
      activePath,
      isOpen,
      handlerItem,
      moduleList,
      expandState
    }

  }
})
</script>

<style lang="scss">
.tab {
  // $radius: 10px;
  display: flex;
  flex-direction: column;
  position: fixed;
  left: 0;
  width: 70px;
  bottom: 0;
  top: 0;
  background: #282c34;
  box-shadow: 3px 0 6px rgba(0, 0, 0, 0.3);
  z-index: 999;

  .q-module-item {
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: 8px 0 8px 0;
    box-sizing: border-box;

    i {
      font-size: 24px;
      margin-bottom: 5px;
    }
  }

  ul {
    padding-left: 0;
    list-style: none;
    display: flex;
    flex-direction: column;
    color: gray;
    max-height: 100%;
    align-items: center;

    li {
      font-size: 12px;
      width: 56px;
      height: 56px;
      border-radius: 5px;
      box-sizing: border-box;
      display: flex;
      flex-direction: row;
      align-items: center;
      justify-content: center;
      margin-top: 8px;

      a {
        color: #cecece;
      }

      &:hover {
        background: #1890ff;
        cursor: pointer;
        color: white;
      }
    }

    .active {
      a {
        color: white !important;
      }

      color: white;

      background: #1890ff;
    }
  }
}

.q-menu-open {
  left: 70px;
}

.q-menu-close {
  left: -130px;
}

.q-menu-container {
  width: 200px;
  transition: left 0.3s linear;
  position: fixed;
  top: 0;
  bottom: 0;
  background: #ffffff;
  z-index: 900;
  padding-top: 10px;
  box-sizing: border-box;

  .el-menu-item-group {
    padding: 0 10px 0 10px;
  }

  .el-menu-item {
    margin: 0 10px 0 10px;
    box-sizing: border-box;
    border-radius: 10px;
  }

  .el-menu-item.is-active {

    background: var(--el-color-primary-light-8);
    color: #0d1b25;
  }

  .el-sub-menu .el-menu-item {
    min-width: 180px;

    &:hover {
      border-radius: 10px;
    }
  }
}
</style>
