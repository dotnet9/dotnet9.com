import { Braces } from '@vicons/tabler';
import { defineTool } from '../tool';

export const tool = defineTool({
  name: 'JSON 转 YAML',
  path: '/json-to-yaml-converter',
  description: '在线将JSON转换为YAML的转换器。',
  keywords: ['yaml', 'to', 'json'],
  component: () => import('./json-to-yaml.vue'),
  icon: Braces,
  createdAt: new Date('2023-04-10'),
});
