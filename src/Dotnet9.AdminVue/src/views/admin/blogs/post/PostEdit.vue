<template>
    <ElCard>
        <template #header>
            <ElButton @click="goBack()" :icon="Back" text type="info">
                返回
            </ElButton>
        </template>
        <ElForm ref="formEl" label-position="top" :rules="rules" :model="form" scroll-to-error @submit.native.prevent>
            <ElFormItem label="标题" prop="title">
                <ElInput v-model="form.title"></ElInput>
            </ElFormItem>
            <ElFormItem :label="`摘要:${form.snippet.length}字`" prop="snippet">
                <ElInput type="textarea" v-model="form.snippet" :rows="5"></ElInput>
            </ElFormItem>
            <ElFormItem label="内容" prop="content">
                <CWangEditor v-model="form.content"></CWangEditor>
            </ElFormItem>
            <ElFormItem label="封面图" prop="thumb">
                <div class="avatar-uploader" @click="(showCropper = !showCropper)">
                    <img v-if="form.thumb" :src="form.thumb" class="avatar" />
                    <el-icon v-else class="avatar-uploader-icon">
                        <Plus />
                    </el-icon>
                </div>
            </ElFormItem>
            <ElFormItem label="分类" prop="cates">
                <ElCheckboxGroup v-model="form.cates">
                    <ElCheckbox v-for="item in cateList" :key="item.id" :label="item.id">
                        {{ item.cateName }}
                    </ElCheckbox>
                </ElCheckboxGroup>
            </ElFormItem>
            <ElFormItem label="标签" prop="tagsStr">
                <ElInput placeholder="标签用|分割" v-model="form.tagsStr"></ElInput>
            </ElFormItem>
            <ElFormItem label="是否发布">
                <ElSwitch v-model="form.isPublish"></ElSwitch>
            </ElFormItem>
            <ElFormItem>
                <ElSpace>
                    <ElButton type="primary" @click="submit()" native-type="submit" :loading="subLoading">保存</ElButton>
                    <ElButton @click="() => router.back()">返回</ElButton>
                </ElSpace>
            </ElFormItem>
        </ElForm>
    </ElCard>
    <SelectImageView v-model="showCropper" :width="200" :height="200" @on-get-img="selectThumb">
    </SelectImageView>
</template>
<script lang="ts" setup>
import { onMounted, reactive, ref } from 'vue'

import { useRoute, useRouter } from 'vue-router';
import { Back, Plus } from "@element-plus/icons-vue"
import SelectImageView from '@/components/SelectImageView.vue';

import CWangEditor from '@/components/CWangEditor.vue';

import { PostEditRequest, CateService, CateDtoModel, PostService } from '@/shared/service'
import { ElLoading, ElMessage, FormInstance, FormRules } from 'element-plus';


const formEl = ref<FormInstance>();

const showCropper = ref(false)

const cateList = ref<CateDtoModel[]>([])

const router = useRouter();

const form = ref<PostEditRequest>({
    content: '',
    id: 0,
    title: '',
    tagsStr: '',
    cates: [1],
    thumb: '',
    snippet: '',
    isPublish: true
})

const rules = reactive<FormRules>({
    content: [
        {
            required: true, message: '内容不能为空'
        }
    ],
    title: [
        {
            required: true, message: '标题不能为空'
        }
    ],
    thumb: [{ required: true, message: '缩略图不能为空' }],
    snippet: [{ required: true, message: '摘要不能为空' }, {
        validator: () => {
            return form.value.snippet.length <= 150
        },
        message: '摘要不能大于150个子'
    }]
})

const subLoading = ref(false)

const submit = () => {
    formEl.value?.validate((isvalid, fields) => {
        if (isvalid) {
            console.log(form)
            save()
        } else {
            fields
            ElMessage.error('')
        }
    })
}

const save = async () => {
    subLoading.value = true;
    try {
        await PostService.edit({
            body: form.value
        })
        ElMessage.success('保存成功');
        router.go(-1)
    } catch (error) {
        ElMessage.error('保存失败');
    } finally {
        subLoading.value = false
    }

}


const goBack = () => {
    router.back();
}

const selectThumb = (url: string) => {
    form.value.thumb = url
}

const getCateList = () => {
    CateService.getList({
        index: 1, pageSize: 999
    }).then(res => {
        cateList.value = []
        if (res.data) {
            cateList.value.push(...res.data)
        }
    })
}

onMounted(() => {
    getCateList();
    var id = route.query.id;
    console.log('id:' + id)
    if (id) {
        PostService.get({ id: id as any }).then(res => {
            form.value = res;
        })
    }
})

const route = useRoute();

</script>

<style lang="scss">
.avatar-uploader {
    width: 178px;
    height: 178px;
    display: block;
}

.avatar-uploader {
    border: 1px dashed var(--el-border-color);
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
    transition: var(--el-transition-duration-fast);
}

.avatar-uploader {
    border-color: var(--el-color-primary);
}

.el-icon.avatar-uploader-icon {
    font-size: 28px;
    color: #8c939d;
    width: 178px;
    height: 178px;
    text-align: center;
}
</style>