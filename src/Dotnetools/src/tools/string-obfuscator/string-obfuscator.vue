<script setup lang="ts">
import { useObfuscateString } from './string-obfuscator.model';
import { useCopy } from '@/composable/copy';

const str = ref('Lorem ipsum dolor sit amet');
const keepFirst = ref(4);
const keepLast = ref(4);
const keepSpace = ref(true);

const obfuscatedString = useObfuscateString(str, { keepFirst, keepLast, keepSpace });
const { copy } = useCopy({ source: obfuscatedString });
</script>

<template>
  <div>
    <c-input-text v-model:value="str" raw-text placeholder="Enter string to obfuscate" label="要混淆的字符串:" clearable multiline />

    <div mt-4 flex gap-10px>
      <div>
        <div>前面保留:</div>
        <n-input-number v-model:value="keepFirst" min="0" />
      </div>

      <div>
        <div>后面保留:</div>
        <n-input-number v-model:value="keepLast" min="0" />
      </div>

      <div>
        <div mb-5px>
          保留空格
        </div>
        <n-switch v-model:value="keepSpace" />
      </div>
    </div>

    <c-card v-if="obfuscatedString" mt-60px max-w-600px flex items-center gap-5px font-mono>
      <div break-anywhere text-wrap>
        {{ obfuscatedString }}
      </div>

      <c-button @click="copy()">
        <icon-mdi:content-copy />
      </c-button>
    </c-card>
  </div>
</template>
