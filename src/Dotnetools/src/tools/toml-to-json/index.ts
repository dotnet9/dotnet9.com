import { defineTool } from '../tool';

import BracketIcon from '~icons/mdi/code-brackets';

export const tool = defineTool({
  name: 'TOML 转 JSON',
  path: '/toml-to-json',
  description: '在线将TOML格式转换为JSON格式.',
  keywords: ['toml', 'json', 'convert', 'online', 'transform', 'parser'],
  component: () => import('./toml-to-json.vue'),
  icon: BracketIcon,
  createdAt: new Date('2023-06-23'),
});
