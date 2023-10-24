<script setup lang="ts">
import slugify from '@sindresorhus/slugify';
import { withDefaultOnError } from '@/utils/defaults';
import { useCopy } from '@/composable/copy';

const input = ref('');
const slug = computed(() => withDefaultOnError(() => slugify(input.value), ''));
const { copy } = useCopy({ source: slug, text: '别名已复制到剪贴板' });
</script>

<template>
  <div>
    <c-input-text v-model:value="input" multiline placeholder="这里粘贴字符串 (例如: My file path)" label="需要转别名的字符串" autofocus raw-text mb-5 />

    <c-input-text :value="slug" multiline readonly placeholder="您的别名将在这里生成 (例如: my-file-path)" label="别名" mb-5 />

    <div flex justify-center>
      <c-button :disabled="slug.length === 0" @click="copy()">
        复制别名
      </c-button>
    </div>
  </div>
</template>
