<template>
    <div class="page-404">
        <img class="img-404" :src="img_404" />
        <ElButton @click="back()">返回上一页</ElButton>
    </div>
</template>

<script lang="ts" setup>
import { useRouter } from "vue-router";
import img_404 from "../../assets/404.svg";

const router = useRouter();

const back = () => {
    router.go(-1)
}

</script>

<style lang="scss">
.page-404{
    display: flex;
    flex-direction: column;
    align-items: center;
}
.img-404 {
    width: 600px;
    margin: 0 auto;
}
</style>