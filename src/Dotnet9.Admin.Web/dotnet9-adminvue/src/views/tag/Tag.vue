<template>
  <el-card class="main-card">
    <div class="title">{{ this.$route.name }}</div>
    <div class="operation-container">
      <el-button type="primary" size="small" icon="el-icon-plus" @click="openModel(null)"> 新增 </el-button>
      <el-button
        type="danger"
        size="small"
        icon="el-icon-delete"
        :disabled="tagIds.length == 0"
        @click="isDelete = true">
        批量删除
      </el-button>
      <div style="margin-left: auto">
        <el-input
          v-model="keywords"
          prefix-icon="el-icon-search"
          size="small"
          placeholder="请输入标签名"
          style="width: 200px"
          @keyup.enter.native="searchTags" />
        <el-button type="primary" size="small" icon="el-icon-search" style="margin-left: 1rem" @click="searchTags">
          搜索
        </el-button>
      </div>
    </div>
    <el-table border :data="tags" v-loading="loading" @selection-change="selectionChange">
      <el-table-column type="selection" width="55" />
      <el-table-column prop="name" label="标签名" align="center">
        <template slot-scope="scope">
          <el-tag>
            {{ scope.row.name }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="blogPostCount" label="文章量" align="center" />
      <el-table-column prop="creationTime" label="创建时间" align="center">
        <template slot-scope="scope">
          <i class="el-icon-time" style="margin-right: 5px" />
          {{ scope.row.creationTime | date }}
        </template>
      </el-table-column>
      <el-table-column label="操作" align="center" width="160">
        <template slot-scope="scope">
          <el-button type="primary" size="mini" @click="openModel(scope.row)"> 编辑 </el-button>
          <el-popconfirm title="确定删除吗？" style="margin-left: 1rem" @confirm="deleteTag(scope.row.id)">
            <el-button size="mini" type="danger" slot="reference"> 删除 </el-button>
          </el-popconfirm>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination
      class="pagination-container"
      background
      @size-change="sizeChange"
      @current-change="currentChange"
      :current-page="current"
      :page-size="pageSize"
      :total="count"
      :page-sizes="[10, 20]"
      layout="total, sizes, prev, pager, next, jumper" />
    <el-dialog :visible.sync="isDelete" width="30%">
      <div class="dialog-title-container" slot="title"><i class="el-icon-warning" style="color: #ff9900" />提示</div>
      <div style="font-size: 1rem">是否删除选中项？</div>
      <div slot="footer">
        <el-button @click="isDelete = false">取 消</el-button>
        <el-button type="primary" @click="deleteTag(null)"> 确 定 </el-button>
      </div>
    </el-dialog>
    <el-dialog :visible.sync="addOrEdit" width="30%">
      <div class="dialog-title-container" slot="title" ref="tagTitle" />
      <el-form label-width="80px" size="medium" :model="tagForm">
        <el-form-item label="标签名">
          <el-input style="width: 220px" v-model="tagForm.name" />
        </el-form-item>
      </el-form>
      <div slot="footer">
        <el-button @click="addOrEdit = false">取 消</el-button>
        <el-button type="primary" @click="addOrEditTag"> 确 定 </el-button>
      </div>
    </el-dialog>
  </el-card>
</template>

<script>
export default {
  created() {
    this.current = this.$store.state.pageState.tag
    this.listTags()
  },
  data: function () {
    return {
      isDelete: false,
      loading: true,
      addOrEdit: false,
      keywords: null,
      tags: [],
      tagIds: [],
      tagForm: {
        id: null,
        name: ''
      },
      current: 1,
      pageSize: 10,
      count: 0
    }
  },
  methods: {
    selectionChange(tags) {
      this.tagIds = []
      tags.forEach((item) => {
        this.tagIds.push(item.id)
      })
    },
    searchTags() {
      this.current = 1
      this.listTags()
    },
    sizeChange(pageSize) {
      this.pageSize = pageSize
      this.listTags()
    },
    currentChange(current) {
      this.current = current
      this.$store.commit('updateTagPageState', current)
      this.listTags()
    },
    deleteTag(id) {
      var param = {}
      if (id == null) {
        param = { ids: this.tagIds }
      } else {
        param = { ids: [id] }
      }
      this.axios.delete('/api/tags', { data: param }).then(({ data }) => {
        if (data.success) {
          this.$notify.success({
            title: '成功',
            message: data.message
          })
          this.listTags()
        } else {
          this.$notify.error({
            title: '失败',
            message: data.message
          })
        }
      })
      this.isDelete = false
    },
    listTags() {
      this.axios
        .get('/api/tags', {
          params: {
            current: this.current,
            pageSize: this.pageSize,
            keywords: this.keywords
          }
        })
        .then(({ data }) => {
          this.tags = data.data.records

          this.count = data.data.total
          this.loading = false
        })
    },
    openModel(tag) {
      if (tag != null) {
        this.tagForm = JSON.parse(JSON.stringify(tag))
        this.$refs.tagTitle.innerHTML = '修改标签'
      } else {
        this.tagForm.id = null
        this.tagForm.name = ''
        this.$refs.tagTitle.innerHTML = '添加标签'
      }
      this.addOrEdit = true
    },
    addOrEditTag() {
      if (this.tagForm.name.trim() == '') {
        this.$message.error('标签名不能为空')
        return false
      }
      this.axios.post('/api/tags', this.tagForm).then(({ data }) => {
        if (data.success) {
          this.$notify.success({
            title: '成功',
            message: data.message
          })
          this.listTags()
        } else {
          this.$notify.error({
            title: '失败',
            message: data.message
          })
        }
      })
      this.addOrEdit = false
    }
  }
}
</script>
