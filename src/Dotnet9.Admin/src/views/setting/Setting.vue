<template>
  <el-card>
    <el-tabs lazy>
      <el-tab-pane label="站点配置">
        <SiteSetting/>
      </el-tab-pane>
      <el-tab-pane label="腾讯云COS配置">
        <CosSetting/>
      </el-tab-pane>
      <el-tab-pane label="微信公众号">
        <WechatSetting></WechatSetting>
      </el-tab-pane>
      <el-tab-pane label="系统配置">
        <SystemSetting/>
      </el-tab-pane>
    </el-tabs>
  </el-card>
</template>

<script lang="ts" setup>
import Cos from './Cos-Setting.vue'
import Site from './Site-Setting.vue'
import System from './System-Setting.vue'
import WechatSetting from './Wechat-Setting.vue'
import SiteSetting from "./Site-Setting.vue";
import CosSetting from "./Cos-Setting.vue";
</script>

<style>
</style>