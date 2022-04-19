<template>
    <el-form
        label-width="100px"
        label-position="top"
        ref="instance"
        :model="formModel"
        :rules="rules"
        v-loading="loading"
    >
        <el-form-item label="关注公众号说明" prop="wechat_official_account_desc">
            <el-input
                type="textarea"
                placeholder="公众号关注的卡片的说明，支持html标签"
                v-model="formModel.wechat_official_account_desc"
            ></el-input>
        </el-form-item>
        <el-form-item label="关注公众号二维码" prop="wechat_official_account_pic">
            <el-button
                type="default"
                @click="selectQr()"
                v-if="!formModel.wechat_official_account_pic"
            >选择图片</el-button>
            <img
                :src="formModel.wechat_official_account_pic"
                style="max-width: 200px;"
                @click="selectQr()"
                v-if="formModel.wechat_official_account_pic"
            />
        </el-form-item>
        <el-form-item>
            <el-button type="primary" @click="submit()">保存</el-button>
        </el-form-item>
    </el-form>

    <cropper-box v-model="showCropper" @cropper="cropperSelect"></cropper-box>
</template>
<script lang="ts" setup>

import { ref } from 'vue';
import { ElLoading } from 'element-plus'
import { post } from "shared/http/HttpClient";
import { useSettingForm } from 'shared/useForm';

const formModel = ref({
    wechat_official_account_desc: '',
    wechat_official_account_pic: ''
})

const rules = {

}


const { instance, loading, submit } = useSettingForm(formModel);

const showCropper = ref(false)

const coverBase64 = ref<string>("")

const cropperSelect = (data: { base64: string }) => {
    coverBase64.value = data.base64;
    let loading = ElLoading.service({
        text: '上传图片中'
    })
    post('/uploadbase64', {
        base64: data.base64
    }).then((res: any) => {
        console.log(res)
        formModel.value.wechat_official_account_pic = res.url
    }).finally(() => {
        loading.close();
    })
}

const selectQr = () => {
    showCropper.value = true
}

</script>