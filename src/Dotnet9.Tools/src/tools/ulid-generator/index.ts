import { SortDescendingNumbers } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'ULID v4 生成器',
  path: '/ulid-generator',
  description: '生成随机的通用唯一词典分类标识符(英文Universally Unique Lexicographically Sortable Identifier，简写ULID).',
  keywords: ['ulid', 'generator', 'random', 'id', 'alphanumeric', 'identity', 'token', 'string', 'identifier', 'unique'],
  component: () => import('./ulid-generator.vue'),
  icon: SortDescendingNumbers,
  createdAt: new Date('2023-09-11'),
});
