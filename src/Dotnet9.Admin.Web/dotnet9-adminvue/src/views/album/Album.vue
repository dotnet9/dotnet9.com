<template>
  <el-card class="main-card">
    <div class="title">{{ this.$route.name }}</div>
    <div class="operation-container">
      <el-button type="primary" size="small" icon="el-icon-plus" @click="openModel(null)"> 新增 </el-button>
      <el-button
        type="danger"
        size="small"
        icon="el-icon-delete"
        :disabled="this.albumIds.length == 0"
        @click="isDelete = true">
        批量删除
      </el-button>
      <div style="margin-left: auto">
        <el-input
          v-model="keywords"
          prefix-icon="el-icon-search"
          size="small"
          placeholder="请输入专辑名"
          style="width: 200px"
          @keyup.enter.native="searchAlbums" />
        <el-button
          type="primary"
          size="small"
          icon="el-icon-search"
          style="margin-left: 1rem"
          @click="searchAlbums">
          搜索
        </el-button>
      </div>
    </div>
    <el-table border :data="albums" @selection-change="selectionChange" v-loading="loading">
      <el-table-column type="selection" width="55" />
      <el-table-column prop="cover" label="封面" align="center">
        <template slot-scope="scope">
          <img :src="scope.row.cover" alt="封面" width="120px" />
        </template>
      </el-table-column>
      <el-table-column prop="name" label="专辑名" align="center" />
      <el-table-column prop="slug" label="别名" align="center" />
      <el-table-column prop="categoryNames" label="所属分类" align="center" />
      <el-table-column prop="visible" label="是否可见" align="center">
        <template slot-scope="scoped">
          <el-switch
            active-color="#13ce66"
            inactive-color="#ff4949"
            v-model="scoped.row.visible"
            @change="changeVisible($event, scoped.row, scoped.$index, scoped.row.visible)">
          </el-switch>
        </template>
      </el-table-column>
      <el-table-column prop="blogPostCount" label="文章量" align="center" />
      <el-table-column prop="sequenceNumber" label="排序" align="center" />
      <el-table-column prop="creationTime" label="创建时间" align="center">
        <template slot-scope="scope">
          <i class="el-icon-time" style="margin-right: 5px" />
          {{ scope.row.creationTime | date }}
        </template>
      </el-table-column>
      <el-table-column label="操作" width="160" align="center">
        <template slot-scope="scope">
          <el-button type="primary" size="mini" @click="openModel(scope.row)"> 编辑 </el-button>
          <el-popconfirm title="确定删除吗？" style="margin-left: 1rem" @confirm="deleteAlbum(scope.row.id)">
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
        <el-button type="primary" @click="deleteAlbum(null)"> 确 定 </el-button>
      </div>
    </el-dialog>
    <el-dialog :visible.sync="addOrEdit" width="30%">
      <div class="dialog-title-container" slot="title" ref="albumTitle" />
      <el-form label-width="80px" size="medium" :model="albumForm" ref="albumForm" :rules="rules">
        <el-form-item label="封面" prop="cover">
          <el-col :span="5">
            <el-image :src="albumForm.cover" :fit="contain" />
          </el-col>
          <el-col :span="1" />
          <el-col :span="18">
            <el-input v-model="albumForm.cover" />
          </el-col>
        </el-form-item>
        <el-form-item label="专辑名" prop="name">
          <el-input v-model="albumForm.name" />
        </el-form-item>
        <el-form-item label="别名" prop="slug">
          <el-input v-model="albumForm.slug" />
        </el-form-item>
        <el-form-item label="分类" prop="parentId">
          <template>
            <span>{{ this.albumForm.categoryNames }}</span>
            <el-input v-model="filterCategoryText" placeholder="搜索分类名" />
            <div class="category-tree">
              <el-tree
                node-key="key"
                :default-checked-keys="albumForm.categoryIds"
                ref="categoryTreeRef"
                class="filter-tree"
                :data="allCategories"
                :props="defaultCategoryProps"
                show-checkbox
                default-expand-all
                :filter-node-method="filterCategoryNode"
                @check="handleParentCategoryChecked"/>
            </div>
          </template>
        </el-form-item>
        <el-form-item label="是否可见" prop="visible">
          <el-switch v-model="albumForm.visible" />
        </el-form-item>
        <el-form-item label="排序" prop="sequenceNumber">
          <el-input v-model.number="albumForm.sequenceNumber" auto-complete="off" />
        </el-form-item>
      </el-form>
      <div slot="footer">
        <el-button @click="addOrEdit = false">取 消</el-button>
        <el-button type="primary" @click="addOrEditAlbum('albumForm')"> 确 定 </el-button>
      </div>
    </el-dialog>
  </el-card>
</template>

