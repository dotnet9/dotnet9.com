<template>
  <div class="dash-content">
    <el-row :gutter="20">
      <el-col v-bind="grid">
        <div class="con_div_text">
          <img src="./../../assets/post.png" class="left text_img" />
          <div class="left text_div">
            <p>文章总数</p>
            <p>{{ model.systemCountInfo?.postCount }}篇</p>
          </div>
        </div>
      </el-col>
      <el-col v-bind="grid">
        <div class="con_div_text">
          <img src="./../../assets/IP.png" class="left text_img" />
          <div class="left text_div">
            <p>24时IP访问</p>
            <p>{{ model.systemCountInfo?.ipOf24Hours }}个</p>
          </div>
        </div>
      </el-col>
      <el-col v-bind="grid">
        <div class="con_div_text">
          <img src="./../../assets/disk-read.png" class="left text_img" />
          <div class="left text_div">
            <p>磁盘读</p>
            <p>{{ model.systemCountInfo?.diskRead }}</p>
          </div>
        </div>
      </el-col>
      <el-col v-bind="grid">
        <div class="con_div_text">
          <img src="./../../assets/disk-write.png" class="left text_img" />
          <div class="left text_div">
            <p>磁盘写</p>
            <p>{{ model.systemCountInfo?.diskWrite }}</p>
          </div>
        </div>
      </el-col>
      <el-col v-bind="grid">
        <div class="con_div_progress">
          <el-progress
            type="dashboard"
            :percentage="model.systemCountInfo?.cpuLoad"
            :color="colors"
          >
            <template #default="{ percentage }">
              <span class="percentage-value"
                >{{ model.systemCountInfo?.cpuLoad }}%</span
              >
              <span class="percentage-label">CPU当前负载</span>
            </template>
          </el-progress>
        </div>
      </el-col>
      <el-col v-bind="grid">
        <div class="con_div_progress">
          <el-progress
            type="dashboard"
            :percentage="model.systemCountInfo?.memoryUsage"
            :color="colors"
          >
            <template #default="{ percentage }">
              <span class="percentage-value"
                >{{ model.systemCountInfo?.memoryUsage }}%</span
              >
              <span class="percentage-label">内存使用率</span>
            </template>
          </el-progress>
        </div>
      </el-col>
    </el-row>
    <div class="HomeTable">
      <el-card shadow="never">
        <template #header>
          <h2>Top10搜索词</h2>
        </template>
        <el-table :data="model.top10Searches?.datas">
          <el-table-column label="搜索词" prop="key" />
          <el-table-column label="浏览量(PV)" prop="pv" />
          <el-table-column label="占比" prop="percent" />
        </el-table>
      </el-card>
      <el-card shadow="never">
        <template #header>
          <h2>Top10受访页面</h2>
        </template>
        <el-table :data="model.top10AccessPages?.datas">
          <el-table-column label="受访页面" prop="url" />
          <el-table-column label="浏览量(PV)" prop="pv" />
          <el-table-column label="占比" prop="percent" />
        </el-table>
      </el-card>
      <el-card shadow="never">
        <template #header>
          <h2>实时访问：{{ latestDate }}</h2>
        </template>
        <el-table :data="latestActionLogs">
          <el-table-column label="时间" prop="createDate" />
          <el-table-column label="访问地址" prop="url" />
          <el-table-column label="IP" prop="ip" />
          <el-table-column label="浏览器" prop="browser" />
          <el-table-column label="操作系统" prop="os" />
        </el-table>
      </el-card>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, reactive } from "vue";

import { get } from "shared/http/HttpClient";
import { ElTable, ElTableColumn } from "element-plus";
import { react } from "@babel/types";
import { Timer } from "@element-plus/icons";

const loading = ref(false);

const grid = ref({
  xs: 24,
  sm: 24,
  md: 12,
  lg: 8,
  xl: 6,
});

const colors = [
  { color: "#f56c6c", percentage: 100 },
  { color: "#e6a23c", percentage: 80 },
  { color: "#5cb87a", percentage: 60 },
  { color: "#1989fa", percentage: 40 },
  { color: "#6f7ad3", percentage: 20 },
];

const model = ref({
  systemCountInfo: {
    postCount: 0,
    ipOf24Hours: 0,
    cpuLoad: 0,
    memoryUsage: 0,
    diskRead: "",
    diskWrite: "",
  },
  top10Searches: {
    datas: [],
  },
  latestLogs: {
    latestDate: "",
  },
  top10AccessPages: {
    datas: [],
  },
});

let latestDate = ref("");
const latestActionLogs = reactive([]);

const url = ref("");

const close = (e: { base64: string }) => {
  url.value = e.base64;
};

onMounted(() => {
  setInterval(loadDatas, 3000);
});

const loadDatas = () => {
  get("/api/dashboard/count", { request: latestDate.value ?? "" }).then(
    (res: any) => {
      model.value = res;
      if (res.latestLogs?.latestDate !== null) {
        latestDate.value = res.latestLogs?.latestDate;
        latestActionLogs.unshift(...res.latestLogs?.datas);
        if (latestActionLogs.length > 10) {
          latestActionLogs.length = 10;
        }
      }
    }
  );
};
</script>

<style lang="scss">
body {
  font-family: "Helvetica Neue", Helvetica, "PingFang SC", "Hiragino Sans GB",
    "Microsoft YaHei", "微软雅黑", Arial, sans-serif;
}

.dash-content {
  padding: 10px;
}

.left {
  float: left;
}

.con_div_text {
  height: 100%;
  width: 100%;
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
  width: 100%;
  background-color: #fff;
  margin-top: 4%;
  margin-right: 1.3%;
  text-align: center;
  padding-top: 10px;
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

.HomeTable {
  display: flex;
  justify-content: space-between;
  margin-top: 1.6%;
  > div {
    &:last-child {
      width: 50%;
    }
    width: 25%;
    margin-right: 0.9%;
  }
}

@media (max-width: 500px) {
  .HomeTable {
    flex-wrap: wrap;

    > div {
      width: 100%;
      margin-top: 10px;
    }
  }
}
</style>