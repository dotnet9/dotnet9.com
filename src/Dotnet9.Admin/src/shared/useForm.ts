import { ElForm, ElMessage, ElMessageBox } from "element-plus";
import { nextTick, onMounted, ref } from "vue";
import { get, post } from "./http/HttpClient";

export function useForm() {

    const loading = ref(false);

    const instance = ref<InstanceType<typeof ElForm>>();

    const clearForm = () => {
        instance.value?.resetFields()
        nextTick(() => {
            instance.value?.clearValidate();
        })
    }

    return {
        loading,
        instance,
        clearForm
    }
}

export function useSettingForm(formModel) {
    const loading = ref(false);

    const instance = ref<InstanceType<typeof ElForm>>();

    const clearForm = () => {
        instance.value?.resetFields()
        nextTick(() => {
            instance.value?.clearValidate();
        })
    }

    const load = async () => {
        loading.value = true
        try {
            var res = await get<any>('/admin/dicdata/get', { groupName: 'wechat' })
            formModel.value = res
        } catch (error) {
            console.error(error)
        } finally {
            loading.value = false
        }
    }
    onMounted(() => {
        load()
    })

    const submit = () => {
        loading.value = true
        console.log(instance.value)
        instance.value?.validate(async (valid) => {
            console.log(valid)
            if (valid) {
                post('/admin/dicdata/update', {
                    groupkey: 'wechat',
                    list: formModel.value
                }).then(()=>{
                    ElMessage.success({
                        message:'更新成功'
                    })
                }).finally(() => loading.value = false)
            }
            else {
                loading.value = false
            }
        })
    }

    return {
        loading,
        instance,
        clearForm,
        submit
    }
}