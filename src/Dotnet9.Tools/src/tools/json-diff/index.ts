import { CompareArrowsRound } from '@vicons/material';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'JSON对比',
  path: '/json-diff',
  description: '比较两个 JSON 对象并获取它们之间的差异。',
  keywords: ['json', 'diff', 'compare', 'difference', 'object', 'data'],
  component: () => import('./json-diff.vue'),
  icon: CompareArrowsRound,
  createdAt: new Date('2023-04-20'),
});
