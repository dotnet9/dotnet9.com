<script setup lang="ts">
import { convertAsciiBinaryToText, convertTextToAsciiBinary } from './text-to-binary.models';
import { withDefaultOnError } from '@/utils/defaults';
import { useCopy } from '@/composable/copy';
import { isNotThrowing } from '@/utils/boolean';

const inputText = ref('');
const binaryFromText = computed(() => convertTextToAsciiBinary(inputText.value));
const { copy: copyBinary } = useCopy({ source: binaryFromText });

const inputBinary = ref('');
const textFromBinary = computed(() => withDefaultOnError(() => convertAsciiBinaryToText(inputBinary.value), ''));
const inputBinaryValidationRules = [
  {
    validator: (value: string) => isNotThrowing(() => convertAsciiBinaryToText(value)),
    message: 'Binary should be a valid ASCII binary string with multiples of 8 bits',
  },
];
const { copy: copyText } = useCopy({ source: textFromBinary });
</script>

<template>
  <c-card title="文本 转 ASCII 二进制">
    <c-input-text v-model:value="inputText" multiline placeholder="例如 'Hello world'" label="输入文本转二进制" autosize autofocus raw-text test-id="text-to-binary-input" />
    <c-input-text v-model:value="binaryFromText" label="转换后的二进制" multiline raw-text readonly mt-2 placeholder="文本的二进制表示将在此处" test-id="text-to-binary-output" />
    <div mt-2 flex justify-center>
      <c-button :disabled="!binaryFromText" @click="copyBinary()">
        复制 二进制 到剪贴板
      </c-button>
    </div>
  </c-card>

  <c-card title="ASCII 二进制 to 转文本">
    <c-input-text v-model:value="inputBinary" multiline placeholder="例如 '01001000 01100101 01101100 01101100 01101111'" label="输入二进制转文本" autosize raw-text :validation-rules="inputBinaryValidationRules" test-id="binary-to-text-input" />
    <c-input-text v-model:value="textFromBinary" label="转换后的文本" multiline raw-text readonly mt-2 placeholder="二进制的文本表示将在此处" test-id="binary-to-text-output" />
    <div mt-2 flex justify-center>
      <c-button :disabled="!textFromBinary" @click="copyText()">
        复制 文本 到剪贴板
      </c-button>
    </div>
  </c-card>
</template>
