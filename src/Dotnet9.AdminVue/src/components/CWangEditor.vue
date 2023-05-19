<template>
    <div style="border: 1px solid #ccc;width: 100%;z-index: 999;">
        <Toolbar style="border-bottom: 1px solid #ccc" :editor="editorRef" :defaultConfig="toolbarConfig"
            mode="simple" />
        <Editor style="height: 500px; overflow-y: hidden;" v-model="valueHtml" :defaultConfig="editorConfig"
            @customAlert="customAlert" mode="simple" @onCreated="handleCreated" @onBlur="onBlur" @onChange="onChange()" />
    </div>
</template>

<script lang="ts" setup>
import '@wangeditor/editor/dist/css/style.css' // 引入 css
import { onBeforeUnmount, ref, shallowRef, onMounted, watch } from 'vue'
import { Editor, Toolbar } from '@wangeditor/editor-for-vue'
import { IEditorConfig, IToolbarConfig } from '@wangeditor/core';
import { ElMessage } from 'element-plus';
type InsertFnType = (url: string, alt: string, href: string) => void

const emit = defineEmits(['update:modelValue'])
const props = defineProps({
    modelValue: String
});

// 编辑器实例，必须用 shallowRef
const editorRef = shallowRef()

// 内容 HTML
const valueHtml = ref(props.modelValue)

watch(() => props.modelValue, () => {
    valueHtml.value = props.modelValue;
})

onMounted(() => {
    // console.log('配置上传图片')

})

const toolbarConfig: Partial<IToolbarConfig> = {

}

const onChange = ()=>{
    emit('update:modelValue', valueHtml.value)
}


const editorConfig: Partial<IEditorConfig> = {
    placeholder: '请输入内容...'
    , MENU_CONF: {
        uploadImage:
        {
            server: '/admin/common/upload',
            fieldName: 'file',
            // 选择文件时的类型限制，默认为 ['image/*'] 。如不想限制，则设置为 []
            allowedFileTypes: ['image/*'],
            // 自定义插入图片
            customInsert(res: any, insertFn: InsertFnType) {  // TS 语法
                // customInsert(res, insertFn) {                  // JS 语法
                // res 即服务端的返回结果
                if (res.list && res.list.length > 0) {
                    for (let i = 0; i < res.list.length; i++) {
                        insertFn(res.list[i], '', '')
                    }
                }
                // insertFn(url, alt, href)
                // 从 res 中找到 url alt href ，然后插入图片

            },
        }
    }
}
// if (editorConfig.MENU_CONF) {
//     editorConfig.MENU_CONF['uploadImage'] =
// }

const customAlert = (s: string, t: string) => {
    switch (t) {
        case 'success':
            ElMessage.success(s)
            break
        case 'info':
            ElMessage.info(s)
            break
        case 'warning':
            ElMessage.warning(s)
            break
        case 'error':
            ElMessage.error(s)
            break
        default:
            ElMessage.info(s)
            break
    }
}



// 组件销毁时，也及时销毁编辑器
onBeforeUnmount(() => {
    const editor = editorRef.value
    if (editor == null) return
    editor.destroy()
})

const handleCreated = (editor: any) => {
    editorRef.value = editor // 记录 editor 实例，重要！
}

const onBlur = () => {
    emit('update:modelValue', valueHtml.value)
}





// console.log(props.modelValue)

if (props.modelValue) {
    valueHtml.value = props.modelValue;
}



</script>