<template>
    <div class="login-wrapper"></div>
    <div class="login-page">
        <div class="login-box" v-loading="loading">
            <el-form :model="loginModel" label-width="" :rules="loginRule" ref="form" @submit.native.prevent
                label-position="top">
                <el-form-item label="用户名" prop="userName">
                    <el-input placeholder="输入用户名" v-model="loginModel.userName"></el-input>
                </el-form-item>
                <el-form-item label="密码" prop="pwd">
                    <el-input placeholder="输入密码" type="password" v-model="loginModel.pwd"></el-input>
                </el-form-item>
                <el-form-item label="验证码" prop="validCode">
                    <el-input style="width: 120px;margin-right: 8px;" v-model="loginModel.validCode"></el-input>
                    <img width="90" height="30" :src="captchaUrl + '&t=' + nowTime" title="点击刷新验证码"
                        @click="refreshValidateCode()" style="cursor: pointer;">
                </el-form-item>
                <el-form-item>
                    <el-button native-type="submit" @click="submit()" type="primary" class="login-btn">
                        登录
                    </el-button>
                    <el-row style="width: 100%;">
                        <el-col :span="12">

                        </el-col>
                        <el-col :span="12" style="text-align: right;">
                            <router-link to="/forget-pwd">
                                <el-link>忘记密码</el-link>
                            </router-link>
                        </el-col>

                    </el-row>
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


const captchaUrl = '/admin/common/getcaptcha?type=1';

const nowTime = ref('1');

const refreshValidateCode = () => {
    nowTime.value = new Date().getTime().toString();
}


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
    ],
    validCode: [
        {
            required: true, message: '验证码不能为空'
        }
    ]
}

// const router = useRouter();

const submit = () => {

    form.value?.validate((isvalid => {
        if (isvalid) {
            loading.value = true
            AuthService.login({ body: loginModel }).then(res => {
                vsetting.setToken(res.token!)
                ElMessage({
                    message: '登录成功',
                    type: 'success'
                })
                AdminRouter.replace('/admin')
            }).finally(() => {
                refreshValidateCode()
                loginModel.validCode = ''
                loading.value = false
                form.value?.resetFields(['validCode'])
            })

        } else {
            console.log('验证失败')
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