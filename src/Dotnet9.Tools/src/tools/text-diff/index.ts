import { FileDiff } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: '文本比较',
  path: '/text-diff',
  description: '比较两个文本并查看它们之间的差异。',
  keywords: ['text', 'diff', 'compare', 'string', 'text diff', 'code'],
  component: () => import('./text-diff.vue'),
  icon: FileDiff,
  createdAt: new Date('2023-08-16'),
});
