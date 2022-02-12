<template>
    <div class="columns-item columns-item-img">
        <div class="columns-config">
            <h3 class="columns-config-title">
                <a style="display: block; word-break: break-all">{{ title }}</a>
            </h3>

            <p class="columns-config-desc">
                <a style="display: block; word-break: break-all">{{ content }}</a>
            </p>

            <div class="columns-config-footer">
                <span style="margin-right: 26px;">{{ userName }}</span>
                <span class="columns-auth-time" title="2022-02-12 17:47:89">{{ createTime }}</span> 
            </div>
        </div>

        <a class="columns-img" style="position: relative; overflow: hidden">
            <img src="../assets/201fdsfs9.jpg" style="position: absolute; top: 50%; left: 0px; width: 100%; transform: translateY(-50%);">
        </a>
    </div>
</template>

<script lang="ts">

import { defineComponent, toRefs } from "vue"

export default defineComponent({
    name: "BlogPost",
    props: {
        content: {
            type: String,
            default: "",
        },
        title: {
            type: String,
            default: "",
        },
        cover: {
            type: String,
            default: "",
        },
        userName: {
            type: String,
            default: "",
        },
        createTime: {
            type: String,
            default: "",
        },
    },
    setup(props: any) {
        toRefs(props);
    }
});

</script>