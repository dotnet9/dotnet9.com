<template>

    <div class="login-wrapper"></div>
    <div class="login-page">
        <div class="login-box" v-loading="loading">
            <el-form :model="loginModel" label-width="" :rules="loginRule" ref="form" @submit.native.prevent>
                <el-form-item label="" prop="userName">
                    <el-input placeholder="输入用户名" v-model="loginModel.userName"></el-input>
                </el-form-item>
                <el-form-item label="" prop="passWord">
                    <el-input placeholder="输入密码" type="password" v-model="loginModel.pwd"></el-input>
                </el-form-item>
                <el-form-item>
                    <el-button native-type="submit" @click="submit()" type="primary" class="login-btn">
                        登录
                    </el-button>
                </el-form-item>
            </el-form>
        </div>
    </div>
</template>

<script setup lang="ts">
import { FormRules, FormInstance, ElMessage } from 'element-plus';
import { reactive, ref } from 'vue';

import { useVSetting } from "@/store/VSetting";

import { AuthService, LoginModel } from "@/shared/service";

import { AdminRouter } from "@/router/AdminRouter"

const vsetting = useVSetting();


const loginModel = reactive<LoginModel>({
    userName: '',
    pwd: '',
    validCode: ''
})

const loading = ref(false)

const form = ref<FormInstance>();

const loginRule: FormRules = {
    userName: [
        {
            required: true, message: "用户名不能为空"
        }
    ],
    pwd: [
        {
            required: true, message: "密码不能为空"
        }
    ]
}

// const router = useRouter();

const submit = () => {
    loading.value = true
    form.value?.validate((isvalid => {
        if (isvalid) {
            AuthService.login({ body: loginModel }).then(res => {
                vsetting.setToken(res.token!)
                ElMessage({
                    message: '登录成功',
                    type: 'success'
                })
                AdminRouter.replace('/admin')
            }).finally(() => {
                loading.value = false
            })

        } else {
            console.log('验证失败')
            setTimeout(() => {
                loading.value = false
            }, 3000);
        }


    }))
}

</script>

<style lang="less">
.login-box {
    width: 360px;
    background-color: white;
    z-index: 100;
    padding: 20px;
    box-sizing: border-box;
    border-radius: 10px;
    border: 1px solid #e2e2e2;
    padding-top: 50px;
    margin: 0 auto;
    margin-top: 120px;
}

.login-btn {
    width: 100%;
}

.login-page {
    width: 100%;
    height: 100%;
    box-sizing: border-box;
}

.login-wrapper {
    position: fixed;
    left: 0;
    right: 0;
    bottom: 0;
    top: 0;
    z-index: -1;
    background-image: url("../../assets/air-bg.png");
}
</style>