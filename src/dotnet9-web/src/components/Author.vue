<template>
    <div class="x-link auth-item" style="padding: 20px 0">
        <div class="auth-avatar">
            <img src="../assets/608144857.jpg">
        </div>
        <div class="auth-info">
            <h3 class="auth-info-name">{{ userName }}</h3>
            <div class="auth-info-statics">
                <span>{{ blogPostsCount }}篇文章</span>
                <a-divider type="vertical" />
                <span>{{ questionsCount }}个问答</span>
            </div>
        </div>        
    </div>
</template>

<script lang="ts">
import { defineComponent, toRefs } from 'vue'

export default defineComponent({
    name: "Author",
    props: {
        userName: {
            type: String,
            default: "",
        },
        blogPostsCount: {
            type: String,
            default: "",
        },
        questionsCount: {
            type: String,
            default: "",
        },
        headPortrait: {
            type: String,
            default: "",
        }
    },
    setup(props: any) {
        toRefs(props);
    }
})
</script>