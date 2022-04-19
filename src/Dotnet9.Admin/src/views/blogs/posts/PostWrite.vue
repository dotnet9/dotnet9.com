<template>
  <el-card>
    <div class="tool-bar" v-if="false">
      <Reprint v-if="formModel.id == 0" @complate="spiderComplate"></Reprint>
    </div>
    <el-form
      label-width="80px"
      v-loading="loading"
      :rules="rules"
      ref="formRef"
      :model="formModel"
      label-position="left"
    >
      <el-form-item label="标题" prop="title">
        <el-input placeholder="输入标题" v-model="formModel.title"></el-input>
      </el-form-item>
      <el-form-item label="正文" prop="content">
        <v-md-editor
          height="500px"
          @upload-image="uploadImage"
          :disabled-menus="[]"
          @change="mdChange"
          v-model="formModel.markdown"
        ></v-md-editor>
      </el-form-item>
      <el-form-item label="分类" prop="categories">
        <el-checkbox-group v-model="formModel.categories">
          <el-checkbox
            :label="item.id"
            v-for="(item, i) in categoryItems"
            :key="i"
            name="categories"
          >{{ item.categoryName }}</el-checkbox>
        </el-checkbox-group>
      </el-form-item>
      <el-form-item label="标签" prop="tags">
        <tag-box v-model="formModel.tags"></tag-box>
      </el-form-item>
      <el-form-item label="封面图">
        <div @click="selectImage()" style="display: inline-block;">
          <el-button type="info" v-if="!formModel.cover">上传一张封面图</el-button>
          <img style="width: 200px;" :src="formModel.cover" />
        </div>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="submitForm()">保存</el-button>
        <el-button type="default" @click="back()">返回</el-button>
      </el-form-item>
    </el-form>
    <cropper-box v-model="showCropper" @cropper="cropperSelect"></cropper-box>
  </el-card>
</template>

<script lang="ts" setup>
import { onMounted } from "@vue/runtime-core";
import { ElForm, ElLoading, ElMessage } from "element-plus";
import { get, post, upload } from "shared/http/HttpClient";
import { ref } from "vue";
import { useRoute, useRouter } from "vue-router";
import CropperBox from "../../../components/CropperBox.vue";
import TagBox from "../../../components/TagBox.vue";
import { CategoryItem } from "../categories/CategoryModel";
import Reprint from "./Reprint.vue";


interface articleItem {
  id: number
  title: string
  content: string
  markdown: string
  categories: number[],
  cover: string,
  tags: string[]
}

const formModel = ref<articleItem>({
  id: 0,
  categories: [],
  title: '',
  content: '',
  markdown: '',
  cover: '',
  tags: []
})

const coverBase64 = ref<string>("")
const title = ref("新建")
const loading = ref(false)
const categoryItems = ref<CategoryItem[]>()
const loadCategories = async () => {
  var res = await get<CategoryItem[]>('/admin/category/getlist', {})
  categoryItems.value = res;
}

onMounted(async () => {
  await loadCategories();
  await loadArticleData();
})

var formRef = ref<InstanceType<typeof ElForm>>()

const submitForm = async () => {
  console.log(formModel.value)
  formRef.value?.validate(async (valid) => {
    if (valid) {
      try {
        loading.value = true;
        await post('/admin/post/addorupdate', formModel.value)
        ElMessage({
          type: 'success',
          message: '保存成功'
        })
        formRef.value?.resetFields();
        router.push('/admin/post')
      } finally {
        loading.value = false
      }
    }
  });
}

const rules = {
  title: [{
    required: true, message: '标题不能为空',
    trigger: 'blur'
  }],
  content: [{
    required: true, message: '内容不能为空'
  }],
  categories: [{
    required: true, message: '分类至少选择一个', trigger: 'blur'
  }]
}

var router = useRouter();
var route = useRoute();

const back = () => {
  router.back();
}

console.log('params:', route.query.id)

router.afterEach(guard => {
  console.log('id:' + route.query.id)
  if (!route.query.id) {
    reSet();
  }
})

const reSet = () => {
  formRef.value?.resetFields();
  setTimeout(() => {
    formRef.value?.clearValidate();
  }, 200);
}


const loadArticleData = async () => {
  try {
    loading.value = true
    if (route.query.id) {
      title.value = "编辑"
      var res = await get<
      { title: string, id: number, content: string, categories: { id: number, cateName: string }[], tags: string[], cover: string, markDown: string }>
        ('/admin/post/get', { id: route.query.id })
      console.log('res:', res)
      formModel.value = {
        title: res.title,
        content: res.content,
        categories: res.categories.map(a => a.id),
        id: res.id,
        tags: res.tags,
        cover: res.cover,
        markdown: res.markDown
      }
      console.log('formModel:', formModel.value.tags)


    }
  } finally {
    loading.value = false
  }

}

const showCropper = ref(false)

const cropperSelect = (data: { base64: string }) => {
  coverBase64.value = data.base64;
  let loading = ElLoading.service({
    text: '上传图片中'
  })
  post('/uploadbase64', {
    base64: data.base64
  }).then((res: any) => {
    console.log(res)
    formModel.value.cover = res.url
  }).finally(() => {
    loading.close();
  })
}

const selectImage = () => {
  showCropper.value = true;
}

const spiderComplate = (html: string) => {
  console.log('回调')
  formModel.value.content = html;
}

const uploadImage = (event: any, insertImage: any, files: File[]) => {
  console.log(files)
  files.map(file => {
    console.log(file.name)
    var formData = new FormData();
    formData.append('img', file);
    upload('/upload', formData).then((res: any) => {
      insertImage({
        url: res.url
      })
    })
  })
}

const mdChange = (text: string, html: string) => {
  console.log(text)
  console.log(html)
  formModel.value.content = html
  formModel.value.markdown = text
}
</script>

<style>
</style>
