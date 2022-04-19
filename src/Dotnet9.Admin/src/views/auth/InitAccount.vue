<template>
    <div class="q-login">
        <div class="q-login-box" v-loading="loading">
            <div class="q-login-header">
                <h1>初始化后台账号</h1>
            </div>
            <el-form
                :model="formData"
                label-width="70px"
                :rules="rules"
                ref="form"
                label-position="top"
            >
                <el-form-item label="用户名" prop="userName">
                    <el-input placeholder="输入用户名" v-model="formData.userName"></el-input>
                </el-form-item>
                <el-form-item label="邮箱" prop="email">
                    <el-input placeholder="输入用户名" v-model="formData.email"></el-input>
                </el-form-item>
                <el-form-item label="密码" prop="pwd">
                    <el-input placeholder="输入密码" v-model="formData.pwd" show-password></el-input>
                </el-form-item>
                <el-form-item label>
                    <el-button type="primary" class="w_100" @click="submitHandler()">创建</el-button>
                </el-form-item>
            </el-form>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { reactive, ref,onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { ElForm,ElMessage } from 'element-plus'
import { post,get } from 'shared/http/HttpClient'


const formData = reactive({
    userName: "",
    pwd: "",
    email: ''
});
const loading = ref<boolean>(false);
const rules = {
    email: [
        {
            required: true,
            message: '邮箱不能为空',
            trigger: 'blur'
        }
    ],
    userName: [
        {
            required: true,
            message: "用户名不能为空",
            trigger: 'blur'
        },
    ],
    pwd: [
        {
            required: true,
            message: "密码不能为空",
            trigger: 'blur'
        },
    ],
};
const form = ref<InstanceType<typeof ElForm>>();

const router = useRouter();

const submitHandler = () => {
    form.value.validate((isvalid) => {
        if (isvalid) {
            loading.value = true;
            post('/admin/createAdminAccount', formData).then(_ => {
                ElMessage({
                    message:'初始化管理员账号成功',
                    type:'success'
                })
                router.replace('/login')
            }).finally(() => loading.value = false)
        }
    })

}

onMounted(() => {
    get('/admin/account/islogin',{})
})

</script>