import { List } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'JSON 转 CSV【有问题】',
  path: '/json-to-csv',
  description: '使用自动标头检测将 JSON 转换为 CSV。',
  keywords: ['json', 'to', 'csv', 'convert'],
  component: () => import('./json-to-csv.vue'),
  icon: List,
  createdAt: new Date('2023-06-18'),
});
