<template>
    <ElDialog v-model="show">
        <div style="width:100%;height:500px">
            <VueCropper ref="cropper" :img="imgData" :outputSize="outputSize" :outputType="type" autoCrop
                :autoCropWidth="width" :autoCropHeight="height" :fixed="fixed" :fixedBox="fixed">
            </VueCropper>
        </div>
        <template #footer>
            <span class="dialog-footer">
                <el-button @click="selectImg()">选择图片</el-button>
                <el-button type="primary" @click="save()">
                    保存
                </el-button>
            </span>
        </template>
        <input type="file" ref="selectFile" multiple="false" hidden @change="selectFileChange($event)" />
    </ElDialog>
</template>

<script lang="ts" setup>
import 'vue-cropper/dist/index.css'
import { VueCropper } from "vue-cropper";
import { ref, watch } from 'vue';
import { ElMessage } from 'element-plus';





interface Size {
    width: number
    height: number
}

const props = defineProps({
    img: String,
    outputSize: {
        type: Number,
        default: 1
    },
    type: {
        type: String,
        default: 'png'
    },
    width: {
        type: Number,
        default: 100
    },
    height: {
        type: Number,
        default: 100
    },
    fixed: { //固定宽高和大小比例
        type: Boolean,
        default: true
    },
    modelValue: {
        type: Boolean,
        default: false
    }
})

const emit = defineEmits(['update:modelValue','on-get-img'])

const imgData = ref(props.img)

const show = ref(props.modelValue)

const cropper = ref<any>(null)

watch(show, () => {

    console.log('对话框变化:' + show.value)
    emit('update:modelValue', show.value)
})

watch(()=>props.modelValue,()=>{
    show.value = props.modelValue;
})

const save = () => {
    cropper.value?.getCropBlob((data: Blob) => {
        // do something
        console.log(data)
        if (data.size > 0) {
            uploadServer(data, getFileName(data.type),()=>{
                //上传成功
                emit('on-get-img',serverUrl.value)
                emit('update:modelValue',false)
            })
        } else {
            ElMessage({
                type: 'error', message: '请选择图片'
            })
        }
    })
}
const selectFile = ref<HTMLInputElement>();
const selectImg = () => {
    selectFile.value?.click()
}

const typeList = ['image/png', 'image/jpg', 'image/jpeg']

const getFileName = (type: string) => {
    switch (type) {
        case 'image/png':
            return "1.png"
        case 'image/jpg':
            return "1.jpg"
        case 'image/jpeg':
            return "1.jpeg";
    }
    return "1.jpg";
}

const selectFileChange = (e: Event) => {
    var el = (e.target as HTMLInputElement)
    if (el.files) {
        let file = el.files[0];
        console.log(file)
        if (typeList.indexOf(file.type) == -1) {
            ElMessage({
                message: '图片只能选择 png jpg jpeg文件',
                type: 'warning'
            })
            return;
        }
        let fileReader = new FileReader();
        fileReader.addEventListener(
            "load",
            (event) => {
                console.log(event);
                if (event.target?.result) {
                    imgData.value = event.target.result.toString();
                }
            },
            false
        );
        fileReader.readAsDataURL(file);
    }
}

const serverUrl = ref('')

const uploadServer = (file: any, name: string, succ: Function) => {
    var form = new FormData();
    form.append('img', file, name);
    var xhr = new XMLHttpRequest();
    xhr.open("post", "/admin/common/upload");
    xhr.onreadystatechange = () => {
        if (xhr.readyState == 4 && xhr.status == 200) {
            var res = JSON.parse(xhr.responseText);
            console.log("上传成功");
            serverUrl.value = res.list[0]
            succ();
        }
    };
    xhr.upload.onprogress = (event) => {
        if (event.lengthComputable) {
            var percent = (event.loaded / event.total) * 100;
            console.log("上传进度:" + percent);
        }
    };
    xhr.onerror = () => {
        console.error("上传文件错误");
        ElMessage({
            type: 'error',
            message: '上传文件错误！'
        })
    };
    xhr.ontimeout = () => {
        console.error("上传超时");
        ElMessage({
            type: 'error',
            message: '上传超时！'
        })
    };
    xhr.send(form);
};

</script>