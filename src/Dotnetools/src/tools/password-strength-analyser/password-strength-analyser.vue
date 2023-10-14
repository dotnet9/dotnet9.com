<script setup lang="ts">
import { getPasswordCrackTimeEstimation } from './password-strength-analyser.service';

const password = ref('');
const crackTimeEstimation = computed(() => getPasswordCrackTimeEstimation({ password: password.value }));

const details = computed(() => [
  {
    label: '密码长度:',
    value: crackTimeEstimation.value.passwordLength,
  },
  {
    label: 'Entropy:',
    value: Math.round(crackTimeEstimation.value.entropy * 100) / 100,
  },
  {
    label: '字符集大小:',
    value: crackTimeEstimation.value.charsetLength,
  },
  {
    label: '分数:',
    value: `${Math.round(crackTimeEstimation.value.score * 100)} / 100`,
  },
]);
</script>

<template>
  <div flex flex-col gap-3>
    <c-input-text
      v-model:value="password"
      type="password"
      placeholder="输入密码..."
      clearable
      autofocus
      raw-text
      test-id="password-input"
    />

    <c-card text-center>
      <div op-60>
        使用暴力破解此密码的持续时间
      </div>
      <div text-2xl data-test-id="crack-duration">
        {{ crackTimeEstimation.crackDurationFormatted }}
      </div>
    </c-card>
    <c-card>
      <div v-for="({ label, value }) of details" :key="label" flex gap-3>
        <div flex-1 text-right op-60>
          {{ label }}
        </div>
        <div flex-1 text-left>
          {{ value }}
        </div>
      </div>
    </c-card>
    <div op-70>
      <span font-bold>备注: </span>
      计算的强度是基于使用暴力方法破解密码所需的时间，它没有考虑字典攻击的可能性。
    </div>
  </div>
</template>
