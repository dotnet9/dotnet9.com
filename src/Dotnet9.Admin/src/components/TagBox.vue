
<template>
    <ul class="tag-box">
        <el-tag
            v-for="(tag,index) in tags"
            :key="index"
            closable
            size="medium"
            type="success"
            @close="close(index)"
        >{{ tag }}</el-tag>
        <li>
            <el-input placeholder="输入标签" @keydown.enter="enter()" v-model="v" v-if="tags.length<=5"></el-input>
        </li>
    </ul>
</template>

<script lang="ts">
import { defineComponent, PropType, ref, watch } from 'vue'
export default defineComponent({
    name: 'tag-box',
    props: {
        modelValue: {
            type: Array as PropType<string[]>,
            required: true,
            default:[]
        }
    },
    emits: ["update:modelValue"],
    setup(props, context) {

        const tags = ref<string[]>(props.modelValue)

        const v = ref('')

        const enter = () => {
            if (tags.value.indexOf(v.value) == -1 && v.value) {
                tags.value.push(v.value)
                v.value = ''
                context.emit('update:modelValue', tags.value)
            }
            else{
                console.log('enter')
            }
        }

        watch(()=>props.modelValue,()=>{
            tags.value.length = 0
            tags.value.push(...props.modelValue)
        })

        const close = (index: number) => {
            tags.value.splice(index, 1)
            context.emit('update:modelValue', tags.value)
        }

        return {
            tags,
            enter,
            v,
            close
        }
    }
})
</script>

<style lang="scss">
.tag-box {
    list-style: none;
    display: flex;
    flex-direction: row;
    padding-left: 0;
    flex-wrap: wrap;
    align-content: center;
    .el-tag {
        margin-right: 15px;
        margin-bottom: 8px;
    }
}
</style>