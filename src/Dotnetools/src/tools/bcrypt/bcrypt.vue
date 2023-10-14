<script setup lang="ts">
import { compareSync, hashSync } from 'bcryptjs';
import { useThemeVars } from 'naive-ui';
import { useCopy } from '@/composable/copy';

const themeVars = useThemeVars();

const input = ref('');
const saltCount = ref(10);
const hashed = computed(() => hashSync(input.value, saltCount.value));
const { copy } = useCopy({ source: hashed, text: 'Hashed string copied to the clipboard' });

const compareString = ref('');
const compareHash = ref('');
const compareMatch = computed(() => compareSync(compareString.value, compareHash.value));
</script>

<template>
  <c-card title="Bcrypt-单向Hash加密">
    <c-input-text
      v-model:value="input"
      placeholder="这里是文本"
      raw-text
      label="需要Hash加密的文本: "
      label-position="left"
      label-width="120px"
      mb-2
    />
    <n-form-item label="循环加盐数: " label-placement="left" label-width="120">
      <n-input-number v-model:value="saltCount" placeholder="Salt rounds..." :max="10" :min="0" w-full />
    </n-form-item>

    <c-input-text :value="hashed" readonly text-center />

    <div mt-5 flex justify-center>
      <c-button @click="copy()">
        复制
      </c-button>
    </div>
  </c-card>

  <c-card title="将字符串与哈希验证">
    <n-form label-width="120">
      <n-form-item label="需要验证的文本: " label-placement="left">
        <c-input-text v-model:value="compareString" placeholder="这里是文本" raw-text />
      </n-form-item>
      <n-form-item label="Hash字符串: " label-placement="left">
        <c-input-text v-model:value="compareHash" placeholder="待比较的hash" raw-text />
      </n-form-item>
      <n-form-item label="验证结果 " label-placement="left" :show-feedback="false">
        <div class="compare-result" :class="{ positive: compareMatch }">
          {{ compareMatch ? '验证成功' : '验证失败' }}
        </div>
      </n-form-item>
    </n-form>
  </c-card>
</template>

<style lang="less" scoped>
.compare-result {
  color: v-bind('themeVars.errorColor');

  &.positive {
    color: v-bind('themeVars.successColor');
  }
}
</style>
