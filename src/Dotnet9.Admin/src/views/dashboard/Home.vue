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
            <h4>磁盘读</h4>
          </template>
          <p class="dash-card-value">{{model.diskRead}}</p>
        </el-card>
      </el-col>
      <el-col v-bind="grid">
        <el-card shadow="never" class="dash-card" v-loading="loading">
          <template #header>
            <h4>磁盘写</h4>
          </template>
          <p class="dash-card-value">{{model.diskWrite}}</p>
        </el-card>
      </el-col>
      <el-col v-bind="grid">
        <el-card shadow="never" class="dash-card"  v-loading="loading">
          <template #header>
            <h4>CPU当前负载</h4>
          </template>
          <p class="dash-card-value">
            <el-progress type="dashboard" :percentage="model.cpuLoad" :color="colors" />
          </p>
        </el-card>
      </el-col>
      <el-col v-bind="grid">
        <el-card shadow="never" class="dash-card" v-loading="loading">
          <template #header>
            <h4>内存使用率</h4>
          </template>
          <p class="dash-card-value">
            <el-progress type="dashboard" :percentage="model.memoryUsage" :color="colors" />
          </p>
        </el-card>
      </el-col>
    </el-row>
    <el-row>
      <el-col>
        <el-card shadow="never">
          <template #header>
            <h2>最近访问</h2>
          </template>
          <el-table :data="list">
            <el-table-column label="时间" prop="createDate" />
            <el-table-column label="访问地址" prop="url" />
            <el-table-column label="IP" prop="ip" />
            <el-table-column label="浏览器" prop="browser" />
            <el-table-column label="操作系统" prop="os" />
          </el-table>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, reactive } from 'vue'

import { get } from "shared/http/HttpClient"

import { ElTable, ElTableColumn } from 'element-plus'
import { react } from '@babel/types'
import { Timer } from '@element-plus/icons'

const loading = ref(false)

const grid = ref({
  xs: 24, sm: 24, md: 12, lg: 8, xl: 6
})

const colors = [
  { color: '#f56c6c', percentage: 20 },
  { color: '#e6a23c', percentage: 40 },
  { color: '#5cb87a', percentage: 60 },
  { color: '#1989fa', percentage: 80 },
  { color: '#6f7ad3', percentage: 100 },
]

const model = ref({
  postCount: 0,
  ipOf24Hours: 0,
  notFoundRequestIn24Hours: 0,
  cpuLoad: 10,
  memoryUsage: 20,
  diskRead: '',
  diskWrite: '',
})

const list = reactive([

])

const page = ref(1)

const url = ref('')

const close = (e: { base64: string }) => {
  url.value = e.base64
}

onMounted(() => {
  setInterval(loadDatas, 1000)
})

const loadDatas = () => {
  get('/api/dashboard/count', {}).then((res: any) => {
    model.value = res
  })
  loadActionLogs();
}

const loadActionLogs = () => {
  get('/api/dashboard/GetActionLog', { page: page.value }).then((res: any) => {
    list.length = 0;
    list.push(...res.datas)
  });
}

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
  min-height: 130px;
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