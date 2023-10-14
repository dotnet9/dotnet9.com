<script setup lang="ts">
import { escape, unescape } from 'lodash';

import { useCopy } from '@/composable/copy';

const escapeInput = ref('<title>Dotnet工具箱</title>');
const escapeOutput = computed(() => escape(escapeInput.value));
const { copy: copyEscaped } = useCopy({ source: escapeOutput });

const unescapeInput = ref('&lt;title&gt;Dotnet工具箱&lt;/title&gt;');
const unescapeOutput = computed(() => unescape(unescapeInput.value));
const { copy: copyUnescaped } = useCopy({ source: unescapeOutput });
</script>

<template>
  <c-card title="html元素转义">
    <n-form-item label="待转义字符串:">
      <c-input-text
        v-model:value="escapeInput"
        multiline
        placeholder="待转义字符串"
        rows="3"
        autosize
        raw-text
      />
    </n-form-item>

    <n-form-item label="已转义字符串:">
      <c-input-text
        multiline
        readonly
        placeholder="已转义字符串"
        :value="escapeOutput"
        rows="3"
        autosize
      />
    </n-form-item>

    <div flex justify-center>
      <c-button @click="copyEscaped()">
        复制
      </c-button>
    </div>
  </c-card>
  <c-card title="取消html元素转义">
    <n-form-item label="已转义字符串:">
      <c-input-text
        v-model:value="unescapeInput"
        multiline
        placeholder="需要取消转义的字符串"
        rows="3"
        autosize
        raw-text
      />
    </n-form-item>

    <n-form-item label="已取消转义的字符串:">
      <c-input-text
        :value="unescapeOutput"
        multiline
        readonly
        placeholder="已取消转义的字符串"
        rows="3"
        autosize
      />
    </n-form-item>

    <div flex justify-center>
      <c-button @click="copyUnescaped()">
        复制
      </c-button>
    </div>
  </c-card>
</template>
