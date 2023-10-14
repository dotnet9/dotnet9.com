import { defineTool } from '../tool';
import BracketIcon from '~icons/mdi/code-brackets';

export const tool = defineTool({
  name: 'TOML 转 YAML',
  path: '/toml-to-yaml',
  description: '在线将TOML格式转换为YAML格式。',
  keywords: ['toml', 'yaml', 'convert', 'online', 'transform', 'parse'],
  component: () => import('./toml-to-yaml.vue'),
  icon: BracketIcon,
  createdAt: new Date('2023-06-23'),
});
