<script setup lang="ts">
import cronstrue from 'cronstrue';
import { isValidCron } from 'cron-validator';
import { useStyleStore } from '@/stores/style.store';

function isCronValid(v: string) {
  return isValidCron(v, { allowBlankDay: true, alias: true, seconds: true });
}

const styleStore = useStyleStore();

const cron = ref('40 * * * *');
const cronstrueConfig = reactive({
  verbose: true,
  dayOfWeekStartIndexZero: true,
  use24HourTimeFormat: true,
  throwExceptionOnParseError: true,
});

const helpers = [
  {
    symbol: '*',
    meaning: 'Any value',
    example: '* * * *',
    equivalent: 'Every minute',
  },
  {
    symbol: '-',
    meaning: 'Range of values',
    example: '1-10 * * *',
    equivalent: 'Minutes 1 through 10',
  },
  {
    symbol: ',',
    meaning: 'List of values',
    example: '1,10 * * *',
    equivalent: 'At minutes 1 and 10',
  },
  {
    symbol: '/',
    meaning: 'Step values',
    example: '*/10 * * *',
    equivalent: 'Every 10 minutes',
  },
  {
    symbol: '@yearly',
    meaning: 'Once every year at midnight of 1 January',
    example: '@yearly',
    equivalent: '0 0 1 1 *',
  },
  {
    symbol: '@annually',
    meaning: 'Same as @yearly',
    example: '@annually',
    equivalent: '0 0 1 1 *',
  },
  {
    symbol: '@monthly',
    meaning: 'Once a month at midnight on the first day',
    example: '@monthly',
    equivalent: '0 0 1 * *',
  },
  {
    symbol: '@weekly',
    meaning: 'Once a week at midnight on Sunday morning',
    example: '@weekly',
    equivalent: '0 0 * * 0',
  },
  {
    symbol: '@daily',
    meaning: 'Once a day at midnight',
    example: '@daily',
    equivalent: '0 0 * * *',
  },
  {
    symbol: '@midnight',
    meaning: 'Same as @daily',
    example: '@midnight',
    equivalent: '0 0 * * *',
  },
  {
    symbol: '@hourly',
    meaning: 'Once an hour at the beginning of the hour',
    example: '@hourly',
    equivalent: '0 * * * *',
  },
  {
    symbol: '@reboot',
    meaning: 'Run at startup',
    example: '',
    equivalent: '',
  },
];

const cronString = computed(() => {
  if (isCronValid(cron.value)) {
    return cronstrue.toString(cron.value, cronstrueConfig);
  }
  return ' ';
});

const cronValidationRules = [
  {
    validator: (value: string) => isCronValid(value),
    message: 'This cron is invalid',
  },
];
</script>

<template>
  <c-card>
    <div mx-auto max-w-sm>
      <c-input-text
        v-model:value="cron"
        size="large"
        placeholder="* * * * *"
        :validation-rules="cronValidationRules"
        mb-3
      />
    </div>

    <div class="cron-string">
      {{ cronString }}
    </div>

    <n-divider />

    <div flex justify-center>
      <n-form :show-feedback="false" label-width="170" label-placement="left">
        <n-form-item label="详细">
          <n-switch v-model:value="cronstrueConfig.verbose" />
        </n-form-item>
        <n-form-item label="使用24小时制时间格式">
          <n-switch v-model:value="cronstrueConfig.use24HourTimeFormat" />
        </n-form-item>
        <n-form-item label="日从 0 开始">
          <n-switch v-model:value="cronstrueConfig.dayOfWeekStartIndexZero" />
        </n-form-item>
      </n-form>
    </div>
  </c-card>
  <c-card>
    <pre>
┌──────────── [可选] 秒 (0 - 59)
| ┌────────── 分 (0 - 59)
| | ┌──────── 时 (0 - 23)
| | | ┌────── 每月几号 (1 - 31)
| | | | ┌──── 月 (1 - 12) OR jan,feb,mar,apr ...
| | | | | ┌── 周几 (0 - 6, 星期天=0) 或者 sun,mon ...
| | | | | |
* * * * * * 命令</pre>

    <div v-if="styleStore.isSmallScreen">
      <c-card v-for="{ symbol, meaning, example, equivalent } in helpers" :key="symbol" mb-3 important:border-none>
        <div>
          符号: <strong>{{ symbol }}</strong>
        </div>
        <div>
          说明: <strong>{{ meaning }}</strong>
        </div>
        <div>
          举例:
          <strong><code>{{ example }}</code></strong>
        </div>
        <div>
          等效: <strong>{{ equivalent }}</strong>
        </div>
      </c-card>
    </div>
    <n-table v-else size="small">
      <thead>
        <tr>
          <th class="text-left" scope="col">
            符号
          </th>
          <th class="text-left" scope="col">
            说明
          </th>
          <th class="text-left" scope="col">
            举例
          </th>
          <th class="text-left" scope="col">
            等效
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="{ symbol, meaning, example, equivalent } in helpers" :key="symbol">
          <td>{{ symbol }}</td>
          <td>{{ meaning }}</td>
          <td>
            <code>{{ example }}</code>
          </td>
          <td>{{ equivalent }}</td>
        </tr>
      </tbody>
    </n-table>
  </c-card>
</template>

<style lang="less" scoped>
::v-deep(input) {
  font-size: 30px;
  font-family: monospace;
  padding: 5px;
  text-align: center;
}

.cron-string {
  text-align: center;
  font-size: 22px;
  opacity: 0.8;
  margin: 5px 0 15px;
}

pre {
  overflow: auto;
  padding: 10px 0;
}
</style>
