<template>
  <el-header :class="[expandState?'open':'close']">
    <div class="q-header-container">
      <div class="q-header-left">
        <div class="q-header-item" @click="handlerExpand(!expandState)" v-if="showExpandButton">
          <div class="q-toolbar-item pointer">
            <i class="ri-menu-fold-line"></i>
          </div>
        </div>
        <div class="q-header-item">
          <el-breadcrumb>
            <el-breadcrumb-item :to="{ path: '/' }">主页</el-breadcrumb-item>
            <el-breadcrumb-item>当前页面</el-breadcrumb-item>
          </el-breadcrumb>
        </div>
      </div>
      <div class="q-toolbar">
        <el-popover trigger="hover" :width="300">
          <template #reference>
            <div class="q-toolbar-item">
              <i class="ri-notification-line"></i>
            </div>
          </template>
          <div class="notify-box">
            <el-tabs v-model="notifySelect">
              <el-tab-pane label="通知" name="notify">通知</el-tab-pane>
              <el-tab-pane label="消息" name="message">消息</el-tab-pane>
              <el-tab-pane label="代办" name="todo">待办</el-tab-pane>
            </el-tabs>
          </div>
        </el-popover>
        <div class="q-toolbar-item">
          <q-fullscreen></q-fullscreen>
        </div>
        <el-dropdown trigger="hover">
          <div class="q-toolbar-item">
            <el-avatar :size="30" src="../../assets/logo.jpeg"></el-avatar>
          </div>
          <template #dropdown>
            <el-dropdown-menu>
              <el-dropdown-item @click="toHome()">主页</el-dropdown-item>
              <el-dropdown-item @click="toSetting()">个人设置</el-dropdown-item>
              <el-dropdown-item divided @click="quit()">退出</el-dropdown-item>
            </el-dropdown-menu>
          </template>
        </el-dropdown>
      </div>
    </div>
  </el-header>
</template>

<script lang="ts">
import {http} from "shared/http/HttpClient";
import {ref} from "@vue/reactivity";

import {defineComponent} from 'vue'
import {useRouter} from "vue-router";

import {expandState, handlerExpand,showExpandButton} from './ExpandState'

export default defineComponent({
  name: "TopBar",
  props: {
    expand: {
      type: Boolean,
      default: false
    }
  },
  emits: ["update:expand"],
  setup(props, context) {
    const router = useRouter()
    const quit = async () => {
      try {
        await http.get('/admin/account/LoginOut')
        router?.replace({
          path: '/login'
        })
      } catch (error) {

      }
    }

    const toSetting = () => {
      router.push('/admin/setting?type=3')
    }

    const toHome = () => {
      router.replace("/admin/dash")
    }

    const notifySelect = ref('notify')
    return {
      quit,
      notifySelect,
      expandState,
      handlerExpand,
      toSetting,
      toHome,
      showExpandButton
    }
  }
})
</script>

<style scoped lang="scss">
.open {
  left: 270px !important;
}

.close {
  left: 70px !important;
}

.el-header {
  background: #fff;
  box-shadow: 0 0 1px #ccc;
  display: flex;
  flex-direction: row;
  align-items: center;
  box-sizing: border-box;
  padding: 5px;
  border-bottom: 0.5px solid #dcdfe6;
  position: absolute;
  left: 0px;
  top: 0;
  right: 0;
  height: 60px;
  z-index: 800;
  transition: left 0.3s linear;

  .q-header-container {
    display: flex;
    width: 100%;
    flex-direction: row;
    justify-content: space-between;

    .q-header-left {
      display: flex;
      flex-direction: row;
      align-items: center;
    }

    .q-header-item {
      margin-left: 15px;
      min-height: 45px;
      display: flex;
      flex-direction: row;
      align-items: center;
      min-width: 45px;
      box-sizing: border-box;
    }
  }

  .q-expand {
    height: 60px;
    width: 60px;
    cursor: pointer;
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
  }
}

.q-toolbar {
  display: flex;
  flex-direction: row;

  .q-toolbar-item {
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    padding: 0 0.8rem;
    cursor: pointer;
  }

  .el-tabs__item {
    width: 1/3;
  }
}

.notify-box {
  width: 100%;

  .el-tabs__nav {
    width: 100%;

    .el-tabs__item {
      width: 33.33%;
      text-align: center;
    }
  }
}
</style>