<template>
  <el-container>
    <el-aside width="220px" v-if="false">
      <div class="q-logo">
        <p>Admin</p>
      </div>
      <SlideMenu></SlideMenu>
    </el-aside>
    <el-container>
      <el-main>
        <tab></tab>
        <div :class="[expandState ? 'q-content-open' : 'q-content-close']" class="q-content">
          <router-view></router-view>
        </div>
      </el-main>
    </el-container>
  </el-container>
</template>
<script lang="ts">
import SlideMenu from "../../components/SlideMenu.vue";
import {Expand, FullScreen} from '@element-plus/icons'
import {useRouter} from "vue-router";
import {ref} from "@vue/reactivity";
import {http} from "shared/http/HttpClient"
import {defineComponent} from "vue";

import {expandState} from './ExpandState'

export default defineComponent({
  components: {
    SlideMenu,
    Expand,
    FullScreen
  },
  setup() {
    const fullScreen = () => {
    }
    const isOpen = ref(true)
    const expandMenu = () => {
      isOpen.value = !isOpen.value
    }
    return {
      fullScreen,
      isOpen,
      expandMenu,
      expandState
    }
  }
})
</script>
<style lang="less" scoped>
.q-content {
  padding: 70px 10px 10px;
  //background: #f6f8f9;
  transition: margin-left 0.3s linear;
  position: relative;

}

.q-content-open {
  margin-left: 270px;
}

.q-content-close {
  margin-left: 70px;
}

.el-container {
  height: 100%;

  .el-aside {
    overflow-x: hidden;
    overflow-y: hidden;
    box-shadow: 10px 0 10px -10px #c7c7c7;
    z-index: 99;

    .q-logo {
      width: 100%;
      height: 60px;
      text-align: center;
      line-height: 60px;
      font-size: 30px;
      font-weight: 400;
    }
  }

  .el-header-close {
    left: 70px !important;
    transition: left 0.3s linear;
  }


}


.el-main {
  box-sizing: border-box;
  padding: 0;

  .el-main-container {
    background: white;
    width: 100%;
    // height: 100%;
  }

  h1 {
    margin: 0;
    padding: 0;
  }
}
</style>