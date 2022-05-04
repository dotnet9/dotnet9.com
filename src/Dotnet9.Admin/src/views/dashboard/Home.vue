<template>
  <div class="dash-content">
    <el-row :gutter="20">
      <el-col v-bind="grid">
        <el-card shadow="never" class="dash-card"  v-loading="loading">
          <template #header>
            <h4>文章数</h4>
          </template>
          <p class="dash-card-value">{{model.postCount}}</p>
        </el-card>
      </el-col>
      <el-col v-bind="grid">
        <el-card shadow="never" class="dash-card" v-loading="loading">
          <template #header>
            <h4>24小时访问IP数</h4>
          </template>
          <p class="dash-card-value">{{model.ipOf24Hours}}</p>
        </el-card>
      </el-col>
      <el-col v-bind="grid">
        <el-card shadow="never" class="dash-card" v-loading="loading">
          <template #header>
            <h4>24小时404数</h4>
          </template>
          <p class="dash-card-value">{{model.notFoundRequestIn24Hours}}</p>
        </el-card>
      </el-col>
    </el-row>
    <el-row>
      <el-col>
        <el-card shadow="never">
          <template #header>
            <h2>今日评论</h2>
          </template>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted } from 'vue'

import { get } from "shared/http/HttpClient"

const loading = ref(false)

const grid = ref({
  xs: 24, sm: 24, md: 12, lg: 8, xl: 6
})

const model = ref({
  postCount: 0,
  ipOf24Hours: 0,
  notFoundRequestIn24Hours: 0
})

const url = ref('')

const close = (e: { base64: string }) => {
  url.value = e.base64

}

onMounted(() => {
  get('/admin/dashboard/count', {}).then((res: any) => {
    model.value = res
  })
})

</script>

<style lang="scss">
body {
  font-family: 'Helvetica Neue', Helvetica, 'PingFang SC', 'Hiragino Sans GB',
    'Microsoft YaHei', '微软雅黑', Arial, sans-serif;
}

.dash-content {
  padding: 10px;
}

.dash-card {
  min-height: 170px;
  margin-bottom: 20px;
}

.dash-content {
  h4 {
    margin: 0;
  }

  .dash-card-value {
    font-size: 2rem;
    font-weight: 400;

    span {
      font-size: 1rem;
      margin-left: 5px;
    }
  }
}
</style>