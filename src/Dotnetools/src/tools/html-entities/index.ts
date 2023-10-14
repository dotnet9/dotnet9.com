import { Code } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'html元素转义',
  path: '/html-entities',
  description: 'html元素转义或取消转义(替换 <,>, &, " and \' 为对应的html版本)',
  keywords: ['html', 'entities', 'escape', 'unescape', 'special', 'characters', 'tags'],
  component: () => import('./html-entities.vue'),
  icon: Code,
});
