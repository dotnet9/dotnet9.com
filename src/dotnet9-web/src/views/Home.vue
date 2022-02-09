<template>
    <div class="home">
        <div 
            class="columns-item columns-item-img"
            v-for="item in blogPostList"
            :key="item.id"
        >
            <div class="columns-config">
                <h3 class="columns-config-title">
                    <a style="display:block;word-break:break-all">{{ item.title }}</a>
                </h3>

                <p class="columns-config-desc">
                    <a style="display:block;word-break:break-all">{{ item.content }}</a>
                </p>

                <div class="columns-config-footer">
                    <span style="margin-right:26px;">{{ item.userName }}</span>
                    <span class="columns-auth-time" title="2022-02-09 21:49">{{ item.createTime }}</span>
                </div>
            </div>

            <a class="columns-img" style="position:relative;overflow:hidden;">
                <img
                    src="../assets/201fdsfs9.jpg"
                    style="
                        position:absolute;
                        top: 50%;
                        left: 0px;
                        width: 100%;
                        transform: translateY(-50%);
                    "
                >
            </a>
        </div>
    </div>
</template>

<script lang="ts">

import { defineComponent, onMounted, ref, reactive } from "vue";
import request from "../api/http";

export default defineComponent({
    name: "Home",
    setup() {
        let blogPostList = ref([]);

        function getBlogPost() {
            request({
                url: "/Home/GetBlogPost",
            }).then((res: any) => {
                blogPostList.value = res.data.response;
            });
        }

        getBlogPost();

        return {
            blogPostList,
        };
    }
});

</script>