<script>
export default {
  created() {
    this.current = this.$store.state.pageState.album
    this.listAlbums()
    this.listAlbumTree()
  },
  watch: {
    filterCategoryText(val) {
      this.$refs.categoryTreeRef.filter(val)
    }
  },
  data: function () {
    return {
      isDelete: false,
      loading: true,
      addOrEdit: false,
      keywords: null,
      albumIds: [],
      albums: [],
      albumForm: {
        id: null,
        cover: '',
        name: '',
        slug: '',
        categoryIds: [],
        categoryNames: '',
        visible: false,
        sequenceNumber: 1
      },
      rules: {
        cover: [
          { required: true, message: '请输入封面', trigger: 'blur' },
          { min: 2, max: 128, message: '长度在 2 到 128个字符', trigger: 'blur' }
        ],
        name: [
          { required: true, message: '请输入名称', trigger: 'blur' },
          { min: 2, max: 32, message: '长度在 2 到 32个字符', trigger: 'blur' }
        ],
        slug: [
          { required: true, message: '请输入别名', trigger: 'blur' },
          { min: 2, max: 256, message: '长度在 2 到 256个字符', trigger: 'blur' }
        ],
        sequenceNumber: [
          { required: true, message: '请输入排序号' },
          { type: 'number', message: '序号必须为数字值' }
        ]
      },
      filterCategoryText: '',
      allCategories: [],
      defaultCategoryProps: {
        children: 'children',
        label: 'title'
      },
      current: 1,
      pageSize: 10,
      count: 0
    }
  },
  methods: {
    selectionChange(albums) {
      this.albumIds = []
      albums.forEach((item) => {
        this.albumIds.push(item.id)
      })
    },
    searchAlbums() {
      this.current = 1
      this.listAlbums()
    },
    sizeChange(pageSize) {
      this.pageSize = pageSize
      this.listAlbums()
    },
    currentChange(current) {
      this.current = current
      this.$store.commit('updateAlbumPageState', current)
      this.listAlbums()
    },
    changeVisible(e, row, index, visible) {
      this.axios
        .put('/api/albums/' + row.id + '/changeVisible', { id: row.id, visible: visible })
        .then(({ data }) => {
          if (data.success) {
            this.$notify.success({
              title: '成功',
              message: data.message
            })
            this.listAlbums()
          } else {
            this.$notify.error({
              title: '失败',
              message: data.message
            })
          }
        })
    },
    deleteAlbum(id) {
      let param = {}
      if (id == null) {
        param = { ids: this.albumIds }
      } else {
        param = { ids: [id] }
      }
      this.axios.delete('/api/albums', { data: param }).then(({ data }) => {
        if (data.success) {
          this.$notify.success({
            title: '成功',
            message: data.message
          })
          this.listAlbums()
        } else {
          this.$notify.error({
            title: '失败',
            message: data.message
          })
        }
        this.isDelete = false
      })
    },
    listAlbums() {
      this.axios
        .get('/api/albums', {
          params: {
            current: this.current,
            pageSize: this.pageSize,
            keywords: this.keywords
          }
        })
        .then(({ data }) => {
          this.albums = data.data.records
          this.count = data.data.count
          this.loading = false
        })
    },
    listAlbumTree() {
      this.axios.get('/api/categories/tree').then(({ data }) => {
        this.allCategories = data.data
      })
    },
    openModel(album) {
      if (album != null) {
      // TODO 切换专辑后，选择的分类无法重置
        this.albumForm.categoryIds = []
        this.albumForm = JSON.parse(JSON.stringify(album))
        this.$refs.albumTitle.innerHTML = '修改专辑'
      } else {
        this.albumForm.id = null
        this.albumForm.name = ''
        this.albumForm.cover = ''
        this.albumForm.slug = ''
        this.albumForm.categoryIds = []
        this.albumForm.categoryNames = ''
        this.albumForm.visible = false
        this.albumForm.sequenceNumber = 0
        this.$refs.albumTitle.innerHTML = '添加专辑'
      }
      this.addOrEdit = true
    },
    filterCategoryNode(value, data) {
      if (!value) return true
      return data.title.indexOf(value) !== -1
    },
    handleParentCategoryChecked(data, checked) {
      this.albumForm.categoryIds = this.$refs.categoryTreeRef.getCheckedKeys()
      this.albumForm.categoryNames = this.$refs.categoryTreeRef.getCheckedNodes().map(function(obj, index){
        return obj.title
      }).join(",")
    },
    addOrEditAlbum(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          if (this.albumForm.id === null) {
            this.axios.post('/api/albums', this.albumForm).then(({ data }) => {
              if (data.success) {
                this.$notify.success({
                  title: '成功',
                  message: data.message
                })
                this.listAlbums()
              } else {
                this.$notify.error({
                  title: '失败',
                  message: data.message
                })
              }
              this.addOrEdit = false
            })
          } else {
            this.axios.put('/api/albums/' + this.albumForm.id, this.albumForm).then(({ data }) => {
              if (data.success) {
                this.$notify.success({
                  title: '成功',
                  message: data.message
                })
                this.listAlbums()
              } else {
                this.$notify.error({
                  title: '失败',
                  message: data.message
                })
              }
              this.addOrEdit = false
            })
          }
        } else {
          return false
        }
      })
    }
  }
}
</script>

<style scoped>
.category-tree {
  height: 250px;
  display: block;
  overflow-y: scroll;
}
</style>