<template>
    <ElCard>
        <template #header>
            测试表单:{{ testBool }}
        </template>
        <ElButton @click="testHandler()">test</ElButton>
        <Test :items="controlItems"></Test>
        <ElInput v-model="name"></ElInput>
    </ElCard>
</template>

<script lang="ts" setup>
import { BaseOption, SimpleFormModel, InputOption, SelectOption } from '@/components/SimpleFormModel';
import SimpleForm from '@/components/SimpleForm.vue';
import { reactive, ref } from 'vue';
import Test from '@/components/test'

import { onActivated, onDeactivated } from 'vue';
const testBool = ref(false)

const name = ref('')

const formDirection = ref<'v' | 'h'>('h')

const select = new SelectOption();
select.items = [{
    label: '测试', value: '测试的值'
}]
const controlItems = reactive<BaseOption[]>([
    new InputOption(), select
])


onDeactivated(() => {
    console.log('form:onDeactivated')
})

onActivated(() => {
    console.log('form:onActivated')
})



// const formList = reactive<SimpleFormModel[]>([
//     {
//         label: '测试', value: '', placeholder: '输入账号', id: 'userName',
//         option: { type: 'year', format: '', control: 'input' }
//     }
// ])



const testHandler = () => {
    // formList.filter(a => a.label == '选择项')[0].selectOption?.items.push({
    //     label: '选择4', value: 4
    // })
    formDirection.value = formDirection.value == 'v' ? 'h' : 'v'

    console.log('更新了')
}

// const formOption = reactive({
//     items: formList,
//     rules: {
//         userName: {
//             required: true,
//             message: '用户名不能为空'
//         }
//     }
// })



const okHandler = (params: any) => {
    console.log('pramas', params)
}

</script>