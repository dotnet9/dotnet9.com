import { Calendar } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '日期时间转换器',
  path: '/date-converter',
  description: '将日期和时间转换为各种不同的格式',
  keywords: ['date', 'time', 'converter', 'iso', 'utc', 'timezone', 'year', 'month', 'day', 'minute', 'seconde'],
  component: () => import('./date-time-converter.vue'),
  icon: Calendar,
});
