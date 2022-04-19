<template>
    <el-form>
        <el-form-item label="修改密码">
            <el-button type="primary" @click="changePwd()">点击修改密码</el-button>
        </el-form-item>
    </el-form>
    <ChangePwd v-model="showChangePwd"></ChangePwd>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue"
import ChangePwd from "./system/ChangePwd.vue"
export default defineComponent(
    {
        components: { ChangePwd },
        setup() {
            const changePwd = () => {
                showChangePwd.value = !showChangePwd.value
            }

            const showChangePwd = ref(false);


            return {
                showChangePwd, changePwd
            }
        }

    }
)
</script>

<style>
</style>