<template>
    <el-dialog v-model="visible" destroy-on-close @close="close()" title="图片剪切" @open="open()">
        <div class="cropper-box">
            <div class="operating-area">
                <img class="imgview" ref="el" :src="url" />
            </div>
            <input
                class="file-selector"
                placeholder="选择图片"
                type="file"
                @change="selectFile"
                ref="fileSelector"
            />
            <div class="preview-box">
                <div class="preview" ref="preview"></div>
                <div>
                    <el-button @click="select()">选择图片</el-button>
                    <el-button type="primary" @click="save()">保存图片</el-button>
                </div>
            </div>
        </div>
    </el-dialog>
</template>

<script lang="ts">
import Cropper from "cropperjs"
import "cropperjs/dist/cropper.min.css"
import { defineComponent, nextTick, onMounted, ref, watch } from 'vue';
export default defineComponent(
    {
        name: 'cropper-box',
        props: {
            visible: {
                type: Boolean
            },
            url: {
                type: String
            },
            width: {
                type: Number,
                default: 300
            },
            height: {
                type: Number,
                default: 300
            }
        },
        emits: ['cropper', 'update:modelValue'],
        setup(props, context) {
            const el = ref<HTMLImageElement>();
            const preview = ref<HTMLDivElement>();

            const visible = ref(props.visible)

            const url = ref(props.url)

            let cropper: Cropper;

            onMounted(() => {
                nextTick(() => initCropper())
            })

            const initCropper = () => {
                if (el.value) {
                    cropper = new Cropper(el.value, {
                        preview: preview.value,
                        checkCrossOrigin: true,
                        aspectRatio: 5 / 5,
                    })
                }
            }

            watch(() => props.visible, () => {
                console.log(visible.value, props.visible)
                visible.value = props.visible
                nextTick(() => {
                    initCropper()
                })
            })

            watch(() => props.url, () => {
                url.value = props.url
            })



            const selectFile = (e: any) => {
                let files = e.target.files;
                console.log(files);
                for (let i = 0; i < files.length; i++) {
                    let file = files[i];
                    var fileReader = new FileReader();
                    fileReader.addEventListener(
                        "load",
                        (event) => {
                            console.log(event);
                            url.value = String(event.target!.result)
                            cropper.clear()
                            cropper.replace(url.value)
                        },
                        false
                    );
                    fileReader.readAsDataURL(file);
                }
            }

            const close = () => {
                // console.log('执行close')
                context.emit('update:modelValue', false)

            }

            const save = () => {
                // console.log('执行close')
                let base64 = cropper.getCroppedCanvas().toDataURL("image/jpeg",0.3)

                context.emit('update:modelValue', false)
                context.emit('cropper', { base64: base64 })
            }

            const fileSelector = ref<HTMLElement>();

            const select = () => {
                fileSelector.value?.click();
            }

            const open = () => {
                console.log('open')
                nextTick(() => {
                    initCropper();
                })
            }

            return {
                el,
                preview,
                selectFile,
                visible,
                close,
                url,
                fileSelector,
                select,
                save,
                open
            }
        }
    }
)
</script>

<style scoped lang="scss">
.imgview {
    width: 400px;
    min-height: 300px;
    display: block;
}
.preview {
    height: 100px;
    width: 100px;
    border: 1px solid #ccc;
    overflow: hidden;
}
.operating-area {
    display: flex;
    flex-direction: column;
    align-items: flex-end;
}

.file-selector {
    display: none;
}
.preview-box {
    margin-top: 10px;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: flex-end;
}
</style>