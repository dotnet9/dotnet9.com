<script setup lang="ts">
import { useStorage } from '@vueuse/core';
import { convert } from './list-converter.models';
import type { ConvertOptions } from './list-converter.types';

const sortOrderOptions = [
  {
    label: '升序排列',
    value: 'asc',
    disabled: false,
  },
  {
    label: '降序排列',
    value: 'desc',
    disabled: false,
  },
];

const conversionConfig = useStorage<ConvertOptions>('list-converter:conversionConfig', {
  lowerCase: false,
  trimItems: true,
  removeDuplicates: true,
  keepLineBreaks: false,
  itemPrefix: '',
  itemSuffix: '',
  listPrefix: '',
  listSuffix: '',
  reverseList: false,
  sortList: null,
  separator: ', ',
});

function transformer(value: string) {
  return convert(value, conversionConfig.value);
}
</script>

<template>
  <div style="flex: 0 0 100%">
    <div style="margin: 0 auto; max-width: 600px">
      <c-card>
        <div flex>
          <div>
            <n-form-item label="清空空白字符" label-placement="left" label-width="150" :show-feedback="false" mb-2>
              <n-switch v-model:value="conversionConfig.trimItems" />
            </n-form-item>
            <n-form-item label="去重" label-placement="left" label-width="150" :show-feedback="false" mb-2>
              <n-switch v-model:value="conversionConfig.removeDuplicates" data-test-id="removeDuplicates" />
            </n-form-item>
            <n-form-item
              label="转换为小写"
              label-placement="left"
              label-width="150"
              :show-feedback="false"
              mb-2
            >
              <n-switch v-model:value="conversionConfig.lowerCase" />
            </n-form-item>
            <n-form-item label="保留换行符" label-placement="left" label-width="150" :show-feedback="false" mb-2>
              <n-switch v-model:value="conversionConfig.keepLineBreaks" />
            </n-form-item>
          </div>
          <div flex-1>
            <c-select
              v-model:value="conversionConfig.sortList"
              label="列表排序"
              label-position="left"
              label-width="120px"
              label-align="right"
              mb-2
              :options="sortOrderOptions"
              w-full
              :disabled="conversionConfig.reverseList"
              data-test-id="sortList"
              placeholder="按字母顺序排序"
            />

            <c-input-text
              v-model:value="conversionConfig.separator"
              label="分隔符"
              label-position="left"
              label-width="120px"
              label-align="right"
              mb-2
              placeholder=","
            />

            <n-form-item label="每项前后缀" label-placement="left" label-width="120" :show-feedback="false" mb-2>
              <c-input-text
                v-model:value="conversionConfig.itemPrefix"
                placeholder="每项前缀"
                test-id="itemPrefix"
              />
              <c-input-text
                v-model:value="conversionConfig.itemSuffix"
                placeholder="每项后缀"
                test-id="itemSuffix"
              />
            </n-form-item>
            <n-form-item label="列表前后缀" label-placement="left" label-width="120" :show-feedback="false" mb-2>
              <c-input-text
                v-model:value="conversionConfig.listPrefix"
                placeholder="列表前缀"
                test-id="listPrefix"
              />
              <c-input-text
                v-model:value="conversionConfig.listSuffix"
                placeholder="列表后缀"
                test-id="listSuffix"
              />
            </n-form-item>
          </div>
        </div>
      </c-card>
    </div>
  </div>
  <format-transformer
    input-label="输入列表数据（每项单独成行）"
    input-placeholder="这里输入换行的列表数据..."
    output-label="转换后数据"
    :transformer="transformer"
  />
</template>
