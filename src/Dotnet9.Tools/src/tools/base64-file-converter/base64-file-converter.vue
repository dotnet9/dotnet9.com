<script setup lang="ts">
import { Upload } from '@vicons/tabler';
import { useBase64 } from '@vueuse/core';
import type { UploadFileInfo } from 'naive-ui';
import type { Ref } from 'vue';
import { useCopy } from '@/composable/copy';
import { useDownloadFileFromBase64 } from '@/composable/downloadBase64';
import { useValidation } from '@/composable/validation';
import { isValidBase64 } from '@/utils/base64';

const base64Input = ref('');
const { download } = useDownloadFileFromBase64({ source: base64Input });
const base64InputValidation = useValidation({
  source: base64Input,
  rules: [
    {
      message: 'Invalid base 64 string',
      validator: value => isValidBase64(value.trim()),
    },
  ],
});

function downloadFile() {
  if (!base64InputValidation.isValid) {
    return;
  }

  try {
    download();
  }
  catch (_) {
    //
  }
}

const fileList = ref();
const fileInput = ref() as Ref<File>;
const { base64: fileBase64 } = useBase64(fileInput);
const { copy: copyFileBase64 } = useCopy({ source: fileBase64, text: 'Base64 string copied to the clipboard' });

async function onUpload({ file: { file } }: { file: UploadFileInfo }) {
  if (file) {
    fileList.value = [];
    fileInput.value = file;
  }
}
</script>

<template>
  <c-card title="Base64转文件">
    <c-input-text
      v-model:value="base64Input"
      multiline
      placeholder="把您的base64文件字符串放在这里..."
      rows="5"
      :validation="base64InputValidation"
      mb-2
    />

    <div flex justify-center>
      <c-button :disabled="base64Input === '' || !base64InputValidation.isValid" @click="downloadFile()">
        下载文件
      </c-button>
    </div>
  </c-card>

  <c-card title="文件转base64">
    <n-upload v-model:file-list="fileList" :show-file-list="true" :on-before-upload="onUpload" list-type="image">
      <n-upload-dragger>
        <div mb-2>
          <n-icon size="35" :depth="3" :component="Upload" />
        </div>
        <div op-60>
          单击或拖动文件到此区域进行上传
        </div>
      </n-upload-dragger>
    </n-upload>

    <c-input-text :value="fileBase64" multiline readonly placeholder="base64的文件字符串将在这里" rows="5" mb-2 />

    <div flex justify-center>
      <c-button @click="copyFileBase64()">
        复制
      </c-button>
    </div>
  </c-card>
</template>

<style lang="less" scoped>
::v-deep(.n-upload-trigger) {
  width: 100%;
}
</style>
