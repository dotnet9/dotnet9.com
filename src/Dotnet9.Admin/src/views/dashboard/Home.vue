<template>
  <div class="dash-content">
    <el-row :gutter="20">
      <el-col v-bind="grid">
        <div class="con_div_text">
          <img src="./../../assets/post.png" class="left text_img">
          <div class="left text_div">
            <p>文章总数</p>
            <p>{{ model.postCount }}篇</p>
          </div>
        </div>
      </el-col>
      <el-col v-bind="grid">
        <div class="con_div_text">
          <img src="./../../assets/IP.png" class="left text_img">
          <div class="left text_div">
            <p>24时IP访问</p>
            <p>{{ model.ipOf24Hours }}个</p>
          </div>
        </div>
      </el-col>
      <el-col v-bind="grid">
        <div class="con_div_text">
          <img src="./../../assets/disk-read.png" class="left text_img">
          <div class="left text_div">
            <p>磁盘读</p>
            <p>{{ model.diskRead }}</p>
          </div>
        </div>
      </el-col>
      <el-col v-bind="grid">
        <div class="con_div_text">
          <img src="./../../assets/disk-write.png" class="left text_img">
          <div class="left text_div">
            <p>磁盘写</p>
            <p>{{ model.diskWrite }}</p>
          </div>
        </div>
      </el-col>
      <el-col v-bind="grid">
        <div class="con_div_progress">
          <el-progress type="dashboard" :percentage="model.cpuLoad" :color="colors">
            <template #default="{ percentage }">
              <span class="percentage-value">{{ model.cpuLoad }}%</span>
              <span class="percentage-label">CPU当前负载</span>
            </template>
          </el-progress>
        </div>
      </el-col>
      <el-col v-bind="grid">
        <div class="con_div_progress">
          <el-progress type="dashboard" :percentage="model.memoryUsage" :color="colors">
            <template #default="{ percentage }">
              <span class="percentage-value">{{ model.memoryUsage }}%</span>
              <span class="percentage-label">内存使用率</span>
            </template>
          </el-progress>
        </div>
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
  { color: '#f56c6c', percentage: 100 },
  { color: '#e6a23c', percentage: 80 },
  { color: '#5cb87a', percentage: 60 },
  { color: '#1989fa', percentage: 40 },
  { color: '#6f7ad3', percentage: 20 },
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

.left {
  float: left;
}

.con_div_text {
  height: 90%;
  width: 90%;
  margin-right: 1.3%;
  margin-top: 1.3%;
  background-color: #034c6a;
}

.text_img {
  margin: 35px;
}

.text_div {
  margin-top: 15px;
  margin-left: 5%;
  text-align: center;

}

.text_div p {
  line-height: 50px;
}

.text_div p:nth-child(1) {
  font-size: 16px;
  color: #ffffff;
}

.text_div p:nth-child(2) {
  font-size: 28px;
  color: #ffff43;
  font-weight: 600;
}

.con_div_progress {
  height: 90%;
  width: 90%;
  background-color: #fff;
  margin-top: 1.3%;
  margin-right: 1.3%;
  text-align: center;
  padding-top: 0 20px;
}

.percentage-value {
  display: block;
  margin-top: 10px;
  font-size: 28px;
}

.percentage-label {
  display: block;
  margin-top: 10px;
  font-size: 12px;
}
</style>