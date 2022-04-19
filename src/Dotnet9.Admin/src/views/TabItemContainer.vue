
<template>
    <el-card>
        <template #header></template>
        <router-view></router-view>
    </el-card>
</template>

<script lang="ts">
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
export default {
    setup() {
        const title = ref('')
        const router = useRouter();
        router.afterEach(a => {
            if (a.name) {
                title.value = a.name.toString()
            }
        })


        onMounted(() => {
            let name = router.currentRoute.value.name;
            if (name) {
                title.value = name.toString()
            }
        })

        return {
            title
        }
    }
}
</script>

<style>
</style>