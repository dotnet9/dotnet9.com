<template>
    <div class="question-item">
        <div class="question-answer">
            <div class="question-answer-num">{{ comments }}</div>
            <div class="question-answer-title">回答</div>
        </div>
        <div class="question-config">
            <h3 class="question-config-title">{{ title }}</h3>
            <a-tag color="pink" v-for="tagName in tagList" :key="tagName">{{ tagName }}</a-tag>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, toRefs } from 'vue'

export default defineComponent({
    name: "Question",
    props: {
        comments: {
            type: Number,
            default: 0,
        },
        title: {
            type: String,
            default: "",
        },
        tag: {
            type: String,
            default: "",
        }
    },
    setup(props: any) {
        let { tag } = toRefs(props);
        let tagList = tag.value.split(",");
        return { tagList };
    },
})
</script>
