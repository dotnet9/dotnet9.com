<script setup lang="ts">
import { textToNatoAlphabet } from './text-to-nato-alphabet.service';
import { useCopy } from '@/composable/copy';

const input = ref('');
const natoText = computed(() => textToNatoAlphabet({ text: input.value }));
const { copy } = useCopy({ source: natoText, text: 'NATO alphabet string copied.' });
</script>

<template>
  <div>
    <c-input-text
      v-model:value="input"
      label="要转换为北约拼音的文本"
      placeholder="这里输入文本..."
      clearable
      mb-5
    />

    <div v-if="natoText">
      <div mb-2>
        北约拼音文本
      </div>
      <c-card>
        {{ natoText }}
      </c-card>

      <div mt-3 flex justify-center>
        <c-button autofocus @click="copy()">
          复制NATO字符串
        </c-button>
      </div>
    </div>
  </div>
</template>
