<script setup lang="ts">
import { useHead } from '@vueuse/head';
import { useTracker } from '@/modules/tracker/tracker.services';

useHead({ title: 'About - IT Tools' });
const { tracker } = useTracker();
</script>

<template>
  <div class="about-page">
    <n-h1>关于Dotnet工具箱</n-h1>
    <n-p>
      参考开源项目
      <c-link href="https://github.com/CorentinTh/it-tools" rel="noopener" target="_blank">
        it-tools
      </c-link>搭建，感谢开源的力量。
    </n-p>
    <n-p>
      这是一个很棒的网站，由
      <c-link href="https://github.com/CorentinTh" target="_blank" rel="noopener">
        Corentin Thomasset
      </c-link>开源分享，站长再参考搭建、修改、个性化完善部署。
      Dotnet工具箱为开发人员和IT工作者收集了有用的工具。如果你觉得它有用，请随时将它分享给你认为可能也有用的人，别忘了在快捷栏中添加书签！
    </n-p>
    <n-p>
      Dotnet工具箱是开源的（基于MIT协议）和免费的，而且永远都是，但托管和续订域名需要我花钱。如果你想支持我的工作，并鼓励我添加更多工具，请考虑
      <c-link
        href="https://dotnet9.com/donation"
        rel="noopener"
        target="_blank"
        @click="() => tracker.trackEvent({ eventName: 'Support button clicked' })"
      >
        打赏我
      </c-link>。
    </n-p>

    <n-h2>开发技术栈</n-h2>
    <n-p>
      Dotnet工具箱是用Vue.js（Vue3）和Naive UI组件库制作的，由华为云托管。一些工具中使用了第三方开源库，您可以在存储库的
      <c-link href="https://github.com/dotnet9/Dotnet9/blob/develop/src/Dotnetools/package.json" rel="noopener" target="_blank">
        package.json
      </c-link>
      文件中找到完整的列表。
    </n-p>

    <n-h2>Dotnet工具箱</n-h2>
    <n-p>
      如果您需要一个目前不在这里的工具，并且您认为它可能有用，欢迎您在GitHub存储库的
      <c-link
        href="https://github.com/dotnet9/Dotnet9/issues/new/choose"
        rel="noopener"
        target="_blank"
      >
        issues区
      </c-link>
      提交功能请求。
    </n-p>
    <n-p>
      如果你发现了一个bug，或者有些东西没有按预期工作，请在GitHub存储库的
      <c-link
        href="https://github.com/dotnet9/Dotnet9/issues/new/choose"
        rel="noopener"
        target="_blank"
      >
        issues区
      </c-link>
      提交一份bug报告。
    </n-p>
  </div>
</template>

<style scoped lang="less">
.about-page {
  max-width: 600px;
  margin: 50px auto;
  box-sizing: border-box;

  .n-h2 {
    margin-bottom: 0px;
  }

  .n-p {
    text-align: justify;
  }
}
</style>
