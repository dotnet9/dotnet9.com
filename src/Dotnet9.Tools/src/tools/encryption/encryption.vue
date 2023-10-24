<script setup lang="ts">
import { AES, RC4, Rabbit, TripleDES, enc } from 'crypto-js';

const algos = { AES, TripleDES, Rabbit, RC4 };

const cypherInput = ref('Lorem ipsum dolor sit amet');
const cypherAlgo = ref<keyof typeof algos>('AES');
const cypherSecret = ref('my secret key');
const cypherOutput = computed(() => algos[cypherAlgo.value].encrypt(cypherInput.value, cypherSecret.value).toString());

const decryptInput = ref('U2FsdGVkX1/EC3+6P5dbbkZ3e1kQ5o2yzuU0NHTjmrKnLBEwreV489Kr0DIB+uBs');
const decryptAlgo = ref<keyof typeof algos>('AES');
const decryptSecret = ref('my secret key');
const decryptOutput = computed(() =>
  algos[decryptAlgo.value].decrypt(decryptInput.value, decryptSecret.value).toString(enc.Utf8),
);
</script>

<template>
  <c-card title="加密">
    <div flex gap-3>
      <c-input-text
        v-model:value="cypherInput"
        label="需要加密的文本:"
        placeholder="这里是文本"
        rows="4"
        multiline raw-text monospace autosize flex-1
      />
      <div flex flex-1 flex-col gap-2>
        <c-input-text v-model:value="cypherSecret" label="密钥:" clearable raw-text />

        <c-select
          v-model:value="cypherAlgo"
          label="加密算法:"
          :options="Object.keys(algos).map((label) => ({ label, value: label }))"
        />
      </div>
    </div>
    <c-input-text
      label="加密后的文本:"
      :value="cypherOutput"
      rows="3"
      placeholder="Your string hash"
      multiline monospace readonly autosize mt-5
    />
  </c-card>
  <c-card title="解密">
    <div flex gap-3>
      <c-input-text
        v-model:value="decryptInput"
        label="需要解密的文本:"
        placeholder="这里是文本"
        rows="4"
        multiline raw-text monospace autosize flex-1
      />
      <div flex flex-1 flex-col gap-2>
        <c-input-text v-model:value="decryptSecret" label="密钥:" clearable raw-text />

        <c-select
          v-model:value="decryptAlgo"
          label="解密算法:"
          :options="Object.keys(algos).map((label) => ({ label, value: label }))"
        />
      </div>
    </div>
    <c-input-text
      label="解密后的文本:"
      :value="decryptOutput"
      placeholder="Your string hash"
      rows="3"
      multiline monospace readonly autosize mt-5
    />
  </c-card>
</template>
