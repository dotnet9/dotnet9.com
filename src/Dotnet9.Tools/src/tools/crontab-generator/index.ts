import { Alarm } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'Crontab生成器',
  path: '/crontab-generator',
  description: '验证并生成 crontab，并获取 cron 计划的人类可读描述。-Validate and generate crontab and get the human readable description of the cron schedule.',
  keywords: [
    'crontab',
    'generator',
    'cronjob',
    'cron',
    'schedule',
    'parse',
    'expression',
    'year',
    'month',
    'week',
    'day',
    'minute',
    'second',
  ],
  component: () => import('./crontab-generator.vue'),
  icon: Alarm,
});
