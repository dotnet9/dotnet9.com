<script setup lang="ts">
import InputCopyable from '../../components/InputCopyable.vue';
import { isNotThrowing } from '@/utils/boolean';
import { withDefaultOnError } from '@/utils/defaults';

const urlToParse = ref('https://dotnet9:123456@dotnetools.com:3000/url-parser?key1=value&key2=value2#the-hash');

const urlParsed = computed(() => withDefaultOnError(() => new URL(urlToParse.value), undefined));
const urlValidationRules = [
  {
    validator: (value: string) => isNotThrowing(() => new URL(value)),
    message: 'Invalid url',
  },
];

const properties: { title: string; key: keyof URL }[] = [
  { title: '协议-Protocol', key: 'protocol' },
  { title: '用户名-Username', key: 'username' },
  { title: '密码-Password', key: 'password' },
  { title: '域名-Hostname', key: 'hostname' },
  { title: '端口-Port', key: 'port' },
  { title: '路径-Path', key: 'pathname' },
  { title: '参数-Params', key: 'search' },
];
</script>

<template>
  <c-card>
    <c-input-text
      v-model:value="urlToParse"
      label="需要分析的Url:"
      placeholder="需要分析的Url..."
      raw-text
      :validation-rules="urlValidationRules"
    />

    <n-divider />

    <InputCopyable
      v-for="{ title, key } in properties"
      :key="key"
      :label="title"
      :value="(urlParsed?.[key] as string) ?? ''"
      readonly
      label-position="left"
      label-width="110px"
      mb-2
      placeholder=" "
    />

    <div
      v-for="[k, v] in Object.entries(Object.fromEntries(urlParsed?.searchParams.entries() ?? []))"
      :key="k"
      mb-2
      w-full
      flex
    >
      <div style="flex: 1 0 110px">
        <icon-mdi-arrow-right-bottom />
      </div>

      <InputCopyable :value="k" readonly />
      <InputCopyable :value="v" readonly />
    </div>
  </c-card>
</template>

<style lang="less" scoped>
.n-input-group-label {
  text-align: right;
}
.n-input-group {
  margin: 2px 0;
}
</style>
